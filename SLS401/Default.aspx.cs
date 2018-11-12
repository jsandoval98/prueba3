using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using NPOI.SS.UserModel;
using SLS401.Models;
using LibreriaQS.Seguridad;
using System.Web.Security;
using System.Configuration;

public partial class _Default : Page
{
    private int filainicio = 2;

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidaQueryPage obj = new ValidaQueryPage();

        if (!IsPostBack)
        {
            if (obj.Validaciones(this))
            {
                if (Session["usuario"] != null && String.Compare(Session["usuario"].ToString(), ValidaQueryPage.codlog) == 0)
                {
                    FormsAuthentication.RedirectFromLoginPage(ValidaQueryPage.codlog, false);
                    //Response.Redirect("ASL_01.aspx");
                }
                else
                {
                    try
                    {
                        Session["usuario"] = ValidaQueryPage.codlog;
                        Session["stUser"] = ValidaQueryPage.codlog;
                        Session["stPass"] = ValidaQueryPage.clave;
                        Session["codlog"] = ValidaQueryPage.codlog;
                        Session["codsoc"] = ValidaQueryPage.codsoc;

                        Session.Timeout = 150;


                        //Credenciales Validas	y le doy autorizacion para entrar a las paginas	            
                        //FormsAuthentication.RedirectFromLoginPage(ValidaQueryPage.codlog, false);
                        ////Response.Redirect("ASL_01.aspx");
                    }
                    catch (Exception ex)
                    {
                        string error = ex.Message;
                        //Session["nomusu"] = "";
                        //Session["apeusu"] = "";
                        //Session["codlog"] = "";
                        LibreriaQS.Email.Mail.SendMail(ex, this, Session["codlog"], "");
                    }
                    //Response.Redirect("ASL_01.aspx");
                }
            }
            else
                Response.Redirect("/extranet");
        }

        ddlAnho.DataSource = BD.ListarAnhos();
        ddlAnho.DataBind();

        ddlMes.SelectedValue = DateTime.Now.Month.ToString();
        ddlAnho.SelectedValue = DateTime.Now.Year.ToString();


        grvHistorico.DataSource = BD.ListarHistorico();
        grvHistorico.DataBind();

        string usuario = Session["usuario"].ToString();
        string sociedad = Session["codsoc"].ToString();


        ddlSociedades.DataSource = BD.ListarSociedades(usuario);
        ddlSociedades.SelectedValue = sociedad;
        ddlSociedades.DataBind();
    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        HttpPostedFile file = Request.Files["file1"];

        if (file != null && file.ContentLength == 0)
        {
            Messagebox.Show(this, "Seleccione el archivo excel a cargar");
            return;
        }

        if (file!= null && file.ContentLength > 0)
        {
            string fname = Path.GetFileName(file.FileName);
            //file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));

            Stream stream2 = new MemoryStream();
            CopyStream(file.InputStream, stream2);

            bool valido = ValidaArchivo(file);

            if (valido)
            {
                IWorkbook workbook = WorkbookFactory.Create(stream2);
                ISheet sheet1 = workbook.GetSheetAt(0);
                ISheet sheet2 = workbook.GetSheetAt(1);
                ISheet sheet3 = workbook.GetSheetAt(2);

                RegistrarPestana1(sheet1);
                RegistrarPestana2(sheet2);
                RegistrarPestana3(sheet3);
            }
        }

    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        BD.LimpiaTablaArchivo1();
        BD.LimpiaTablaArchivo2();
        BD.LimpiaTablaArchivo3();

        grvArchivo1.DataSource = null;
        grvArchivo2.DataSource = null;
        grvArchivo3.DataSource = null;
        grvArchivo1.DataBind();
        grvArchivo2.DataBind();
        grvArchivo3.DataBind();
    }

    public static void CopyStream(Stream input, Stream output)
    {
        byte[] buffer = new byte[16 * 1024];
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, read);
        }
    }

    private bool ValidaArchivo(HttpPostedFile file)
    {
        if (file == null)
        {
            Messagebox.Show(this, "No se pudo leer el archivo.");
            return false;
        }

        try
        {
            Stream stream = file.InputStream;
            IWorkbook workbook = WorkbookFactory.Create(stream);
            ISheet sheet = workbook.GetSheetAt(0);
        }
        catch (Exception ex)
        {
            Messagebox.Show(this, "El archivo no es un archivo excel xls o xlsx valido");
            return false;
        }

        return true;

    }

    public void RegistrarPestana1(ISheet sheet)
    {
        List<Archivo1> lista = new List<Archivo1>();

        for (int row = filainicio; row <= sheet.LastRowNum; row++)
        {
            if (sheet.GetRow(row) != null)
            {

                try
                {
                    string sociedad = "";

                    if (sheet.GetRow(row).GetCell(0).CellType == CellType.Numeric)
                        sociedad = sheet.GetRow(row).GetCell(0).NumericCellValue.ToString().PadLeft(4, '0');
                    else
                        sociedad = sheet.GetRow(row).GetCell(0).StringCellValue;

                    int anho = 0;
                    if (sheet.GetRow(row).GetCell(1).CellType == CellType.Numeric)
                        anho = (int)sheet.GetRow(row).GetCell(1).NumericCellValue;
                    else
                        anho = Convert.ToInt32(sheet.GetRow(row).GetCell(1).StringCellValue);

                    int mes = 0;
                    if (sheet.GetRow(row).GetCell(2).CellType == CellType.Numeric)
                        mes = (int)sheet.GetRow(row).GetCell(2).NumericCellValue;
                    else
                        mes = Convert.ToInt32(sheet.GetRow(row).GetCell(2).StringCellValue);

                    string oficina = "";
                    if (sheet.GetRow(row).GetCell(3).CellType == CellType.Numeric)
                        oficina = sheet.GetRow(row).GetCell(3).NumericCellValue.ToString().PadLeft(4, '0');
                    else
                        oficina = sheet.GetRow(row).GetCell(3).StringCellValue;

                    string ffvv = "";
                    if (sheet.GetRow(row).GetCell(4).CellType == CellType.Numeric)
                        ffvv = sheet.GetRow(row).GetCell(4).NumericCellValue.ToString().PadLeft(3, '0');
                    else
                        ffvv = sheet.GetRow(row).GetCell(4).StringCellValue;

                    string cliente = "";
                    if (sheet.GetRow(row).GetCell(5).CellType == CellType.Numeric)
                        cliente = sheet.GetRow(row).GetCell(5).NumericCellValue.ToString().PadLeft(10, '0');
                    else
                        cliente = sheet.GetRow(row).GetCell(5).StringCellValue;

                    string grupoarticulos = "";
                    if (sheet.GetRow(row).GetCell(6).CellType == CellType.Numeric)
                        grupoarticulos = sheet.GetRow(row).GetCell(6).NumericCellValue.ToString().ToUpper().PadLeft(3, '0');
                    else
                        grupoarticulos = sheet.GetRow(row).GetCell(6).StringCellValue.ToUpper();


                    string vendedor = "";
                    if (sheet.GetRow(row).GetCell(7).CellType == CellType.Numeric)
                        vendedor = sheet.GetRow(row).GetCell(7).NumericCellValue.ToString().PadLeft(8, '0');
                    else
                        vendedor = sheet.GetRow(row).GetCell(7).StringCellValue;

                    lista.Add(new Archivo1(
                        sociedad,
                        anho,
                        mes,
                        oficina,
                        ffvv,
                        cliente,
                        grupoarticulos,
                        vendedor));

                }
                catch (Exception ex)
                {
                    Messagebox.Show(this, string.Format("El registro {0} tiene errores de formato", row));
                    return;
                }
            }
        }


        //Si todo esta bien lo registra en la bd.
        BD.LimpiaTablaArchivo1();
        BD.RegistraTablaArchivo1(lista);

        grvArchivo1.PageSize = Convert.ToInt32(ddlListar.SelectedValue);
        grvArchivo1.PageIndex = 0;
        grvArchivo1.DataSource = BD.ListarTablaArchivo1();
        grvArchivo1.DataBind();
    }

    public void RegistrarPestana2(ISheet sheet)
    {
        List<Archivo2> lista = new List<Archivo2>();

        for (int row = filainicio; row <= sheet.LastRowNum; row++)
        {
            if (sheet.GetRow(row) != null)
            {
                try
                {
                    string sociedad = "";

                    if (sheet.GetRow(row).GetCell(0).CellType == CellType.Numeric)
                        sociedad = sheet.GetRow(row).GetCell(0).NumericCellValue.ToString().PadLeft(4, '0');
                    else
                        sociedad = sheet.GetRow(row).GetCell(0).StringCellValue;

                    int anho = 0;
                    if (sheet.GetRow(row).GetCell(1).CellType == CellType.Numeric)
                        anho = (int)sheet.GetRow(row).GetCell(1).NumericCellValue;
                    else
                        anho = Convert.ToInt32(sheet.GetRow(row).GetCell(1).StringCellValue);

                    int mes = 0;
                    if (sheet.GetRow(row).GetCell(2).CellType == CellType.Numeric)
                        mes = (int)sheet.GetRow(row).GetCell(2).NumericCellValue;
                    else
                        mes = Convert.ToInt32(sheet.GetRow(row).GetCell(2).StringCellValue);

                    string oficina = "";
                    if (sheet.GetRow(row).GetCell(3).CellType == CellType.Numeric)
                        oficina = sheet.GetRow(row).GetCell(3).NumericCellValue.ToString().PadLeft(4, '0');
                    else
                        oficina = sheet.GetRow(row).GetCell(3).StringCellValue;

                    string ffvv = "";
                    if (sheet.GetRow(row).GetCell(4).CellType == CellType.Numeric)
                        ffvv = sheet.GetRow(row).GetCell(4).NumericCellValue.ToString().PadLeft(3, '0');
                    else
                        ffvv = sheet.GetRow(row).GetCell(4).StringCellValue;

                    string cliente = "";
                    if (sheet.GetRow(row).GetCell(5).CellType == CellType.Numeric)
                        cliente = sheet.GetRow(row).GetCell(5).NumericCellValue.ToString().PadLeft(10, '0');
                    else
                        cliente = sheet.GetRow(row).GetCell(5).StringCellValue;

                    string vendedor = "";
                    if (sheet.GetRow(row).GetCell(6).CellType == CellType.Numeric)
                        vendedor = sheet.GetRow(row).GetCell(6).NumericCellValue.ToString().PadLeft(8, '0');
                    else
                        vendedor = sheet.GetRow(row).GetCell(6).StringCellValue;

                    string division = "";
                    if (sheet.GetRow(row).GetCell(7).CellType == CellType.Numeric)
                        division = sheet.GetRow(row).GetCell(7).NumericCellValue.ToString().ToUpper().PadLeft(2, '0');
                    else
                        division = sheet.GetRow(row).GetCell(7).StringCellValue.ToUpper();


                    lista.Add(new Archivo2(
                                    sociedad,
                                    anho,
                                    mes,
                                    oficina,
                                    ffvv,
                                    cliente,
                                    vendedor,
                                    division));
                }
                catch (Exception ex)
                {
                    Messagebox.Show(this, string.Format("El registro {0} tiene errores de formato", row));
                    return;
                }

            }
        }

        //Si todo esta bien lo registra en la bd.
        BD.LimpiaTablaArchivo2();
        BD.RegistraTablaArchivo2(lista);

        grvArchivo2.PageSize = Convert.ToInt32(ddlListar.SelectedValue);
        grvArchivo2.PageIndex = 0;
        grvArchivo2.DataSource = BD.ListarTablaArchivo2();
        grvArchivo2.DataBind();
    }

    public void RegistrarPestana3(ISheet sheet)
    {
        List<Archivo3> lista = new List<Archivo3>();

        for (int row = filainicio; row <= sheet.LastRowNum; row++)
        {
            if (sheet.GetRow(row) != null)
            {
                try
                {
                    string sociedad = "";

                    if (sheet.GetRow(row).GetCell(0).CellType == CellType.Numeric)
                        sociedad = sheet.GetRow(row).GetCell(0).NumericCellValue.ToString().PadLeft(4, '0');
                    else
                        sociedad = sheet.GetRow(row).GetCell(0).StringCellValue;

                    int anho = 0;
                    if (sheet.GetRow(row).GetCell(1).CellType == CellType.Numeric)
                        anho = (int)sheet.GetRow(row).GetCell(1).NumericCellValue;
                    else
                        anho = Convert.ToInt32(sheet.GetRow(row).GetCell(1).StringCellValue);

                    int mes = 0;
                    if (sheet.GetRow(row).GetCell(2).CellType == CellType.Numeric)
                        mes = (int)sheet.GetRow(row).GetCell(2).NumericCellValue;
                    else
                        mes = Convert.ToInt32(sheet.GetRow(row).GetCell(2).StringCellValue);


                    string ffvv = "";
                    if (sheet.GetRow(row).GetCell(3).CellType == CellType.Numeric)
                        ffvv = sheet.GetRow(row).GetCell(3).NumericCellValue.ToString().PadLeft(3, '0');
                    else
                        ffvv = sheet.GetRow(row).GetCell(3).StringCellValue;

                    string cliente = "";
                    if (sheet.GetRow(row).GetCell(4).CellType == CellType.Numeric)
                        cliente = sheet.GetRow(row).GetCell(4).NumericCellValue.ToString().PadLeft(10, '0');
                    else
                        cliente = sheet.GetRow(row).GetCell(4).StringCellValue;

                    string vendedor = "";
                    if (sheet.GetRow(row).GetCell(5).CellType == CellType.Numeric)
                        vendedor = sheet.GetRow(row).GetCell(5).NumericCellValue.ToString().PadLeft(8, '0');
                    else
                        vendedor = sheet.GetRow(row).GetCell(5).StringCellValue;

                    string division = "";
                    if (sheet.GetRow(row).GetCell(6).CellType == CellType.Numeric)
                        division = sheet.GetRow(row).GetCell(6).NumericCellValue.ToString().ToUpper().PadLeft(2, '0');
                    else
                        division = sheet.GetRow(row).GetCell(6).StringCellValue.ToUpper();


                    lista.Add(new Archivo3(
                            sociedad,
                            anho,
                            mes,
                            ffvv,
                            cliente,
                            vendedor,
                            division));
                }
                catch (Exception ex)
                {
                    Messagebox.Show(this, string.Format("El registro {0} tiene errores de formato", row));
                    return;
                }
            }
        }

        //Si todo esta bien lo registra en la bd.
        BD.LimpiaTablaArchivo3();
        BD.RegistraTablaArchivo3(lista);

        grvArchivo3.PageSize = Convert.ToInt32(ddlListar.SelectedValue);
        grvArchivo3.PageIndex = 0;
        grvArchivo3.DataSource = BD.ListarTablaArchivo3();
        grvArchivo3.DataBind();
    }

    protected void grvArchivo1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvArchivo1.PageIndex = e.NewPageIndex;
        grvArchivo1.DataSource = BD.ListarTablaArchivo1();
        grvArchivo1.DataBind();
    }

    protected void grvArchivo2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvArchivo2.PageIndex = e.NewPageIndex;
        grvArchivo2.DataSource = BD.ListarTablaArchivo2();
        grvArchivo2.DataBind();
    }

    protected void grvArchivo3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvArchivo3.PageIndex = e.NewPageIndex;
        grvArchivo3.DataSource = BD.ListarTablaArchivo3();
        grvArchivo3.DataBind();
    }

    protected void ddlListar_SelectedIndexChanged(object sender, EventArgs e)
    {
        grvArchivo1.PageSize = Convert.ToInt32(ddlListar.SelectedValue);
        grvArchivo1.PageIndex = 0;
        grvArchivo1.DataSource = BD.ListarTablaArchivo1();
        grvArchivo1.DataBind();

        grvArchivo2.PageSize = Convert.ToInt32(ddlListar.SelectedValue);
        grvArchivo2.PageIndex = 0;
        grvArchivo2.DataSource = BD.ListarTablaArchivo2();
        grvArchivo2.DataBind();

        grvArchivo3.PageSize = Convert.ToInt32(ddlListar.SelectedValue);
        grvArchivo3.PageIndex = 0;
        grvArchivo3.DataSource = BD.ListarTablaArchivo3();
        grvArchivo3.DataBind();

    }

    protected void btnProcesar_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);

        if(ddlSociedades.SelectedIndex == -1)
        {
            Messagebox.Show(this, "Seleccione la sociedad");
            return;
        }

        if (ddlMes.SelectedIndex == -1)
        {
            Messagebox.Show(this, "Seleccione el mes");
            return;
        }

        if (ddlAnho.SelectedIndex == -1)
        {
            Messagebox.Show(this, "Seleccione el año");
            return;
        }

        if (grvArchivo1.Rows.Count ==  0 )
        {
            Messagebox.Show(this, "No ha cargado los datos del archivo excel pestaña 1");
            return;
        }

        if (grvArchivo2.Rows.Count == 0)
        {
            Messagebox.Show(this, "No ha cargado los datos del archivo excel pestaña 2.");
            return;
        }

        if (grvArchivo3.Rows.Count == 0)
        {
            Messagebox.Show(this, "No ha cargado los datos del archivo excel pestaña 3.");
            return;
        }

        int anho = Convert.ToInt32(ddlAnho.SelectedValue);
        int mes = Convert.ToInt32(ddlMes.SelectedValue);

        string loginusuario = Session["usuario"].ToString();
        string sociedad = Session["codsoc"].ToString();

        try
        {
            TVRequest req = new TVRequest();
            req.server = ConfigurationManager.AppSettings["server"].ToString();
            req.user = ConfigurationManager.AppSettings["user"].ToString();
            req.pwd = ConfigurationManager.AppSettings["pwd"].ToString();
            req.mes = mes;
            req.anho = anho;
            req.sociedad = sociedad;
            req.loginuser = loginusuario;

            BD.ProcesarTraladoVenta(req);

            grvHistorico.DataSource = BD.ListarHistorico();
            grvHistorico.DataBind();


            grvArchivo1.DataSource = null;
            grvArchivo2.DataSource = null;
            grvArchivo3.DataSource = null;
            grvArchivo1.DataBind();
            grvArchivo2.DataBind();
            grvArchivo3.DataBind();

            Messagebox.Show(this, "El proceso se realizo con exito.");
        }
        catch(Exception ex)
        {
            Messagebox.Show(this, "Sucedio un error durante la ejecucion.");
        }

    }
}