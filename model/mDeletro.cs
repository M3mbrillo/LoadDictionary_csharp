using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;


namespace LoadDictionary_csharp.model
{
    class mDeletro
    {
        public int posicion { get; set; }
        public string letra { get; set; }
        public int codigoPalabra { get; set; }

        public mDeletro(int _pos, string _l, int _p)
        {
            this.posicion = _pos;
            this.letra = _l;
            this.codigoPalabra = _p;
        }

        public void InsertRowIn(ref DataTable _dt)
        {
            DataRow row = _dt.NewRow();
            row["Codigo_Palabra"] = this.codigoPalabra;
            row["Letra"] = this.letra;
            row["Posicion"] = this.posicion;
            _dt.Rows.Add(row);
        }
    }
}
