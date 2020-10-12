using System;
using System.Collections.Generic;
using System.IO;

namespace Salesforce_Backup
{
    public class Backup
    {
        public void SaveToCSV<T>(List<T> recordsList, string objectName)
        {
            var Timestamp = DateTime.Now.ToString("dd_MMM_HH_mm_ss");
            string header = CreateHeader(recordsList);
            File.WriteAllText(objectName + "_" + Timestamp + ".csv", String.Join('\n', header + StringifyRecords(recordsList)));
        }

        public string CreateHeader<T>(List<T> recordsList)
        {
            string header = String.Empty;
            var properties = recordsList[0]?.GetType().GetProperties();
            for (int i = 0; i < properties.Length - 1; i++)
            {
                if (!(i == properties.Length - 2))
                {
                    header += properties[i].Name + ',';
                }
                else
                {
                    header += properties[i].Name + '\n';
                }
            }

            return header;
        }

        public string StringifyRecords<T>(List<T> recordsList)
        {
            string AllLines = String.Empty;
            var properties = recordsList[0]?.GetType().GetProperties();
            foreach (var item in recordsList)
            {
                string line = String.Empty;
                for (int i = 0; i < properties.Length - 1; i++)
                {
                    if (!(i == properties.Length - 2))
                    {
                        line += item.GetType().GetProperty(properties[i].Name).GetValue(item, null) + ",";
                    }
                    else
                    {
                        line += item.GetType().GetProperty(properties[i].Name).GetValue(item, null) + "\n";
                    }
                }
                AllLines += line;
            }
            return AllLines.Remove(AllLines.Length - 1);
        }
    }
}
