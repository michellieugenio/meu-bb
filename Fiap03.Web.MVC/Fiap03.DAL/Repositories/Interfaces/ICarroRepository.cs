using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap03.DAL.Repositories.Interfaces
{
    public interface ICarroRepository
    {
        void Cadastrar(CarroMOD carro);
        IList<CarroMOD> Listar();
        void Editar(CarroMOD carro);
        CarroMOD Buscar(int id);
        IList<CarroMOD> BuscarPorAno(int ano);
        void excluir(int id);
    }
}
