﻿using System;
using System.Web.UI;

public static class Messagebox
{
    public static void Show(this Page Page, String Message)
    {
        Page.ClientScript.RegisterStartupScript(
           Page.GetType(),
           "MessageBox",
           "<script language='javascript'>alert('" + Message + "');</script>"
        );
    }

}