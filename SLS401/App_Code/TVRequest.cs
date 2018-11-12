using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TVRequest
/// </summary>
public class TVRequest
{
    public string server { get; set; }
    public string user { get; set; }
    public string pwd { get; set; }
    public string sociedad { get; set; }
    public int anho { get; set; }
    public int mes { get; set; }
    public string loginuser { get; set; }


    public TVRequest()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}