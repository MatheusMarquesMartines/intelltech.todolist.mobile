using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    class AuxClass
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string DataHora { get; set; }
        public bool Concluida { get; set; }
        public string GUID { get; set; }
    }
}
