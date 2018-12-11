using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fiap03.Web.MVC.Models
{
    public class ModeloModel

    {
        public ModeloModel()
        {

        }

        public ModeloModel(ModeloMOD mod)
        {
            Nome = mod.Nome;
            MarcaId = mod.MarcaId;
            Id = mod.Id;
            
        }

        [Display(Name = "Marca")]
        public int MarcaId { get; set; }
        [Display(Name = "Modelo")]
        public string Nome { get; set; }
        public int Id { get; set; }
        public IList<MarcaModel> Marcas { get; set; }
        public IList<ModeloModel> Modelos { get; set; }

        public IEnumerable<SelectListItem> MarcasSelectList => Marcas.Select(c => new SelectListItem() { Value = c.ID.ToString(), Text = c.Nome, Text});
        
    }
}