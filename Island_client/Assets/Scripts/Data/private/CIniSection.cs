using System;
using System.Collections.Generic;
using System.IO;

    public class CIniSection
    {
        private Dictionary<string, string> m_dicKeyValue;

        private string sectionName;

        public string SectionName
        {
            get
            {
                return this.sectionName;
            }
        }

        public int Count
        {
            get
            {
                return this.m_dicKeyValue.Count;
            }
        }

        public CIniSection(string secName)
        {
            this.sectionName = secName;
            this.m_dicKeyValue = new Dictionary<string, string>();
        }

        public void Clear()
        {
            this.m_dicKeyValue.Clear();
        }

        public void AddKeyValue(string key, string value)
        {
            string text;
            if (this.m_dicKeyValue.TryGetValue(key, out text))
            {
                this.m_dicKeyValue[key] = value;
                return;
            }
            this.m_dicKeyValue.Add(key, value);
        }

        public void WriteValue(string key, string value)
        {
            this.AddKeyValue(key, value);
        }

        public void WriteValue(string key, bool value)
        {
            this.AddKeyValue(key, Convert.ToString(value));
        }

        public void WriteValue(string key, int value)
        {
            this.AddKeyValue(key, Convert.ToString(value));
        }

        public void WriteValue(string key, float value)
        {
            this.AddKeyValue(key, Convert.ToString(value));
        }

        public void WriteValue(string key, ulong value)
        {
            this.AddKeyValue(key, Convert.ToString(value));
        }

        public void WriteValue(string key, DateTime value)
        {
            this.AddKeyValue(key, Convert.ToString(value));
        }

        public string GetValue(string key, string defaultv)
        {
            string result;
            if (this.m_dicKeyValue.TryGetValue(key, out result))
            {
                return result;
            }
            return defaultv;
        }

        public bool GetValue(string key, bool defaultv)
        {
            string value = this.GetValue(key, Convert.ToString(defaultv));
            return Convert.ToBoolean(value);
        }

        public int GetValue(string key, int defaultv)
        {
            string value = this.GetValue(key, Convert.ToString(defaultv));
            return Convert.ToInt32(value);
        }

        public float GetValue(string key, float defaultv)
        {
            string value = this.GetValue(key, Convert.ToString(defaultv));
            return Convert.ToSingle(value);
        }

        public ulong GetValue(string key, ulong defaultv)
        {
            string value = this.GetValue(key, Convert.ToString(defaultv));
            return Convert.ToUInt64(value);
        }

        public DateTime GetValue(string key, DateTime defaultv)
        {
            string value = this.GetValue(key, Convert.ToString(defaultv));
            return Convert.ToDateTime(value);
        }

        public void SaveToStream(Stream stream)
        {
            StreamWriter streamWriter = new StreamWriter(stream);
            this.SaveToStream(streamWriter);
            streamWriter.Dispose();
        }

        public void SaveToStream(StreamWriter SW)
        {
            SW.WriteLine("[" + this.sectionName + "]");
            foreach (KeyValuePair<string, string> current in this.m_dicKeyValue)
            {
                SW.WriteLine(current.Key + "=" + current.Value);
            }
        }

        public Dictionary<string, string> GetKeyValueDictionary()
        {
            return this.m_dicKeyValue;
        }


        public bool isSame(CIniSection other)
        {
            if (other.m_dicKeyValue.Count != m_dicKeyValue.Count)
            {
                return false;
            }

            foreach (var pair in m_dicKeyValue)
            {
                if (!other.m_dicKeyValue.ContainsKey(pair.Key)) return false;
                if (other.m_dicKeyValue[pair.Key] != pair.Value) return false;
            }

            return true;
        }

        public bool isSameID(CIniSection other)
        {
            return other.sectionName == sectionName;
        }
    }

