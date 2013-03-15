using System;
using System.Collections.Generic;
using System.IO;

namespace PerformanceLab.Utils.ReportMaker
{
    class ConfigMaster
    {
        protected static Dictionary<string, string> Config = new Dictionary<string, string>(); 

        protected ConfigMaster(FileInfo configFile, char delimiter)
        {
            try
            {
                if (configFile != null && configFile.Exists)
                {
                    using (var reader = new StreamReader(configFile.FullName,System.Text.Encoding.Default))
                    {
                        while (!reader.EndOfStream)
                        {
                            var readLine = reader.ReadLine();
                            if (readLine == null || readLine.StartsWith("#")) continue;
                            var pair = readLine.Split(delimiter);
                            Config.Add(pair[0], pair[1]);
                        }
                    }
                }
            }
            catch (ArgumentException ae)
            {
                Popup.ShowException(ae);
            }
            catch (IOException ioe)
            {
                Popup.ShowException(ioe);
            }
            catch (NullReferenceException nre)
            {
                Popup.ShowException(nre);
            }
        }
    }

    class JMeterLogDictionary : ConfigMaster
    {
        public JMeterLogDictionary(FileInfo configFile, char delimiter = ';') : base(configFile, delimiter)
        {
        }

        public string Lookup(string keyToFind)
        {
            if (keyToFind == null) return null;
            string value;
            Config.TryGetValue(keyToFind, out value);
            return value ?? System.IO.Path.GetFileName(keyToFind);
        }
        public string search(string toMatch)
        {
            string []temp=new string[Config.Keys.Count];
            Config.Keys.CopyTo(temp,0);
            for (int i = 0; i < Config.Keys.Count; i++)
            {
                if(toMatch.Contains(temp[i]))
                {
                    string value;
                    Config.TryGetValue(temp[i], out value);
                    return value;
                }                
            }
            return "н/я";
        }
    }
}
