using Dapper;
using Fiap03.DAL.Interfaces;
using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap03.DAL.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        public MarcaMOD Buscar(int id)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Marca 
                         WHERE
                          Id = @Id";
                var marca = db.Query<MarcaMOD>(sql, new { id }).SingleOrDefault();
                return marca;
            }

        }

        public MarcaMOD BuscarComCarros(int id)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Marca 
                            WHERE
                                Id = @Id;
                           SELECT * FROM Carro
                            WHERE 
                                MarcaId = @Id";
                using (var resultados = db.QueryMultiple(sql, new { Id = id }))
                {
                    var Marca = resultados.Read<MarcaMOD>().SingleOrDefault();
                    var Carro = resultados.Read<CarroMOD>().ToList();

                    if (Marca != null && Carro != null)
                    {
                        Marca.Carros = Carro;
                    }

                    return Marca;
                }
            }
        }

        public void Cadastrar(MarcaMOD marca)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"INSERT INTO Marca
                            (Nome, CNPJ, DataCriacao)
                              VALUES 
                            (@Nome, @CNPJ, @DataCriacao) SELECT CAST(SCOPE_IDENTITY() as int);";
                int id = db.Query<int>(sql, marca).Single();
                marca.ID = id;
            }
        }

        public void Editar(MarcaMOD marca)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"UPDATE Marca
                                SET
                            Nome = @Nome, 
                            CNPJ = @CNPJ,
                            DataCriacao = @DataCriacao
                              WHERE 
                            Id = @Id";

                int id = db.Query<int>(sql, marca).Single();
                marca.ID = id;
            }
        }

        public IList<MarcaMOD> Listar()
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Marca";
                var lista = db.Query<MarcaMOD>(sql).ToList();
                return lista;
            }
        }
    }

}
