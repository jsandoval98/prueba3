<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/estandar.css" rel="stylesheet" />
    <link href="css/fontawesome.css" rel="stylesheet" />
    
    <script type="text/javascript" src="Scripts/jquery-3.2.1.slim.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.js"></script>
</head>
<body>

    <div class="loading" align="center" style="display: none">
        Procesando...<br />
        Espere por favor<br />
        <img src="../../sls401/images/tenor.gif" width="100px" alt="" />
    </div>

    <form enctype="multipart/form-data" method="post" action="Default.aspx" runat="server">

        <div class="container-fluid">
            <div class="row">

                <div class="col-sm-4">
                    <br />

                    <fieldset style="border: 1px solid #ddd">
                        <legend>Historia  Ejecucion</legend>
                        <table style="width: 100%; margin-bottom: 0px; border-collapse: collapse" class="table table-striped table-bordered">
                            <tr style="height: 20px">
                                <th width="20%">Sociedad</th>
                                <th width="12%">Año</th>
                                <th width="12%">Mes</th>
                                <th width="40%">Fecha Ejecucíon</th>
                                <th width="18%">Estado</th>
                            </tr>
                        </table>
                        <asp:GridView ID="grvHistorico" runat="server" CssClass="table table-striped table-bordered" style="width:100%; border-collapse:collapse" AutoGenerateColumns="False" EnableModelValidation="True" OnPageIndexChanging="grvArchivo1_PageIndexChanging" AllowPaging="True" PageSize="20" ShowHeader="False">
                            <Columns>
                                <asp:BoundField DataField="sociedad" HeaderText="Sociedad" >
                                <ItemStyle Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="anho" HeaderText="Año" >
                                <ItemStyle Width="12%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mes" HeaderText="Mes" >
                                <ItemStyle Width="12%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fecha" HeaderText="Fecha Ejecucion" >
                                <ItemStyle Width="40%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="estado" HeaderText="Estado" >
                                <ItemStyle Width="18%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </fieldset>

                </div>

                <div class="col-sm-8">
                    <br />

                    <fieldset style="border: 1px solid #ddd">
                        <legend>Nueva  Ejecucion</legend>

                        <div class="form-group row">
                            <label class="col-sm-1" for="mes">Sociedad:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlSociedades" runat="server" DataValueField="value" DataTextField="text" CssClass="cajatexto" Style="width: 100%">
                                </asp:DropDownList>
                            </div>

                            <label class="col-sm-1" for="mes">Mes:</label>
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlMes" runat="server" CssClass="cajatexto" Style="width: 100%">
                                    <asp:ListItem Value="1">Enero</asp:ListItem>
                                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                                    <asp:ListItem Value="4">Abril</asp:ListItem>
                                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                                    <asp:ListItem Value="6">Junio</asp:ListItem>
                                    <asp:ListItem Value="7">Julio</asp:ListItem>
                                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                                    <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <label class="col-sm-1" for="anho">Año:</label>
                            <div class="col-sm-2">

                                <asp:DropDownList ID="ddlAnho" runat="server" DataValueField="value" DataTextField="text" CssClass="cajatexto" Style="width: 100%">
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-1">

                                <asp:Button ID="btnProcesar" runat="server" Text="Procesar" CssClass="btn"
                                    OnClientClick="return Procesar()" OnClick="btnProcesar_Click" />

                            </div>
                        </div>

                        <div class="form-group row">
                            <label class="col-sm-1" for="mes">Archivo:</label>
                            <div class="col-sm-5">
                                <input type="file" name="file1" class="cajatexto" style="width: 100%">
                            </div>
                            <div class="col-sm-2">
<%--                                <asp:Button ID="btnCargar12" runat="server" Text="Cargar" CssClass="btn" OnClick="btnCargar1_Click"/>--%>
                                <button ID="btnCargar1" runat="server" type="submit" class="btn" value="Cargar" style="width:40px" title="Cargar Archivo"  onserverclick="btnCargar_Click">
                                    <i class="fa fa-upload"></i>
                                </button>
                                <button runat="server" type="submit" class='btn' value="Limpiar" style="width: 40px; margin-left: 5px" title="Eliminar Carga" onserverclick="btnLimpiar_Click">
                                    <i class="fa fa-trash-alt"></i>
                                </button>
                            </div>                            
                            <label class="col-sm-1" for="listar">Listar:</label>
                            <div id="listar" class="col-sm-2">
                                <asp:DropDownList ID="ddlListar" runat="server" CssClass="cajatexto" Style="width: 70px" AutoPostBack="true" OnSelectedIndexChanged="ddlListar_SelectedIndexChanged">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            
<%--                            <div class="col-sm-2">
                                <asp:Button ID="btnLimpiar1" runat="server" Text="Limpiar" CssClass="btn" OnClick="btnLimpiar1_Click" />
                            </div>--%>

                        </div>


                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab1">Pestaña 1</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab2">Pestaña 2</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab3">Pestaña 3</a>
                            </li>
                        </ul>

                        <!-- Tab panes -->
                        <div class="tab-content ">
                            <div id="tab1" class="tab-pane active">
                                <br>
                                <h6>Reasignacion Mi Farma</h6>
                                <p>Actualizacion de las ventas para el caso Mi farma.</p>

                                <table style="width: 100%; margin-bottom: 0px; border-collapse: collapse" class="table table-striped table-bordered">
                                    <tr>
                                        <th width="10%">Sociedad</th>
                                        <th width="10%">Año</th>
                                        <th width="10%">Mes</th>
                                        <th width="10%">Oficina</th>
                                        <th width="15%">Atributo FFVV</th>
                                        <th width="15%">Cliente</th>
                                        <th width="10%">Grupo Articulos</th>
                                        <th width="20%">Vendedor</th>
                                    </tr>
                                </table>
                                <asp:GridView ID="grvArchivo1" runat="server" Style="width: 100%; border-collapse: collapse" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" EnableModelValidation="True" OnPageIndexChanging="grvArchivo1_PageIndexChanging" AllowPaging="True" ShowHeader="False">
                                    <Columns>
                                        <asp:BoundField DataField="sociedad" HeaderText="Sociedad" >
                                        <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="anho" HeaderText="Año" >
                                        <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes" HeaderText="Mes" >
                                        <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="oficina" HeaderText="Oficina" >
                                        <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ffvv" HeaderText="Atributo FFVV" >
                                        <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cliente" HeaderText="Cliente" >
                                        <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="grupoarticulos" HeaderText="Grupo Articulos" >
                                        <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vendedor" HeaderText="Vendedor" >
                                        <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div id="tab2" class="tab-pane fade">
                                <br>
                                <h6>Reasignacion Atributos FFVV de Lima</h6>
                                <p>Actualizacion del atributo FFVV en las tablas de ventas</p>

                                <table style="width: 100%; margin-bottom: 0px; border-collapse: collapse" class="table table-striped table-bordered">
                                    <tr>
                                        <th width="15%">Sociedad</th>
                                        <th width="10%">Año</th>
                                        <th width="10%">Mes</th>
                                        <th width="15%">Oficina</th>
                                        <th width="15%">Atributo FFVV</th>
                                        <th width="15%">Cliente</th>
                                        <th width="10%">Vendedor</th>
                                        <th width="10%">Division</th>
                                    </tr>
                                </table>
                                <asp:GridView ID="grvArchivo2" runat="server" Style="width: 100%; border-collapse: collapse" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" EnableModelValidation="True" OnPageIndexChanging="grvArchivo2_PageIndexChanging" AllowPaging="true" PageSize="10" ShowHeader="false">
                                    <Columns>
                                        <asp:BoundField DataField="sociedad" HeaderText="Sociedad">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="anho" HeaderText="Año">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes" HeaderText="Mes">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="oficina" HeaderText="Oficina">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ffvv" HeaderText="Atributo FFVV">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cliente" HeaderText="Cliente">
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vendedor" HeaderText="Vendedor">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="division" HeaderText="Division">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div id="tab3" class="tab-pane fade">
                                <br>
                                <h6>Reasignacion Grupos Articulos 936</h6>
                                <p>Actualizacion de los grupos de articulos en las tablas de ventas.</p>

                                <table style="width: 100%; margin-bottom: 0px; border-collapse: collapse" class="table table-striped table-bordered">
                                    <tr>
                                        <th width="15%">Sociedad</th>
                                        <th width="10%">Año</th>
                                        <th width="10%">Mes</th>
                                        <th width="15%">Atributo FFVV</th>
                                        <th width="20%">Cliente</th>
                                        <th width="20%">Vendedor</th>
                                        <th width="10%">Division</th>
                                    </tr>
                                </table>
                                <asp:GridView ID="grvArchivo3" runat="server" Style="width: 100%; border-collapse: collapse" CssClass="table table-striped table-bordered" AutoGenerateColumns="False" EnableModelValidation="True" OnPageIndexChanging="grvArchivo3_PageIndexChanging" AllowPaging="true" PageSize="10" ShowHeader="false">
                                    <Columns>
                                        <asp:BoundField DataField="sociedad" HeaderText="Sociedad" >
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="anho" HeaderText="Año" >
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mes" HeaderText="Mes" >
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ffvv" HeaderText="Atributo FFVV" >
                                            <ItemStyle Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cliente" HeaderText="Cliente" >
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vendedor" HeaderText="Vendedor" >
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="division" HeaderText="Division" >
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </fieldset>

                </div>
            </div>
        </div>

        <script type="text/javascript">

            $('#myTab a').on('click', function (e) {
                e.preventDefault()
                $(this).tab('show')
            })

            function Procesar() {

                if (confirm('¿Desea Mandar a Procesar?')) {

                    var modal = $('<div />');

                    modal.addClass("modal");

                    $('body').append(modal);

                    var loading = $(".loading");

                    loading.show();

                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);

                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);

                    loading.css({ top: top, left: left, position: 'absolute' });

                    return true;
                }
                else
                    return false;
            }
        </script>


    </form>
</body>
</html>
