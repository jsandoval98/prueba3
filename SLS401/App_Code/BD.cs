using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

using LibreriaQS;
using SLS401.Models;

/// <summary>
/// Descripción breve de Consulta
/// </summary>
public class BD
{

    public static string ConSQL()
    {
        return ConfigurationManager.AppSettings["conexiontv"].ToString();
    }
    public static List<Archivo1> ListarTablaArchivo1()
    {
        List<Archivo1> lista = new List<Archivo1>();

        try
        {
            String strQuery = "SELECT [0COMP_CODE],[0CALYEAR],[0CALMONTH2],[0SALES_OFF],[ZQBCHATFV],[0CUSTOMER],[0MATL_GROUP],[0PERSON] FROM TB_TV_MiFarma";

            using (SqlDataReader dr = SqlHelper.ExecuteReader(ConSQL(), CommandType.Text, strQuery))
            {
                while (dr.Read())
                {
                    string sociedad = dr.GetString(0);
                    int anho = dr.GetInt32(1);
                    int mes = dr.GetInt32(2);
                    string oficina = dr.GetString(3);
                    string ffvv = dr.GetString(4);
                    string cliente = dr.GetString(5);
                    string grupoarticulos = dr.GetString(6);
                    string vendedor = dr.GetString(7);

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
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return lista;
    }
    public static List<Archivo2> ListarTablaArchivo2()
    {
        List<Archivo2> lista = new List<Archivo2>();

        try
        {
            String strQuery = "SELECT [0COMP_CODE],[0CALYEAR],[0CALMONTH2],[0SALES_OFF],[ZQBCHATFV],[0CUSTOMER],[0PERSON],[ZQBCHDIVS] FROM TB_TV_ffvv_Lima";

            using (SqlDataReader dr = SqlHelper.ExecuteReader(ConSQL(), CommandType.Text, strQuery))
            {
                while (dr.Read())
                {
                    string sociedad = dr.GetString(0);
                    int anho = dr.GetInt32(1);
                    int mes = dr.GetInt32(2);
                    string oficina = dr.GetString(3);
                    string ffvv = dr.GetString(4);
                    string cliente = dr.GetString(5);
                    string vendedor = dr.GetString(6);
                    string division = dr.GetString(7);

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
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return lista;
    }

    public static List<Archivo3> ListarTablaArchivo3()
    {
        List<Archivo3> lista = new List<Archivo3>();

        try
        {
            String strQuery = "SELECT [0COMP_CODE],[0CALYEAR],[0CALMONTH2],[ZQBCHATFV],[0CUSTOMER],[0PERSON],[ZQBCHDIVS] FROM TB_TV_Linea_936";

            using (SqlDataReader dr = SqlHelper.ExecuteReader(ConSQL(), CommandType.Text, strQuery))
            {
                while (dr.Read())
                {
                    string sociedad = dr.GetString(0);
                    int anho = dr.GetInt32(1);
                    int mes = dr.GetInt32(2);
                    string ffvv = dr.GetString(3);
                    string cliente = dr.GetString(4);
                    string vendedor = dr.GetString(5);
                    string division = dr.GetString(6);

                    lista.Add(new Archivo3(
                        sociedad,
                        anho,
                        mes,
                        ffvv,
                        cliente,
                        vendedor,
                        division));
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return lista;
    }

    public static void LimpiaTablaArchivo1()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();

                String strQuery_ = "delete from TB_TV_MiFarma";

                using (SqlCommand cmd = new SqlCommand(strQuery_, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void LimpiaTablaArchivo2()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();

                String strQuery_ = "delete from TB_TV_ffvv_Lima";

                using (SqlCommand cmd = new SqlCommand(strQuery_, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void LimpiaTablaArchivo3()
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();

                String strQuery_ = "delete from TB_TV_Linea_936";

                using (SqlCommand cmd = new SqlCommand(strQuery_, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void RegistraTablaArchivo1(List<Archivo1> lista)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();

                foreach (Archivo1 a in lista)
                {
                    String formato = "insert into TB_TV_MiFarma([0COMP_CODE],[0CALYEAR],[0CALMONTH2],[0SALES_OFF],[ZQBCHATFV],[0CUSTOMER],[0MATL_GROUP],[0PERSON]) values('{0}', {1}, {2},'{3}','{4}','{5}','{6}','{7}')";

                    String strQuery_ = string.Format(formato, a.sociedad, a.anho, a.mes, a.oficina, a.ffvv, a.cliente, a.grupoarticulos, a.vendedor);

                    using (SqlCommand cmd = new SqlCommand(strQuery_, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void RegistraTablaArchivo2(List<Archivo2> lista)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();

                foreach (Archivo2 a in lista)
                {
                    String formato = "insert into TB_TV_ffvv_Lima([0COMP_CODE],[0CALYEAR],[0CALMONTH2],[0SALES_OFF],[ZQBCHATFV],[0CUSTOMER],[0PERSON],[ZQBCHDIVS]) values('{0}', {1}, {2},'{3}','{4}','{5}','{6}','{7}')";

                    String strQuery_ = string.Format(formato, a.sociedad, a.anho, a.mes, a.oficina, a.ffvv, a.cliente, a.vendedor, a.division);

                    using (SqlCommand cmd = new SqlCommand(strQuery_, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void RegistraTablaArchivo3(List<Archivo3> lista)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();

                foreach (Archivo3 a in lista)
                {
                    String formato = "insert into TB_TV_Linea_936([0COMP_CODE],[0CALYEAR],[0CALMONTH2],[ZQBCHATFV],[0CUSTOMER],[0PERSON],[ZQBCHDIVS]) values('{0}', {1}, {2},'{3}','{4}','{5}','{6}')";

                    String strQuery_ = string.Format(formato, a.sociedad, a.anho, a.mes, a.ffvv, a.cliente, a.vendedor, a.division);

                    using (SqlCommand cmd = new SqlCommand(strQuery_, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static List<Historia> ListarHistorico()
    {
        List<Historia> lista = new List<Historia>();

        try
        {
            String strQuery = "SELECT [0COMP_CODE],[MONTH2],[YEAR],[EXECUTIONDATE],[EXECUTIONUSER],[ESTADO] FROM TB_TV_Historia order by [EXECUTIONDATE] desc ";

            using (SqlDataReader dr = SqlHelper.ExecuteReader(ConSQL(), CommandType.Text, strQuery))
            {
                while (dr.Read())
                {
                    string sociedad = dr.GetString(0);
                    int anho = dr.GetInt32(1);
                    int mes = dr.GetInt32(2);
                    DateTime fechaejeccuion = dr.GetDateTime(3);
                    string usuario = dr.GetString(4);
                    int estado = dr.GetInt32(5);
                

                    lista.Add(new Historia(
                        sociedad,
                        anho,
                        mes,
                        fechaejeccuion,
                        usuario,
                        estado));
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return lista;
    }

    public static void RegistraTablaHistorico(Historia a)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("registrar_historico", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@sociedad", SqlDbType.Char, 4).Value = a.sociedad;
                    cmd.Parameters.Add("@mes", SqlDbType.Int, 10).Value = a.mes;
                    cmd.Parameters.Add("@anho", SqlDbType.Int, 10).Value = a.anho;
                    cmd.Parameters.Add("@usuario", SqlDbType.Char, 20).Value = a.usuario;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static void ProcesarTraladoVenta(TVRequest a)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConSQL()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("usp_facturacion_traslado_ventas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@server", SqlDbType.VarChar, 20).Value = a.server;
                    cmd.Parameters.Add("@user", SqlDbType.Int, 10).Value = a.user;
                    cmd.Parameters.Add("@pwd", SqlDbType.Int, 10).Value = a.pwd;
                    cmd.Parameters.Add("@ffactura_month", SqlDbType.Int).Value = a.mes;
                    cmd.Parameters.Add("@ffactura_year", SqlDbType.Int).Value = a.anho;
                    cmd.Parameters.Add("@ffactura_sociedad", SqlDbType.Char, 20).Value = a.sociedad;
                    cmd.Parameters.Add("@loginuser", SqlDbType.Char, 20).Value = a.loginuser;

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public static List<Item> ListarSociedades(string usuario)
    {
        List<Item> lista = new List<Item>();

        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["conexion"]))
            {
                con.Open();

                using (SqlCommand cmdSociedad = new SqlCommand("sp_acp030_obtener_sociedades", con))
                {
                    cmdSociedad.CommandType = CommandType.StoredProcedure;
                    cmdSociedad.Parameters.Add("@codlog", SqlDbType.VarChar, 20).Value = usuario;

                    using (SqlDataReader dr = cmdSociedad.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string soc = dr["codsoc"].ToString().Trim();
                            string nomsoc= dr["nomsoc"].ToString().Trim();
                            Item item = new Item(soc, nomsoc);
                            lista.Add(item);
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            throw new Exception(ex.Message);
        }

        return lista;
    }


    public static List<Item> ListarAnhos()
    {
        int i = 2018;
        int fin = 0;

        if (DateTime.Now.Month >= 11)
            fin = DateTime.Now.Year + 1;
        else
            fin = DateTime.Now.Year;

        List<Item> lista = new List<Item>();

        while(i <= fin)
        {            
            lista.Add(new Item(i.ToString(), i.ToString()));
            i++;
        }

        return lista;
    }
}