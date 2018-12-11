using Fiap03.MOD;
using Fiap03.MOD.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Models
{
    public class CarroModel
    {
        public int Id { get; set; }

        //FK
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        public int Ano { get; set; }
        public bool Esportivo { get; set; }
        public string Placa { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Combustível")]
        public Combustivel? Combustivel { get; set; } // o '?' deixa ele ser null

        //FK
        public DocumentoModel Documento { get; set; }
        public int Renavam { get; set; }

        public CarroModel(CarroMOD mod)
        {
            Id = mod.Id;
            MarcaId = mod.MarcaId;
            Ano = mod.Ano;
            Esportivo = mod.Esportivo;
            Placa = mod.Placa;
            Descricao = mod.Descricao;
            Combustivel = mod.Combustivel;
            Renavam = mod.Renavam;
            Documento = new DocumentoModel(mod.Documento);
        }

        public CarroModel()
        {

        }
    }

    
}