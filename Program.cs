using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDictionary_csharp
{
    class Program
    {
        static void Main(string[] args)
        {           
            Console.WriteLine("## LoadDictionary - HangmanBird - C# - V0.1");
            BirdDic2 dic = new BirdDic2();
                        
            DateTime time1 = DateTime.Now;

            dic.SaveWords();

            DateTime time2 = DateTime.Now;           
            TimeSpan total = new TimeSpan(time2.Ticks - time1.Ticks);
            Console.Write("TIEMPO: " + total.ToString());
            
            Console.Read();

        }
    }
}
