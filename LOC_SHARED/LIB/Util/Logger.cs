using System;
using System.Collections.Generic;
using System.Threading;

namespace LOC_SHARED.Util
{
   public class Logger
    {
        static List< string> Data = new List< string>();
        static int index = 0;
        public static void Log(string data)
        {
          //  Console.WriteLine(data);
            
            Data.Add(data);
        }
        public static void Log(string data1, string data2)
        {
          // Console.WriteLine(data1 + ": "+ data2);
            
              Data.Add(data1 + ": " + data2);
        }

        public static void Log(byte[] array, int startIndex)
        {
            string bString = "";
            for (int b = startIndex; b < array.Length; b++)
            {
                bString += array[b] + ",";
            }
            Data.Add(bString.Substring(0, bString.Length - 1));
            //Console.WriteLine(bString.Substring(0, bString.Length - 1));
          
        }

        public static void Init() 
        {
            Thread th = new Thread(new ThreadStart(LogIntoScreen));
            th.Start();
        }

        public static void LogIntoScreen()
        {
            while (true)
            { 
                if (index < Data.Count)
                {
                    Console.WriteLine(Data[index]);
                    index++;
                }
            }
        }

    }
}
