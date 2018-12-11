using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap03.DAL.Interfaces
{
    public interface IMarcaRepository
    {
        void Cadastrar(MarcaMOD marca);
        IList<MarcaMOD> Listar();
        void Editar(MarcaMOD marca);
        MarcaMOD Buscar(int id);
        MarcaMOD BuscarComCarros(int id);

    }
}
