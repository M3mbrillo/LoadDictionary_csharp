using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace LoadDictionary_csharp.model
{
    class mRepeticiones
    {
        public int repeticion { get; set; }
        public string letra { get; set; }
        public int codigoPalabra { get; set; }

        public mRepeticiones(int _r, string _l, int _p)
        {
            this.repeticion = _r;
            this.letra = _l;
            this.codigoPalabra = _p;
        }

        public void InsertRowIn(ref DataTable _dt)
        {
            DataRow row = _dt.NewRow();
            row["Codigo_Palabra"] = this.codigoPalabra;
            row["Letra"] = this.letra;
            row["Repeticiones"] = this.repeticion;
            _dt.Rows.Add(row);
        }
    }
}
