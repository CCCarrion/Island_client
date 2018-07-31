using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//CSV单条数据
public class CSVEntry
{
    private CSVData m_dataCSV;
    private List<string> listValue;

    public List<string> ListValue
    {
        get
        {
            if (listValue == null)
            {
                listValue = new List<string>();
            }
            return this.listValue;
        }
    }


    public CSVEntry(List<string> csvLineList, CSVData parent)
    {
        this.listValue = csvLineList;
        this.m_dataCSV = parent;
    }


    public string GetValue(string key)
    {
        if (m_dataCSV.CSVFileNames.Contains(key))
        {
            int index = m_dataCSV.CSVFileNames.IndexOf(key);
            if (index < ListValue.Count)
            {
                return ListValue[index];
            }
            else
            {
                //SGG_Logger.Print(SGG_Logger.CSV, "CSVEntry ERROR: 根据提供字段名 " + key + " 获得的索引" + index + "超出当前数据索引！！");
                return "";
            }
        }
        else
        {
            //SGG_Logger.Print(SGG_Logger.CSV, "CSVEntry ERROR: 提供字段名： " + key + " 有误,当前表" + this.m_dataCSV.CSVName + "没有此字段！！");
            return "";
        }

    }

    public string this[string key]
    {
        get
        {
            return GetValue(key);
        }


    }

}

