using Fiap03.MOD.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap03.MOD
{
    public class DocumentoMOD
    {
        public int Renavam { get; set; }
        public Categoria? Categoria { get; set; }
        public DateTime DataFabricacao { get; set; }
    }
}
