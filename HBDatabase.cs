using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace LoadDictionary_csharp
{
    class HBDatabase
    {

        private SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlBulkCopy bulkCopy;

        //lo pongo por el momento public, algun dia lo pondra bien...
        //quiero aprovechar el loop de words en BirdDic2 para ir llenando la tablas esta...
        public DataTable tPalabras;
        public DataTable tDeletreo;
        public DataTable tRepeticion;

        public HBDatabase()
        {            

            this.cb.DataSource = @"OLIVETTI-PC\SQLEXPRESS";
            this.cb.InitialCatalog = "HangmanBird";
            this.cb.UserID = "bird";
            this.cb.Password = "bird";            

            this.conn = new SqlConnection(cb.ConnectionString);
            this.cmd = new SqlCommand();
            this.cmd.Connection = this.conn;
            this.conn.Open();

            this.bulkCopy = new SqlBulkCopy(this.conn);

            this.BuildTablePalabras();
            this.BuildTableDeletreo();
            this.BuildTableRepeticion();
        }

        private void BuildTableRepeticion()
        {
            this.tRepeticion = new DataTable();

            this.tRepeticion.Columns.Add("Codigo_Palabra", typeof(int));
            this.tRepeticion.Columns.Add("Letra", typeof(string));
            this.tRepeticion.Columns.Add("Repeticiones", typeof(int));
        }

        private void BuildTableDeletreo()
        {
            this.tDeletreo = new DataTable();
            this.tDeletreo.Columns.Add("Codigo_Palabra", typeof(int));
            this.tDeletreo.Columns.Add("Letra", typeof(string));
            this.tDeletreo.Columns.Add("Posicion", typeof(int));
        }

        private void BuildTablePalabras()
        {
            this.tPalabras = new DataTable();
            this.tPalabras.Columns.Add("Codigo_Palabra", typeof(int));
            this.tPalabras.Columns.Add("Palabra", typeof(string));
            this.tPalabras.Columns.Add("Cantidad_Letras", typeof(int));
            this.tPalabras.Columns.Add("Indice_uso", typeof(int));
            this.tPalabras.Columns.Add("Palabra_Raiz", typeof(string));
            this.tPalabras.Columns.Add("Palabra_Limpia", typeof(string));            
        }      
               
        public void InsertWord(List<Word> words)
        {
            this.ClearDataTables();
            foreach (Word word in words)
            {

                word.palabra.InsertRowIn(ref this.tPalabras);
                foreach (var item in word.repeticione)
                {
                    item.InsertRowIn(ref this.tRepeticion);
                }
                foreach (var item in word.deletreo)
                {
                    item.InsertRowIn(ref this.tDeletreo);
                }
            }
            this.BulkCopy();            
        }

        public void ClearDataTables()
        {
            this.tPalabras.Clear();
            this.tDeletreo.Clear();
            this.tRepeticion.Clear();
        }

        public void BulkCopy()
        {
            try
            {
                if (this.tPalabras.Rows.Count > 0)
                {
                    this.bulkCopy.DestinationTableName = "Palabras";
                    
                    /*
                    foreach (DataRow row in this.tPalabras.Rows)
                    {                        
                        Console.WriteLine("Palabra: " + row["Palabra"]);
                        Console.WriteLine("ID: " + row["Codigo_Palabra"]);
                        Console.WriteLine("----------");
                    }
                    */
                    
                    this.bulkCopy.WriteToServer(this.tPalabras);
                }

                if (this.tRepeticion.Rows.Count > 0)
                {
                    this.bulkCopy.DestinationTableName = "Repeticion_Letras";
                    this.bulkCopy.WriteToServer(this.tRepeticion);
                }                                
                
                if (this.tDeletreo.Rows.Count > 0)
                {
                    this.bulkCopy.DestinationTableName = "Deletreo_Palabras";
                    this.bulkCopy.WriteToServer(this.tDeletreo);
                }                
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error - duplicate primary key or unique column ?");
            }
        }
    }
}
