using Fiap03.DAL.Interfaces;
using Fiap03.DAL.Repositories;
using Fiap03.DAL.Repositories.Interfaces;
using Fiap03.MOD;
using Fiap03.Web.MVC.Models;
using System.Linq;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Controllers
{
    public class ModeloController : Controller       
    {
        private IModeloRepository _modeloRepository = new ModeloRepository();
        private IMarcaRepository _marcaRepository = new MarcaRepository();

        // GET: Modelo
        public ActionResult Index(int id)
        {
            ViewBag.marca = new MarcaModel(_marcaRepository.Buscar(id));
            ViewBag.modelos = _modeloRepository.Listar(id).Select(c => new ModeloModel(c)).ToList();
            return View();
            // Buscar as marcas cadastradas no banco
            var marcasMod = _marcaRepository.Listar();
            // Buscar os modelos cadastrados no banco
            var modelosMod = _modeloRepository.Listar(id);
            // Converter a lista de MarcaMOD para MarcaModel
            var marcasModel = marcasMod.Select(c => new MarcaModel(c)).ToList();
            // Converter a lista de ModeloMOD para ModeloModel
            var modeloModel = modelosMod.Select(c => new ModeloModel(c)).ToList();


            // Retornar a view passando um objeto do tipo ModeloModel que dentro tem uma lista de marcas (MarcaModel)
            return View(new ModeloModel {
                Marcas = marcasModel,
                Modelos = modeloModel
            });
        }

        public ActionResult Cadastrar(ModeloModel modelo)
        {
            //tranformar de model para mod 
            var mod = new ModeloMOD()
            {
                MarcaId = modelo.MarcaId,
                Nome = modelo.Nome
                
            };

            
            _modeloRepository.Cadastrar(mod);
            TempData["msg"] = "Cadastrado com Sucesso";
            return RedirectToAction("Index", new { id = modelo.MarcaId });

        }


    }
}