using System;
using System.Collections.Generic;
using System.Web;

public class Historia
{
    public string sociedad { get; set; }

    public int anho { get; set; }
    public int mes { get; set; }

    public DateTime fechaejecucion { get; set; }

    public string fecha
    {
        get
        {
            return fechaejecucion.ToString("dd/MM/yyyy hh:mm");
        }
    }

    public string usuario { get; set; }

    public int estado { get; set; }

    public string sestado
    {
        get
        {
            if (estado == 1) return "Procesado";
            else
                return "error";
        }
    }

    public Historia(string sociedad, int anho, int mes, DateTime fechaejecucion, string usuario, int estado)
    {
        this.sociedad = sociedad;
        this.anho = anho;
        this.mes = mes;
        this.fechaejecucion = fechaejecucion;
        this.usuario = usuario;
        this.estado = estado;
    }


    public Historia(string sociedad, int anho, int mes, string usuario)
    {
        this.sociedad = sociedad;
        this.anho = anho;
        this.mes = mes;
        this.usuario = usuario;
    }
}