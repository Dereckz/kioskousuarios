using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using kioskotem.wcfoperadora;


namespace kioskotem.juridico
{
    public partial class contratos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objusuario"] == null)
            {
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                Response.Redirect("../default.aspx");
            }
            
            if (!IsPostBack)
            {

               

            }
            
        }

        protected void dtgnominas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dtgnominas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Select")
                {
                    int id = Convert.ToInt32(e.CommandArgument);


                    //DateTime fecha = DateTime.Parse(((Label)dtgnominas.Rows[id].FindControl("lblfecha")).Text);
                    string archivo = ((Label)dtgnominas.Rows[id].FindControl("nombrearchivo")).Text;

                    Session["ruta"] = "contratost/" + archivo;
                    String path = Server.MapPath("../contratost") + "\\" + archivo;
                    String path2 = "../contratost/" + archivo;



                    Session["archivo"] = archivo;

                    System.IO.FileInfo toDownload = new System.IO.FileInfo(path);
                    if (toDownload.Exists)
                    {
                        Response.Redirect("../descargar.aspx", false);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + path2  + "','_blank')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('No se encuentra el archivo ');", true);

                    }

                }


            }
            catch (Exception EX)
            {
                clFunciones.JQMensaje(this, EX.Message.Replace("'", ""), TipoMensaje.Error);
            }
        }


        static public void Download(string patch)
        {
            System.IO.FileInfo toDownload =
                       new System.IO.FileInfo(HttpContext.Current.Server.MapPath(patch));


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                       "attachment; filename=" + toDownload.Name);
            HttpContext.Current.Response.AddHeader("Content-Length",
                       toDownload.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(patch,false);
            HttpContext.Current.Response.End();
        }

        protected void dtgnominas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dtgnominas.DataSource = (DataSet)Session["dsPagos"];
            dtgnominas.PageIndex = e.NewPageIndex;
            dtgnominas.DataBind();
        }

        protected void cmdbuscar_Click(object sender, EventArgs e)
        {
            lblmensaje.Text = "";

            cargar_grid();
        }


        private void cargar_grid(){
            IsvcOperadoraMxClient Manejador = new IsvcOperadoraMxClient();

          
            DataSet dsDocumentos = new DataSet();
            dsDocumentos.Tables.Add("Tabla");
            dsDocumentos.Tables[0].Columns.Add("iIdArchivosAlta");
            dsDocumentos.Tables[0].Columns.Add("cNombreArchivo");
            dsDocumentos.Tables[0].Columns.Add("mes");
            dsDocumentos.Tables[0].Columns.Add("anio");
            dsDocumentos.Tables[0].Columns.Add("fecha");

            try
            {
                Tabla tbDocumentos = Manejador.getEjecutaStoredProcedure1("getArchivosAltaListar", Session["idusuario"].ToString() + "|" + (cbomes.SelectedIndex + 1) + "|" + cboanio.Text);
                if (tbDocumentos != null)
                {
                    DataTable dtDocumentos = clFunciones.convertToDatatable(tbDocumentos);
                    for (int x = 0; x < dtDocumentos.Rows.Count; x++)
                    {
                        dsDocumentos.Tables[0].Rows.Add(dtDocumentos.Rows[x]["iIdArchivosAlta"],
                                                        dtDocumentos.Rows[x]["cNombreArchivo"],
                                                        month(dtDocumentos.Rows[x]["mes"].ToString()),
                                                        dtDocumentos.Rows[x]["anio"],
                                                        DateTime.Parse(dtDocumentos.Rows[x]["fecha"].ToString().Remove(18)).ToShortDateString());
                                                        // DateTime.Parse(dtDocumentos.Rows[x]["fecha"].ToString().Remove(18)),//).ToShortDateString());

                    }

                    Session["dsDocs"] = dsDocumentos;
                    dtgnominas.DataSource = dsDocumentos;

                }
                else
                {
                    Session["dsDocs"] = null;
                    dtgnominas.DataSource = null;
                    lblmensaje.Text = "Sin Documentos";

                }
                dtgnominas.DataBind();

            }
            catch (Exception EX)
            {
                clFunciones.JQMensaje(this, EX.Message.Replace("'", ""), TipoMensaje.Error);
            }

        }

        public String month(String n)
        {
            String mes = "";
            switch (Int32.Parse(n))
            {
                case 1: mes = "Enero"; break;
                case 2: mes = "Febrero"; break;
                case 3: mes = "Marzo"; break;
                case 4: mes = "Abril"; break;
                case 5: mes = "Mayo"; break;
                case 6: mes = "Junio"; break;
                case 7: mes = "Julio"; break;
                case 8: mes = "Agosto"; break;
                case 9: mes = "Septiembre"; break;
                case 10: mes = "Octubre"; break;
                case 11: mes = "Noviembre"; break;
                case 12: mes = "Diciembre"; break;
                default: mes = " "; break;
            }
            return mes;
        }
    }
}