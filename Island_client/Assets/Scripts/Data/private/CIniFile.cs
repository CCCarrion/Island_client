using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


    public class CIniFile
    {
        private class SortById : IComparer
        {
            public int Compare(object Info1, object Info2)
            {
                CIniSection cIniSection = (CIniSection)Info1;
                CIniSection cIniSection2 = (CIniSection)Info2;
                if (Info1 == null && Info2 == null)
                {
                    return 0;
                }
                if (Info1 == null)
                {
                    return -1;
                }
                if (Info2 == null)
                {
                    return 1;
                }
                uint num;
                uint.TryParse(cIniSection.SectionName, out num);
                uint value;
                uint.TryParse(cIniSection2.SectionName, out value);
                return num.CompareTo(value);
            }
        }

        public List<CIniSection> m_List;

        public bool SectionExists(string SectionName)
        {
            foreach (CIniSection cIniSection in this.m_List)
            {
                if (cIniSection.SectionName.ToLower() == SectionName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public CIniSection FindSection(string SectionName)
        {
            foreach (CIniSection cIniSection in this.m_List)
            {
                if (cIniSection.SectionName.ToLower() == SectionName.ToLower())
                {
                    return cIniSection;
                }
            }
            return null;
        }

        public List<CIniSection> GetAllSection()
        {
            return this.m_List;
        }

        public List<string> GetAllSectionName()
        {
            List<string> list = new List<string>();
            foreach (CIniSection cIniSection in this.m_List)
            {
                list.Add(cIniSection.SectionName.ToLower());
            }
            return list;
        }

        public CIniFile()
        {
            this.m_List = new List<CIniSection>();
        }

        public void Clear()
        {
            this.m_List.Clear();
        }

        public void LoadFromStream(Stream stream)
        {
            StreamReader streamReader = new StreamReader(stream, Encoding.Default);
            this.m_List.Clear();
            CIniSection cIniSection = null;
            while (true)
            {
                string text = streamReader.ReadLine();
                if (text == null)
                {
                    break;
                }
                text = text.Trim();
                if (!(text == "") && text[0] != ';' && (text.Length < 2 || text[0] != '/' || text[1] != '/'))
                {
                    if (text != "" && text[0] == '[' && text[text.Length - 1] == ']')
                    {
                        text = text.Remove(0, 1);
                        text = text.Remove(text.Length - 1, 1);
                        cIniSection = this.FindSection(text);
                        if (cIniSection == null)
                        {
                            cIniSection = new CIniSection(text);
                            this.m_List.Add(cIniSection);
                        }
                    }
                    else
                    {
                        if (cIniSection == null)
                        {
                            cIniSection = this.FindSection("UnDefSection");
                            if (cIniSection == null)
                            {
                                cIniSection = new CIniSection("UnDefSection");
                                this.m_List.Add(cIniSection);
                            }
                        }
                        int num = text.IndexOf('=');
                        if (num != 0)
                        {
                            string key = text.Substring(0, num);
                            string value = text.Substring(num + 1, text.Length - num - 1);
                            cIniSection.AddKeyValue(key, value);
                        }
                        else
                        {
                            cIniSection.AddKeyValue(text, "");
                        }
                    }
                }
            }
            streamReader.Dispose();
        }


        public void SaveToStream(Stream stream)
        {
            StreamWriter streamWriter = new StreamWriter(stream,Encoding.Default);
            foreach (CIniSection cIniSection in this.m_List)
            {
                cIniSection.SaveToStream(streamWriter);
            }
            streamWriter.Dispose();
        }

        public string GetValue(string SectionName, string key, string defaultv)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection != null)
            {
                return cIniSection.GetValue(key, defaultv);
            }
            return defaultv;
        }

        public bool GetValue(string SectionName, string key, bool defaultv)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection != null)
            {
                return cIniSection.GetValue(key, defaultv);
            }
            return defaultv;
        }

        public int GetValue(string SectionName, string key, int defaultv)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection != null)
            {
                return cIniSection.GetValue(key, defaultv);
            }
            return defaultv;
        }

        public float GetValue(string SectionName, string key, float defaultv)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection != null)
            {
                return cIniSection.GetValue(key, defaultv);
            }
            return defaultv;
        }

        public ulong GetValue(string SectionName, string key, ulong defaultv)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection != null)
            {
                return cIniSection.GetValue(key, defaultv);
            }
            return defaultv;
        }

        public DateTime GetValue(string SectionName, string key, DateTime defaultv)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection != null)
            {
                return cIniSection.GetValue(key, defaultv);
            }
            return defaultv;
        }

        public CIniSection WriteValue(string SectionName, string key, string value)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection == null)
            {
                cIniSection = new CIniSection(SectionName);
                this.m_List.Add(cIniSection);
            }
            cIniSection.WriteValue(key, value);
            return cIniSection;
        }

        public CIniSection WriteValue(string SectionName, string key, bool value)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection == null)
            {
                cIniSection = new CIniSection(SectionName);
                this.m_List.Add(cIniSection);
            }
            cIniSection.WriteValue(key, value);
            return cIniSection;
        }

        public CIniSection WriteValue(string SectionName, string key, int value)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection == null)
            {
                cIniSection = new CIniSection(SectionName);
                this.m_List.Add(cIniSection);
            }
            cIniSection.WriteValue(key, value);
            return cIniSection;
        }

        public CIniSection WriteValue(string SectionName, string key, float value)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection == null)
            {
                cIniSection = new CIniSection(SectionName);
                this.m_List.Add(cIniSection);
            }
            cIniSection.WriteValue(key, value);
            return cIniSection;
        }

        public CIniSection WriteValue(string SectionName, string key, ulong value)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection == null)
            {
                cIniSection = new CIniSection(SectionName);
                this.m_List.Add(cIniSection);
            }
            cIniSection.WriteValue(key, value);
            return cIniSection;
        }

        public CIniSection WriteValue(string SectionName, string key, DateTime value)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection == null)
            {
                cIniSection = new CIniSection(SectionName);
                this.m_List.Add(cIniSection);
            }
            cIniSection.WriteValue(key, value);
            return cIniSection;
        }

        public void DeleteSection(string SectionName)
        {
            CIniSection cIniSection = this.FindSection(SectionName);
            if (cIniSection != null)
            {
                this.m_List.Remove(cIniSection);
            }
        }

        public void AddSection(CIniSection sec)
        {
            this.m_List.Add(sec);
        }
        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            this.LoadFromStream(fileStream);
            fileStream.Close();
            fileStream.Dispose();
        }

        public void SaveToFile(string filePath)
        {
            
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            this.SaveToStream(fileStream);
            fileStream.Close();
            fileStream.Dispose();
        }

        public void SaveToFileEx(string FileName)
        {
        }

        public int GetSectionCount()
        {
            return this.m_List.Count;
        }

        public CIniSection GetSection(int index)
        {
            if (index < 0 || index >= this.m_List.Count)
            {
                return null;
            }
            return this.m_List[index] as CIniSection;
        }
    }

