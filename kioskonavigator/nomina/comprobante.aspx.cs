﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using kioskotem.wcfoperadora ;

namespace kioskotem.nomina
{
    public partial class comprobante : System.Web.UI.Page
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

                dtpinicio.SelectedDate = DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                dtpfinal.SelectedDate = DateTime.Parse(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);


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
                    string carpetab = ((Label)dtgnominas.Rows[id].FindControl("lblb")).Text;
                    string path;
                    string sa = ((Label)dtgnominas.Rows[id].FindControl("lbldpagosa")).Text;

                    if (carpetab != "")
                    {
                        Session["ruta"] = "pagosnSOVER/" + carpetab + "/" + sa + ".pdf";
                        path = Server.MapPath("../pagosnSOVER/") + carpetab + "\\" + sa + ".pdf";
                        String path2 = "../pagosnSOVER/" + carpetab + "/" + sa + ".pdf";
                    }
                    else
                    {

                        Session["ruta"] = "pagosnSOVER/" + sa + ".pdf";
                        path = Server.MapPath("../pagosnSOVER") + "\\" + sa + ".pdf";
                        String path2 = "../pagosnSOVER/" + sa + ".pdf";

                    }
                  
                    Session["archivo"] = sa + ".pdf";
                    System.IO.FileInfo toDownload = new System.IO.FileInfo(path);
                    ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert(" + path+ ");", true);
                    if (toDownload.Exists)
                    {
                        Response.Redirect("../descargar.aspx", false);
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + path2 + "','_blank')", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('No se encuentra el archivo ');", true);

                    }

                }
                if (e.CommandName == "Delete")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    string carpetab = ((Label)dtgnominas.Rows[id].FindControl("lblb")).Text;
                    string path;
                    string sindicato = ((Label)dtgnominas.Rows[id].FindControl("lbldpagosin")).Text;


                    if (carpetab != "")
                    {
                        Session["ruta"] = "pagosnSOVER/" + carpetab + "/" + sindicato + ".pdf";
                        path = Server.MapPath("../pagosnSOVER/") + carpetab + "\\" + sindicato + ".pdf";
                        String path2 = "../pagosnSOVER/" + carpetab + "/" + sindicato + ".pdf";
                    }
                    else 
                    {
                        Session["ruta"] = "pagosnSOVER/" + sindicato + ".pdf";
                        path = Server.MapPath("../pagosnSOVER") + "\\" + sindicato + ".pdf";
                        String path2 = "../pagosnSOVER/" + sindicato + ".pdf";
                    }
                    

                    Session["archivo"] = sindicato + ".pdf";

                    System.IO.FileInfo toDownload = new System.IO.FileInfo(path);
                    if (toDownload.Exists)
                    {
                        Response.Redirect("../descargar.aspx");
                        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + path2 + "','_blank')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('No se encuentra el archivo ');", true);

                    }

                }
            }
            catch (Exception EX)
            {
                //clFunciones.JQMensaje(this, EX.Message.Replace("'", ""), TipoMensaje.Error);
            }
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

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Cookies.Clear();

            cargar_grid();
        }


        private void cargar_grid()
        {
            IsvcOperadoraMxClient Manejador = new IsvcOperadoraMxClient(); 


            DataSet dsEmpresas = new DataSet();
            dsEmpresas.Tables.Add("Tabla");
            dsEmpresas.Tables[0].Columns.Add("iIdPago");
            dsEmpresas.Tables[0].Columns.Add("dpagosa");
            dsEmpresas.Tables[0].Columns.Add("dpagosin");
            dsEmpresas.Tables[0].Columns.Add("Fecha");
            dsEmpresas.Tables[0].Columns.Add("importe");
            dsEmpresas.Tables[0].Columns.Add("importesin");
            dsEmpresas.Tables[0].Columns.Add("nombrenomina");

            DateTime inicio = DateTime.Parse(dtpinicio.SelectedDate.ToString());
            DateTime final = DateTime.Parse(dtpfinal.SelectedDate.ToString());
            string inicial = inicio.Year.ToString() + inicio.Month.ToString("00") + inicio.Day.ToString("00");
            string fin = final.Year.ToString() + final.Month.ToString("00") + final.Day.ToString("00");



            try
            {
                Tabla tbEmpresas = Manejador.getEjecutaStoredProcedure1("getpagosnomina", "1|" + Session["idusuario"].ToString() + "|" + inicial + "|" + fin);
                if (tbEmpresas != null)
                {
                    DataTable dtEmpresas = clFunciones.convertToDatatable(tbEmpresas);
                    for (int x = 0; x < dtEmpresas.Rows.Count; x++)
                    {
                        dsEmpresas.Tables[0].Rows.Add(dtEmpresas.Rows[x]["iIdPago"],
                                                        dtEmpresas.Rows[x]["dpagosa"],
                                                        dtEmpresas.Rows[x]["dpagoasi"],
                                                        DateTime.Parse(dtEmpresas.Rows[x]["Fecha"].ToString().Remove(18)),
                                                        
                                                        //DateTime.Parse(dtEmpresas.Rows[x]["fecha"].ToString()).ToShortDateString(),
                                                        dtEmpresas.Rows[x]["importesa"],
                                                        dtEmpresas.Rows[x]["importeasi"],
                                                         dtEmpresas.Rows[x]["nombrenomina"]);



                    }

                    Session["dsPagos"] = dsEmpresas;
                    dtgnominas.DataSource = dsEmpresas;

                }
                else
                {
                    Session["dsPagos"] = null;
                    dtgnominas.DataSource = null;
                    lblmensaje.Text = "Sin Pagos Recientes";

                }
                dtgnominas.DataBind();

            }
            catch (Exception EX)
            {
                clFunciones.JQMensaje(this, EX.Message.Replace("'", ""), TipoMensaje.Error);
            }

        }
    }
}