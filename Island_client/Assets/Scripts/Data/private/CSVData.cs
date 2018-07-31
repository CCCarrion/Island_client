
//CSV 表


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

public class CSVData
{
    private string csvName;   //表名
    private List<string> csvFileNames;
    private Dictionary<string, CSVEntry> EntryDic;   //每条数据

    public string CSVName
    {
        get { return csvName; }
    }
    public List<string> CSVFileNames
    {
        get
        {
            if (csvFileNames == null)
            {
                csvFileNames = new List<string>();
            }
            return this.csvFileNames;
        }
    }

    public void LoadCSV(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            //SGG_Logger.Print(SGG_Logger.CSV, "CSVData ERROR ：希望加载的表名" + path + "不存在或为空！！");
            return;
        }
        if (!loadCSV(path))
            return;
    }

    //路径包括 表名.CSV
    public void SaveCSV(string path)
    {
        this.saveFile(path);
    }

    public bool AddEntry(int ID, CSVEntry csvEntry)
    {
    
        if (this.EntryDic.ContainsKey(ID.ToString()))
        {
            //SGG_Logger.Print(SGG_Logger.CSV, "ID" + ID + "已经存在，无法添加！！");
            return false;
        }
        this.EntryDic.Add(ID.ToString(), csvEntry);
        return true;
    }

    public List<CSVEntry> GetCsvEntrys()
    {
        return this.EntryDic.Values.ToList();
    }

    public List<string> GetCsvEntryKeys()
    {
        return this.EntryDic.Keys.ToList();
    }


    public CSVEntry GetCsvEntry(string ID)
    {
        if (EntryDic.ContainsKey(ID))
        {
            return this.EntryDic[ID];
        }
        return null;
    }


    public Dictionary<string, CSVEntry> GetCsvEctryDic()
    {
        return this.EntryDic;
    }

    #region CSV 辅助
    /// <summary>
    /// 解析CSV
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private bool loadCSV(string path)
    {
        string[] csvData = null;
        try
        {
            csvData = File.ReadAllLines(path, Encoding.Default);

        }
        catch
        {
            //SGG_Logger.Print(SGG_Logger.CSV, "CSVData ERROR ：一次性加载表格" + path + "的时候出错！请关闭要加载的表格！！");
        }

        /*********************解析表操作******************/
        if (csvData == null)
            return false;
        csvName = Path.GetFileName(path);
        List<string> indexContentList = new List<string>();
        this.EntryDic = new Dictionary<string, CSVEntry>();
        csvFileNames = DealSpecialLine(csvData[0].Split(','));    //头字段
        for (int dataLine = 1; dataLine < csvData.Length; dataLine++)
        {

            string[] idealFile = csvData[dataLine].Split(',');   //理想的字段
            int Fenhao = csvData[dataLine].Split('"').Length;
            int fieldNameCount = 0;
            if (idealFile.Length == csvFileNames.Count && Fenhao == 1)
            {
                // Debug.Log("这是理想字段");

                for (fieldNameCount = 0; fieldNameCount < csvFileNames.Count; fieldNameCount++)
                {
                    indexContentList.Add(idealFile[fieldNameCount]);
                }
                try
                {
                    this.EntryDic.Add(indexContentList[0], new CSVEntry(indexContentList, this));
                }
                catch (ArgumentException e)
                {
                    //SGG_Logger.Print(SGG_Logger.CSV, "CSVData ERROR +" + csvName + ": 第" + dataLine + "行字段键值重复！！");
                }

                indexContentList = new List<string>();  //一行一个新的List
            }
            else
            {
                List<string> newLineField = DealSpecialLine(idealFile);
                for (fieldNameCount = 0; fieldNameCount < csvFileNames.Count; fieldNameCount++)
                {
                    if (fieldNameCount >= newLineField.Count)
                    {
                        indexContentList.Add("");
                        continue;
                    }
                    indexContentList.Add(newLineField[fieldNameCount]);
                }
                try
                {
                    this.EntryDic.Add(indexContentList[0], new CSVEntry(indexContentList, this));
                }
                catch (ArgumentException e)
                {

                    //SGG_Logger.Print(SGG_Logger.CSV, "CSVData ERROR +" + csvName + ": 第" + dataLine + "行字段键值重复！！");
                }

                indexContentList = new List<string>();  //一行一个新的List



            }

        }
        return true;
    }


    private void saveFile(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            //SGG_Logger.Print(SGG_Logger.CSV, "CSVData ERROR ：希望保存的路径" + path + "不存在或为空！！");
        }

        if (this.csvFileNames == null || this.EntryDic == null)
        {
            //SGG_Logger.Print(SGG_Logger.CSV, "CSVData ERROR ：数据为空，无法保存文件");
            return;
        }


        FileInfo fi = new FileInfo(path);
        if (!fi.Directory.Exists)
        {
            fi.Directory.Create();
        }
        FileStream csvStream = new FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write);
        StreamWriter csvWriter = new StreamWriter(csvStream, System.Text.Encoding.Default);

        //写出列名称
        string data = "";
        for (int i = 0; i < this.csvFileNames.Count; i++)
        {
            data += this.csvFileNames[i];
            if (i < this.csvFileNames.Count - 1)
            {
                data += ",";
            }
        }
        csvWriter.WriteLine(data);

        List<CSVEntry> CSVEntryList = this.EntryDic.Values.ToList();
        //写出各行数据
        for (int i = 0; i < CSVEntryList.Count; i++)
        {
            List<string> LineValue = CSVEntryList[i].ListValue;
            data = "";
            for (int j = 0; j < LineValue.Count; j++)
            {
                string str = LineValue[j];
                str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                if (str.Contains(",") || str.Contains("\"") || str.Contains("\r") || str.Contains("\n")) //含逗号 冒号 换行符的需要放到引号中
                {
                    str = string.Format("\"{0}\"", str);
                }

                data += str;
                if (j < LineValue.Count - 1)
                {
                    data += ",";
                }
            }
            csvWriter.WriteLine(data);
        }
        csvWriter.Close();
        csvStream.Close();
    }

    /// <summary>
    /// 处理特殊字段
    /// </summary>
    /// <param name="idealFile"></param>
    /// <returns></returns>
    private static List<string> DealSpecialLine(string[] idealFile)
    {
        StringBuilder curField = new StringBuilder(); //当前要被添加的完整字段
        List<string> newLineField = new List<string>();

        for (int i = 0; i < idealFile.Length; i++)
        {
            curField.Append(idealFile[i]);
            char[] file = curField.ToString().ToCharArray();
            int count = 0;


            for (int j = 0; j < file.Length; j++)
            {
                if (file[j] == '"')
                {
                    count++;

                }
            }
            if (count % 2 == 0)
            {

                char[] FieldChar = curField.ToString().ToCharArray();
                List<int> Removeindex = new List<int>();   //要被移除的索引
                count = 0;
                for (int j = 0; j < FieldChar.Length; j++)
                {
                    if (FieldChar[j] == '"')
                    {
                        count++;

                        if (count % 2 == 1 || j == FieldChar.Length - 1)   //移除多余的引号
                        {
                            Removeindex.Add(j);
                        }


                    }

                }

                curField = new StringBuilder();
                for (int j = 0; j < FieldChar.Length; j++)
                {
                    if (!Removeindex.Contains(j))
                    {
                        curField.Append(FieldChar[j]);
                    }
                }

                newLineField.Add(curField.ToString());


                curField = new StringBuilder();
            }
            else
            {
                curField.Append(",");
            }

        }

        return newLineField;
    }
    #endregion



    //测试
    public void Print()
    {
        StringBuilder line = new StringBuilder();
        foreach (var curCsvEntry in this.EntryDic.Values)
        {
            line = new StringBuilder();
            foreach (var curField in curCsvEntry.ListValue)
            {
                line.Append(curField + "  ");
            }
            //SGG_Logger.Print(SGG_Logger.CSV, line.ToString());
        }
    }


}

