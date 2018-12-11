using Dapper;
using Fiap03.DAL.ConnectionFactories;
using Fiap03.DAL.Interfaces;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Fiap03.DAL.Repositories
{
    public class CarroRepository : ICarroRepository
    {
        
        public CarroMOD Buscar(int id)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Carro AS c INNER JOIN
                           Documento AS d ON c.Renavam = d.Renavam where c.Id = @Id";                         
                var carro = db.Query<CarroMOD, DocumentoMOD, CarroMOD>(sql, (c, doc) => { c.Documento = doc; return c; },
                    new { Id = id }, splitOn: "Renavam,Renavam").FirstOrDefault();
                return carro;
            }
        }

        public IList<CarroMOD> BuscarPorAno(int ano)
        {
            using (IDbConnection db = ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Carro AS C INNER JOIN Documento AS D ON C.Renavam = D.Renavam
                            WHERE C.Ano = @Ano or 0 = @Ano";

                var lista = db.Query<CarroMOD, DocumentoMOD, CarroMOD>(sql, (carro, doc) => { carro.Documento = doc; return carro; },
                    new { Ano = ano },
                    splitOn: "Renavam, Renavam").ToList();
                return lista;                
            }
        }

        public void Cadastrar(CarroMOD carro)
        {
            using (var db = ConnectionFactories.ConnectionFactory.GetConnection())
            {
                using (var txtScope = new TransactionScope())
                {
                    var sqlDoc = @"INSERT INTO Documento (Renavam, Categoria, DataFabricacao)
                    VALUES(@Renavam, @Categoria, @DataFabricacao);";

                    db.Execute(sqlDoc, carro.Documento);

                    //Cadastra o Carro
                    var sqlCar = @"INSERT INTO Carro (MarcaId, Ano, Esportivo, Placa, Combustivel, Descricao, Renavam)
                       VALUES (@MarcaId, @Ano, @Esportivo, @Placa, @Combustivel, @Descricao, @Renavam); 
                       SELECT CAST(SCOPE_IDENTITY() as int);";

                    carro.Renavam = carro.Documento.Renavam;
                    int codigo = db.Query<int>(sqlCar, carro).Single();
                    txtScope.Complete();
                }
            }
        }

        public void Editar(CarroMOD carro)
        {
            using (IDbConnection db = ConnectionFactory.GetConnection())
            {

                var sqlDoc = @"UPDATE Documento
                                    SET 
                                  Renavam = @Renavam,
                                  DataFabricacao = @DataFabricacao,
                                  Categoria = @Categoria
                                    WHERE
                                  Renavam = @Renavam";

                db.Execute(sqlDoc, carro.Documento);

                string sql = @"UPDATE Carro
                                    SET 
                                MarcaId = @MarcaId,
                                Ano = @Ano,
                                Esportivo = @Esportivo,
                                Placa = @Placa,
                                Combustivel = @Combustivel,
                                Descricao = @Descricao
                                    WHERE 
                                Id = @Id";

                db.Execute(sql, carro);
            }
        }

        public void excluir(int codigo)
        {
            using (IDbConnection db = ConnectionFactory.GetConnection())
            {
                db.Execute("DELETE From Carro WHERE Id = @Id", new { Id = codigo });
            }
        }

        public IList<CarroMOD> Listar()
        {
            using (var db = ConnectionFactory.GetConnection())
            {
                var sql = @"SELECT * FROM Carro";
                var lista = db.Query<CarroMOD>(sql).ToList();
                return lista;
            }
        }
    }
}

