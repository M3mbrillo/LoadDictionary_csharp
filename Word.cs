using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadDictionary_csharp
{
    class Word
    {       

        public model.mWord palabra { get; set; }
        public List<model.mDeletro> deletreo { get; set; }
        public List<model.mRepeticiones> repeticione { get; set; }       

        public static int identity = 0;

        public Word()
        {
            this.deletreo = new List<model.mDeletro>();
            this.repeticione = new List<model.mRepeticiones>();
            this.palabra.palabra = "";
            this.palabra.palabraMadre = "";
            this.palabra.cantidadLetras = 0;
            this.palabra.myIdentity = Word.identity;
            Word.identity++;
        }

        public Word(string _palabra, string _palabraMadre)
        {
            this.palabra = new model.mWord();
            this.deletreo = new List<model.mDeletro>();
            this.repeticione = new List<model.mRepeticiones>();
            this.palabra.myIdentity = Word.identity;

            this.BuildWord(_palabra, _palabraMadre);            
            Word.identity++;
        }

        public Word(string _palabra, string _palabraMadre, ref HBDatabase _db)
        {            
            this.palabra = new model.mWord();
            this.deletreo = new List<model.mDeletro>();
            this.repeticione = new List<model.mRepeticiones>();
            this.palabra.myIdentity = Word.identity;

            //this.BuildWord(_palabra, _palabraMadre);           
            this.BuildWordAndInsert(_palabra, _palabraMadre, ref _db);
            
            Word.identity++;
        }

        private void BuildWordAndInsert(string _palabra, string _palabraMadre, ref HBDatabase _db)
        {            
            this.palabra.palabraMadre = _palabraMadre;
            this.palabra.palabra = _palabra;
            this.palabra.cantidadLetras = _palabra.Length;

            int pos = 0;

            model.mDeletro dele;
            model.mRepeticiones repe;
            foreach (char letter in this.palabra.palabra)
            {
                dele = new model.mDeletro(pos, letter.ToString(), this.palabra.myIdentity);
                dele.InsertRowIn(ref _db.tDeletreo);
                this.deletreo.Add(dele);
                pos++;

                if (!this.repeticione.Exists(x => x.letra == letter.ToString()))
                {
                    this.repeticione.Add(new model.mRepeticiones(1, letter.ToString(), this.palabra.myIdentity));
                }
                else
                {
                    this.repeticione.Find(x => x.letra == letter.ToString()).repeticion++;
                }
            }

            string _pabra_limpia = _palabra.Replace('á', 'a');
            _pabra_limpia = _pabra_limpia.Replace('é', 'e');
            _pabra_limpia = _pabra_limpia.Replace('í', 'i');
            _pabra_limpia = _pabra_limpia.Replace('ó', 'o');
            _pabra_limpia = _pabra_limpia.Replace('ú', 'u');
            this.palabra.palabraLimpia = _pabra_limpia;
            this.palabra.InsertRowIn(ref _db.tPalabras);
        }

        private void BuildWord(string _palabra, string _palabraMadre)
        {
            this.palabra = new model.mWord();

            this.palabra.palabraMadre = _palabraMadre;
            this.palabra.palabra = _palabra;
            this.palabra.cantidadLetras = _palabra.Length;

            int pos = 0;
            foreach (char letter in this.palabra.palabra)
            {
                this.deletreo.Add(new model.mDeletro(pos, letter.ToString(), this.palabra.myIdentity));
                pos++;

                if (!this.repeticione.Exists(x => x.letra == letter.ToString())) {
                    this.repeticione.Add(new model.mRepeticiones(1, letter.ToString(), this.palabra.myIdentity));
                }
                else
                {
                    this.repeticione.Find(x => x.letra == letter.ToString()).repeticion++;
                }
            }

            string _pabra_limpia = _palabra.Replace('á', 'a');
            _pabra_limpia = _pabra_limpia.Replace('é', 'e');
            _pabra_limpia = _pabra_limpia.Replace('í', 'i');
            _pabra_limpia = _pabra_limpia.Replace('ó', 'o');
            _pabra_limpia = _pabra_limpia.Replace('ú', 'u');
            this.palabra.palabraLimpia = _pabra_limpia;
        }       
    }
}
