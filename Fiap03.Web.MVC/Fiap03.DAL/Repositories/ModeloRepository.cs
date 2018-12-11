using Dapper;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap03.DAL.Repositories
{
    public class ModeloRepository : IModeloRepository
    {
        public void Cadastrar(ModeloMOD modelo)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"INSERT INTO Modelo
                            (Nome, MarcaID)
                              VALUES 
                            (@Nome, @MarcaId) SELECT CAST(SCOPE_IDENTITY() as int);";
                int id = db.Query<int>(sql, modelo).Single();
                modelo.Id = id;
            }
        }
        //passei parametro pq quero sdaber o modele especifico da marca
        public IList<ModeloMOD> Listar(int marcaId)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Modelo WHERE MarcaId=@Id";
                var lista = db.Query<ModeloMOD>(sql, new { Id = marcaId }).ToList();
                return lista;
            }
        }
    }
}
