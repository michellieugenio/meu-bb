using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap03.MOD
{
    public class MarcaMOD
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public string CNPJ { get; set; }
        public IList<CarroMOD> Carros { get; set; }
    }
}
