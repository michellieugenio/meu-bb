using Dapper;
using Fiap03.DAL.Interfaces;
using Fiap03.DAL.Repositories;
using Fiap03.MOD;
using Fiap03.Web.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Controllers
{
    public class MarcaController : Controller
    {
        private IMarcaRepository _marcaRepository = new MarcaRepository();

        
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View(new MarcaModel());
        }

        [HttpPost]
        public ActionResult Cadastrar(MarcaModel marca)
        {
            //tranformar de model para mod 
            var mod = new MarcaMOD()
            {
                CNPJ = marca.CNPJ,
                DataCriacao = marca.DataCriacao,
                Nome = marca.Nome
            };

            //chamar repository cadastar para gravar bd
            _marcaRepository.Cadastrar(mod);
            TempData["msg"] = "Cadastrado com Sucesso";
            return RedirectToAction("Listar");

            //using (IDbConnection db = new SqlConnection(
            //    ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            //{
            //    var sql = @"INSERT INTO Marca (Nome, DataCriacao, 
            //           CNPJ)
            //           VALUES (@Nome, @DataCriacao, @CNPJ); SELECT CAST(SCOPE_IDENTITY() as int);";

            //    int id = db.Query<int>(sql, marca).Single();

            //    TempData["msg"] = "Cadastrado com Sucesso! ID: " + id;
            //}
            //return RedirectToAction("Listar");
        }
        [HttpGet]
        public ActionResult Listar()
        {
            //instanciar uma lista de marca model
            var listaModel = new List<MarcaModel>();

            //Buscar as marcaMod do banco de dados
            var listaMod = _marcaRepository.Listar();

            //converter mod para model
            listaMod.ToList().ForEach(c => listaModel.Add(new MarcaModel(c)));
            //retornar a view para lista model
            return View(listaModel);

            ////retornar a view com a listar de marca model
            //using (IDbConnection db = new SqlConnection(
            //    ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            //{
            //    IList<MarcaModel> lista = db.Query<MarcaModel>("SELECT * FROM Marca").ToList();
            //    return View(lista);
            //}

        }

        [HttpPost]
        public ActionResult Excluir(int codigo)
        {
            using (IDbConnection db = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            {
                db.Execute("DELETE From Marca WHERE id = @ID", new { id = codigo });
            }
            TempData["msg"] = "Marca excluída com sucesso";
            return RedirectToAction("Listar");
        }

        //Abre a tela de edição com o formulário preenchido
        [HttpPost]
        public ActionResult Editar(MarcaModel marca)
        {
            //transformar o model para mod
            var mod = new MarcaMOD()
            {
                
                CNPJ = marca.CNPJ,
                ID = marca.ID,              
                Nome = marca.Nome
            };

            //chamar o metodo de repository para editar
            _marcaRepository.Editar(mod);


            TempData["msg"] = "Marca editada com sucesso";
            return RedirectToAction("Listar");
            //using (IDbConnection db = new SqlConnection(
            //    ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            //{
            //    string sql = @"UPDATE Marca
            //                        SET 
            //                    Nome = @Nome,
            //                    DataCriacao = @DataCriacao,
            //                    CNPJ = @CNPJ
            //                        WHERE 
            //                    Id = @ID";

            //    db.Execute(sql, marca);
            //    TempData["msg"] = "Marca editada com sucesso";
            //    return RedirectToAction("Listar");
            //}
        }
        [HttpGet]
        public ActionResult Editar(int id)
        {
            //buscar a marca mod no bando de dados pelo id
            var mod = _marcaRepository.Buscar(id);
            //tranasformar o mod em model
            var model = new MarcaModel(mod);
            //retornar a view
            return View(model);


            //using (IDbConnection db = new SqlConnection(
            //    ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            //{
            //    var sql = @"SELECT * FROM Marca 
            //                    WHERE Id = @ID";

            //    var marca = db.Query<MarcaModel>(sql, new { Id = id }).FirstOrDefault();
            //    return View(marca);
            //}
        }

        public ActionResult Detalhar(int id)
        {
            //Pesquisa a marca com os carros
            var mod = _marcaRepository.BuscarComCarros(id);
            //transformar mod em model

            var model = new MarcaModel(mod);
            return View(model);

            //using (IDbConnection db = new SqlConnection(
            //   ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString))
            //{
            //    var sql = @"SELECT * FROM Marca 
            //             WHERE
            //              Id = @Id;
            //            SELECT * FROM Carro
            //             WHERE 
            //              MarcaId = @Id";
            //   using (var resultados = db.QueryMultiple(sql, new { Id = id }))
            //    {
            //        var Marca = resultados.Read<MarcaModel>().SingleOrDefault();
            //        var Carro = resultados.Read<CarroModel>().ToList();
            //
            //        if (Marca != null && Carro != null)
            //        {
            //            Marca.Carros = Carro;
            //        }
            //
            //        return View(Marca);
            //    }
            //}
        }
    }
}