using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace LoadDictionary_csharp.model
{
    class mWord
    {
        public string palabra { get; set; }
        public string palabraMadre { get; set; }
        public int cantidadLetras { get; set; }
        public int myIdentity { get; set; }
        public string palabraLimpia { get; set; }

        public mWord()
        {
            this.cantidadLetras = 0;
            this.palabra = "";
            this.palabraMadre = "";
            this.myIdentity = 0;
            
        }

        public void InsertRowIn(ref DataTable _dt)
        {
            DataRow row = _dt.NewRow();
            row["Codigo_Palabra"] = this.myIdentity;
            row["Palabra"] = this.palabra;
            row["Cantidad_Letras"] = this.cantidadLetras;
            row["Indice_uso"] = 0;
            row["Palabra_Raiz"] = this.palabraMadre;
            row["Palabra_Limpia"] = this.palabraLimpia;
            
            _dt.Rows.Add(row);
        }
    }
}
