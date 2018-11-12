using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Descripción breve de Item
/// </summary>
public class Item
{
    public string value { get; set; }
    public string text { get; set; }

    public Item(string value, string text)
    {
        this.value = value;
        this.text = text;
    }

}