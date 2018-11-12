using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLS401.Models
{
    public class Archivo1
    {
        public string sociedad { get; set; }
        public int anho { get; set; }
        public int mes { get; set; }
        public string oficina { get; set; }
        public string ffvv { get; set; }
        public string cliente { get; set; }
        public string grupoarticulos { get; set; }
        public string vendedor { get; set; }

        public Archivo1(string sociedad, int anho, int mes, string oficina, string ffvv, 
            string cliente, string grupoarticulos, string vendedor)
        {
            this.sociedad = sociedad;
            this.anho = anho;
            this.mes = mes;
            this.oficina = oficina;
            this.ffvv = ffvv;
            this.cliente = cliente;
            this.grupoarticulos = grupoarticulos;
            this.vendedor = vendedor;
        }

    }
}