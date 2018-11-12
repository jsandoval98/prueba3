using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLS401.Models
{
    public class Archivo3
    {
        public string sociedad { get; set; }
        public int anho { get; set; }
        public int mes { get; set; }
        public string ffvv { get; set; }
        public string cliente { get; set; }
        public string vendedor { get; set; }
        public string division { get; set; }

        public Archivo3(string sociedad, int anho, int mes, string ffvv, string cliente, string vendedor, string division)
        {
            this.sociedad = sociedad;
            this.anho = anho;
            this.mes = mes;
            this.ffvv = ffvv;
            this.cliente = cliente;
            this.vendedor = vendedor;
            this.division = division;
        }
    }
}