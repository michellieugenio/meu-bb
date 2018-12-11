using Fiap03.MOD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fiap03.Web.MVC.Models
{
    public class MarcaModel
    {
        /** Criar a tabela e realizar o CRUD **/

        public int ID { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime DataCriacao { get; set; }
        public string CNPJ { get; set; }

        public IList<CarroModel> Carros { get; set; }

        public MarcaModel(MarcaMOD mod)
        {
            Nome = mod.Nome;
            ID = mod.ID;
            DataCriacao = mod.DataCriacao;
            CNPJ = mod.CNPJ;

            if(mod.Carros != null)
            {
                //Instancia a lista de carroModel
                var lista = new List<CarroModel>();

                //Popula a lista com os carros
                mod.Carros.ToList().ForEach(c => lista.Add(new CarroModel(c)));

                //Associa a lista na propriedade
                Carros = lista;
            }
           
        }

        public MarcaModel()
        {

        }
    }
}