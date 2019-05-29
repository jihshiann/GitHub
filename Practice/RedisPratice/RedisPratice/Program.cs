using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisPratice
{
    class Program
    {
        static string ConnectionString = string.Empty;
        static string IP1;
        static string IP2;
        static string IP3;
        static string Name;
        static string Password;
        static int DBIndex;
        static IDatabase Db;
        static ConnectionMultiplexer Redis;
        static string Key;
        static string Value;
        static void Main(string[] args)
        {
            Program RedisConnect = new Program();

            RedisConnect.ConnectInput();
            RedisConnect.RedisConnectimg();
            RedisConnect.Insert();
        }

        public void ConnectInput()
        {
            Console.WriteLine("Redis connection begin");
            
            Console.WriteLine("IP1");
            IP1 = Console.ReadLine();
            Console.WriteLine("IP2");
            IP2 = Console.ReadLine();
            Console.WriteLine("IP3");
            IP3 = Console.ReadLine();

            if (IP1.Length < 10)
            { IP1 = "10.30.3.49:8001"; }
            if (IP2.Length < 10)
            { IP2 = "10.30.3.49:8002"; }
            if (IP3.Length < 10)
            { IP3 = "10.30.3.49:8003"; }

            Console.WriteLine("name");
            Name = Console.ReadLine();
            if (Name == "")
            { Name = "Hotel.Jihshian"; }

            Console.WriteLine("password");
            Password = Console.ReadLine();
            if (Password == "")
            { Password = "liontravelHT"; }

            Console.WriteLine("DB index(0-15)");
            int.TryParse(Console.ReadLine(), out DBIndex);
            if (!(DBIndex >= 0 && DBIndex <= 15))
            { DBIndex = 0; }

        }

        public void RedisConnectimg()
        {
            Console.WriteLine("Redis connecting");
            ConnectionString = string.Format("{0}, {1}, {2}, name = {3}, password = {4}", IP1, IP2, IP3, Name, Password);

            Redis = ConnectionMultiplexer.Connect(ConnectionString);
            Db = Redis.GetDatabase(DBIndex);

            if (Db != null)
            { Console.WriteLine("Redis connected"); }
        }

        public void Insert()
        {
            Console.WriteLine("Insert key");
            Key = Console.ReadLine();

            if (Key == "")
            { Key = "JihShiann"; }

            Console.WriteLine("Insert value");
            Value = Console.ReadLine();
            if (Value == "")
            { Value = "Test"; }

            Db.StringSet(Key, Value);

            Console.WriteLine(string.Format("value={0}",Db.StringGet(Key)));
            Console.ReadLine();

        }
    }
}
