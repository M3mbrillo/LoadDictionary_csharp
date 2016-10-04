using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDictionary_csharp
{
    class BirdDic
    {
        private HBDatabase db { get; set; }
        private string pathDic = @"C:\diccionario.dic";
        System.IO.StreamReader file;

        HashSet<string> all_words;

        public BirdDic()
        {
            this.all_words = new HashSet<string>();
            this.file = new System.IO.StreamReader(this.pathDic);
            this.db = new HBDatabase();
        }

        public void SaveDic()
        {
            List<Word> words;
            
            while((words = this.ReadWords()) != null)
            {                
                this.db.InsertWord(words);
            }
            Console.WriteLine("End read");
        }

        private int i = 1;
        private List<Word> ReadWords()
        {
            int maxReading = 100;
            int countRead = maxReading;
            List<Word> words = new List<Word>();

            string line = "";
            //IEnumerable<string> readWords;            
            List<string> readWords = new List<string>();

            
            //leo muchas lineas, y luego proceso las palabras
            while (countRead != 0)
            {
                if ((line = this.file.ReadLine()) != null)
                {
                    Console.WriteLine("[" + this.i + "] Read Line");
                    this.i++;
                    //readWords = line.Split(" ".ToCharArray()).Distinct();
                    readWords.AddRange(line.Split(" ".ToCharArray()).Distinct());
                }
                else
                {
                    break;
                }

                countRead--;
            }
            if(readWords.Count == 0)
            {
                return null;
            }

            //elimino los repetidos del bloque...
            IEnumerable<string>a =  readWords.Distinct();
            foreach (string word in a)
            {
                if (word.Length > 0)
                {
                    if (this.all_words.Contains(word) == false)
                    {
                        this.all_words.Add(word);
                        words.Add(new Word(word, readWords.ElementAt(0)));
                    }                    
                }
            }            
            return words;
        }
    }
}
