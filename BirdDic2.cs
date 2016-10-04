using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDictionary_csharp
{
    class BirdDic2
    {
        HashSet<String> allWords;
        int maxReadLines;

        private HBDatabase db;
        private string pathDic = @"C:\diccionario.dic";
        System.IO.StreamReader file;

        public BirdDic2(){
            this.allWords = new HashSet<string>();
            this.maxReadLines = 100;

            this.file = new System.IO.StreamReader(this.pathDic);
            this.db = new HBDatabase();
        }

        public void SaveWords()
        {
            string line;
            int countLines = 0;
            //HashSet<Word> words = new HashSet<Word>();
            while ((line = this.file.ReadLine()) != null)
            {
                countLines++;
                Console.WriteLine("[ "+countLines+" ] Save line...");

                this.ProcessLine(line);
                /*
                foreach (Word word in this.ProcessLine(line))
                {
                    words.Add(word);
                }
                */
                if ((countLines % this.maxReadLines) == 0)
                {
                    //aqui hago el bulk copy...
                    Console.Write("BulkCopy... ");
                    this.db.BulkCopy();                                        
                  //  words.Clear();
                    this.db.ClearDataTables();
                    Console.WriteLine("OK!");
                }
            }

            //termine el archivo, busco si me quedo algo por poner...
            //if (words.Count > 0)
            if ((countLines % this.maxReadLines) != 0)
            {
                //aqui hago el bulk copy...
                this.db.BulkCopy();
                //words.Clear();
                this.db.ClearDataTables();
            }
        }

        private IEnumerable<Word> ProcessLine(string line)
        {
            List<string>words = line.Split(new char[] {' '}).ToList<string>();
            string motherWord = words[0];

            List<Word> processWords = new List<Word>();
            Word w;
            foreach (string word in words)
            {
                if (!this.allWords.Contains(word)) {
                    this.allWords.Add(word);

                    //aprovecho y ya la meto en el data set...
                    w = new Word(word, motherWord, ref this.db);
                    processWords.Add(w);                    

                    //hacer este loop me hace que el proceso de leido y procesado sin inserccion lleve 1:20, 
                    //con esto comentado, solo toma 40 seg...
                    //por esta razon, modificare el construcctor de palbras, para que tome referencia de HDDatabase
                    //y relice el insertRowIn cuando genere el deletreo y repeticion
                    // w.palabra.InsertRowIn(ref this.db.tPalabras);
                    /*
                    foreach (model.mRepeticiones item in w.repeticione)
                    {
                        item.InsertRowIn(ref this.db.tRepeticion);
                    }

                    foreach (model.mDeletro item in w.deletreo)
                    {
                        item.InsertRowIn(ref this.db.tDeletreo);
                    }
                    */
                }
            }
            return processWords;
        }
    }
}
