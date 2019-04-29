using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CsvToJson
{
    class Program
    {
        //Data Model
        public class DataGroup
        {
            public string GroupCode { get; set; }
            public List<Data> Datas { get; set; }
        }
        public class Data
        {
            public string DataCode { get; set; }
            public string DataName { get; set; }
            public string Colspan3 { get; set; }
            public string Colspan4 { get; set; }
            public string Colspan5 { get; set; }
        }
        static void Main(string[] args)
        {
            string CsvFilePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Csv.csv");
            var CsvLines = File.ReadAllLines(CsvFilePath);
            var CsvGroups = CsvLines.GroupBy(CsvLine => new
            {
                cols1 = CsvLine.Split(',')[0],
                cols2 = CsvLine.Split(',')[1],
            });

            List<DataGroup> DataList = new List<DataGroup>();
            foreach (var CsvGroup in CsvGroups)
            {
                List<Data> Datas = new List<Data>();

                foreach (var CsvLine in CsvGroup)
                {
                    string[] cols = CsvLine.Split(',');

                    Datas.Add(new Data()
                    {
                        DataCode = cols[0],
                        DataName = cols[1],
                        Colspan3 = cols[2],
                        Colspan4 = cols[3],
                        Colspan5 = cols[4],
                    });
                }

                DataList.Add(new DataGroup()
                {
                    GroupCode = CsvGroup.Key.cols1,
                    Datas = Datas
                });

            }
            var TicketJson = JsonConvert.SerializeObject(DataList);
            string JsonFilePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Json.json");
            File.WriteAllText(JsonFilePath, TicketJson);
        }
    }
}
