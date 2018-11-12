using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLS401.Models
{
    public class Archivo2
    {
        public string sociedad { get; set; }
        public int anho { get; set; }
        public int mes { get; set; }

        public string oficina { get; set; }
        public string ffvv { get; set; }
        public string cliente { get; set; }
        public string vendedor { get; set; }
        public string division { get; set; }

        public Archivo2(string sociedad, int anho, int mes, string oficina, string ffvv, string cliente, string vendedor, string division)
        {
            this.sociedad = sociedad;
            this.anho = anho;
            this.mes = mes;
            this.oficina = oficina;
            this.ffvv = ffvv;
            this.cliente = cliente;
            this.vendedor = vendedor;
            this.division = division;
        }
    }
}