using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;
using System.Transactions;
using Fiap03.DAL.ConnectionFactories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.DAL.Interfaces;
using Fiap03.DAL.Repositories;
using Fiap03.MOD;

namespace Fiap03.Web.MVC.Controllers
{
    public class CarroController : Controller
    {
        private ICarroRepository _carroRepository = new CarroRepository();
        private IMarcaRepository _marcaRepository = new MarcaRepository();
        // IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString);

        //simular o bd
        //private -> metodos e atributos / n precisa ter um obj // static é da classe e n do obj

        //private static IList<CarroModel> _carros = new List<CarroModel>();

        private IList<String> _marcas = new List<String>() { "ferrari", "jeep", "fiat" };

        [HttpGet]
        public ActionResult Cadastrar()
        {
            //Listar as marcas do BD
            CarregarMarcas();

            return View();
        }

        private void CarregarMarcas()
        {
            var lista = _marcaRepository.Listar();
            ViewBag.marcas = new SelectList(lista, "Id", "Nome");
        }

        [HttpPost]
        public ActionResult Cadastrar(CarroModel carro)
        {

            var mod = new CarroMOD()
            {
                Combustivel = carro.Combustivel,
                Ano = carro.Ano,
                Placa = carro.Placa,
                Descricao = carro.Descricao,
                MarcaId = carro.MarcaId,
                Renavam = carro.Renavam,
                Esportivo = carro.Esportivo,
                Documento = new DocumentoMOD()
                {
                    Categoria = carro.Documento.Categoria,
                    Renavam = carro.Documento.Renavam,
                    DataFabricacao = carro.Documento.DataFabricacao
                }
            };
            _carroRepository.Cadastrar(mod);
            TempData["msg"] = "Cadastrado com Sucesso";
            return RedirectToAction("Listar");
            //using (IDbConnection db = ConnectionFactory.GetConnection())
            //{
            //    using (var txScope = new TransactionScope())
            //    {
            //        //Cadastra o Documento
            //        var sqlDoc = @"INSERT INTO Documento (Renavam, Categoria, DataFabricacao)
            //        VALUES(@Renavam, @Categoria, @DataFabricacao);";

            //        db.Execute(sqlDoc, carro.Documento);

            //        //Cadastra o Carro
            //        var sqlCar = @"INSERT INTO Carro (MarcaId, Ano, Esportivo, Placa, Combustivel, Descricao, Renavam)
            //           VALUES (@MarcaId, @Ano, @Esportivo, @Placa, @Combustivel, @Descricao, @Renavam); 
            //           SELECT CAST(SCOPE_IDENTITY() as int);";

            //        carro.Renavam = carro.Documento.Renavam;
            //        int codigo = db.Query<int>(sqlCar, carro).Single();

            //        TempData["msg"] = "Cadastrado com Sucesso! ID: " + codigo;

            //        txScope.Complete();
            //    }


            //}
            //_carros.Add(carro); //add o carro na lista
            //redirecionando para uma url, cria uma segunda request
            //para abrir a página de resposta
            //f5 n cadastra novamente
            
        }

        [HttpGet]
        public ActionResult Listar()
        {
            //instanciar uma lista de marca model
            var listaModel = new List<CarroModel>();

            //Buscar as marcaMod do banco de dados
            var listaMod = _carroRepository.Listar();

            //converter mod para model
            listaMod.ToList().ForEach(c => listaModel.Add(new CarroModel(c)));
            //retornar a view para lista model
            return View(listaModel);
            //using (IDbConnection db = ConnectionFactory.GetConnection())
            //{
            //    var sql = @"SELECT * FROM Carro AS CAR INNER JOIN Documento AS DC ON CAR.Renavam = DC.Renavam";

            //    var lista = db.Query<CarroModel, DocumentoModel, CarroModel>(sql, (carro, doc) => { carro.Documento = doc; return carro; },
            //        splitOn: "Renavam, Renavam").ToList();

            //    return View(lista);
            //}

            //envia a lista de carros para a view
            //return View();
        }

        [HttpPost]
        public ActionResult Excluir(int codigo)
        {            
           _carroRepository.Buscar(codigo);
            return RedirectToAction("Listar");
        }

        //Abre a tela de edição com o formulário preenchido
        [HttpPost]
        public ActionResult Editar(CarroModel carro)
        {
            //transformar o model para mod
            var mod = new CarroMOD()
            {
                Combustivel = carro.Combustivel,
                Ano = carro.Ano,
                Placa = carro.Placa,
                Descricao = carro.Descricao,
                MarcaId = carro.MarcaId,
                Renavam = carro.Renavam,                
                Esportivo = carro.Esportivo,
                Documento = new DocumentoMOD()
                {
                    Categoria = carro.Documento.Categoria,
                    Renavam = carro.Documento.Renavam,
                    DataFabricacao = carro.Documento.DataFabricacao
                }
            };

            //chamar o metodo de repository para editar
            _carroRepository.Editar(mod);


            TempData["msg"] = "Marca editada com sucesso";
            return RedirectToAction("Listar");
            //using (IDbConnection db = ConnectionFactory.GetConnection())
            //{
            //    using (var txtScope = new TransactionScope())
            //    {
            //        var sqlDoc = @"UPDATE Documento
            //                        SET 
            //                      Renavam = @Renavam,
            //                      DataFabricacao = @DataFabricacao,
            //                      Categoria = @Categoria
            //                        WHERE
            //                      Renavam = @Renavam";

            //        db.Execute(sqlDoc, carro.Documento);

            //        string sql = @"UPDATE Carro
            //                        SET 
            //                    MarcaId = @MarcaId,
            //                    Ano = @Ano,
            //                    Esportivo = @Esportivo,
            //                    Placa = @Placa,
            //                    Combustivel = @Combustivel,
            //                    Descricao = @Descricao
            //                        WHERE 
            //                    Id = @Id";

            //        db.Execute(sql, carro);
            //        TempData["msg"] = "Carro editado com sucesso";

            //        txtScope.Complete();
            //        return RedirectToAction("Listar");

            //    }
            //}
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            //buscar a marca mod no bando de dados pelo id
            var mod = _carroRepository.Buscar(id);
            CarregarMarcas();
            //tranasformar o mod em model
            var model = new CarroModel(mod);
            //retornar a view
            return View(model);

            //using (IDbConnection db = ConnectionFactory.GetConnection())
            //{
            //    var sql = @"SELECT * FROM Carro AS C INNER JOIN Documento AS D ON C.Renavam = D.Renavam 
            //                WHERE C.Id = @Id";

            //    var carro = db.Query<CarroModel, DocumentoModel, CarroModel>(sql, (c, doc) => { c.Documento = doc; return c; },
            //         new { Id = id },
            //         splitOn: "Renavam, Renavam").FirstOrDefault();
            //    CarregarMarcas();
            //    return View(carro);
            //}
        }

        [HttpGet]
        public ActionResult Pesquisar(int ano)
        {
            var listaModel = new List<CarroModel>();

            var listaMod = _carroRepository.BuscarPorAno(ano);

            listaMod.ToList().ForEach(c => listaModel.Add(new CarroModel(c)));
            return View("Listar", listaModel);
           
        }

       


    }

}
