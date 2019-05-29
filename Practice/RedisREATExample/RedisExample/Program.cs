using Redis_LinuxServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RedisExample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region use Redis & DB
            //Console.WriteLine("Connecting to the Redis Server...");
            //Repository repository = new Repository();

            //string input = string.Empty;
            //Console.WriteLine();
            //Console.WriteLine("Which currency's exchange rate ?");

            //input = Console.ReadLine();

            //if (!repository.IsSet(input)) //If there is no key matching in redis
            //{
            //    List<EratStruct> EratList = repository.GetErat(); //Connect Erat's DB and get currency list
            //    var erat = EratList.Find(value => value.Curr == input);

            //    while (erat == null) //If input still can't not match in DB
            //    {
            //        Console.WriteLine(string.Format("{0} Not found", input)); 
            //        Console.WriteLine("Plz type currency again...");
            //        input = Console.ReadLine();
            //        erat = EratList.Find(value => value.Curr == input);
            //    }
            //        repository.Set(input, erat, TimeSpan.FromDays(1)); //Else: write down in redis
            //}

            //string Value = repository.Get(input);
            //Console.WriteLine(string.Format("Get '{0}' Key Value :  {1}", input, Value));

            //Console.ReadLine();
            #endregion

            #region use Redis & API
            Console.WriteLine("Connecting to the Redis Server...");
            Repository repository = new Repository();

            string input = string.Empty;
            Console.WriteLine();
            Console.WriteLine("Which currency's exchange rate ?");

            input = Console.ReadLine();

            if (!repository.IsSet(input)) //If there is no key matching in redis
            {
                string url = 
                string.Format("https://tk-core.api.liontravel.com/api/V1/GetLionCurrencyRate?$filter=EratKind eq 'ORDER' and Date eq '{0}' and tCurr eq 'NTD'and Curr eq '{1}'", 
                DateTime.Now.ToString("yyyyMMdd"), input);

                Task.Run(async () =>
                {
                    HttpClient client = new HttpClient();
                    string response = await client.GetStringAsync(url);
                    repository.Set(input, response, TimeSpan.FromDays(1)); //Else: write down in redis
                }).Wait();
            }

            string Value = repository.Get(input);
            Console.WriteLine(string.Format("Get '{0}' Key Value :  {1}", input, Value));

            Console.ReadLine();
            #endregion

        }
    }
}
