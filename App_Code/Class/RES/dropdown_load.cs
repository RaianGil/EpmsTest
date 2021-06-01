using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace UtilitiesComponents.RES
{
    public class dropdown_load
    {
        public dropdown_load()
        {
        }

        public static void load_dropdown_table(DropDownList dll_input, string str_query, string str_column_show, string str_colum_return, string str_message_default)
        {
            DataTable dtTabla = new DataTable();
            string srtConfiguracion = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
            using (var sqlconConexion = new SqlConnection(srtConfiguracion))
            using (var sqlcoCommand = new SqlCommand(str_query, sqlconConexion))
            using (var sqldaDataAdapter = new SqlDataAdapter(sqlcoCommand))
            {
                //sqlcoCommand.CommandType = CommandType.TableDirect;
                //sqlcoCommand.Parameters.AddWithValue("@isComercial", 1);
                //sqlcoCommand.Parameters.Clear();
                sqldaDataAdapter.Fill(dtTabla);
            }
            List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>();
            items.Add(new System.Web.UI.WebControls.ListItem(dtTabla.Columns[0].ToString(), dtTabla.Columns[0].ToString()));
            dll_input.DataSource = dtTabla;
            dll_input.DataTextField = str_column_show;
            dll_input.DataValueField = str_colum_return;
            dll_input.DataBind();
            dll_input.SelectedIndex = -1;
            dll_input.Items.Insert(0, str_message_default);
        }

        public void load_dropdown(DropDownList dll_input, string str_query, string str_column_show, string str_colum_return, string str_message_default)
        {
            DataTable dtTabla = new DataTable();
            string srtConfiguracion = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
            using (var sqlconConexion = new SqlConnection(srtConfiguracion))
            using (var sqlcoCommand = new SqlCommand(str_query, sqlconConexion))
            using (var sqldaDataAdapter = new SqlDataAdapter(sqlcoCommand))
            {
                //sqlcoCommand.CommandType = CommandType.TableDirect;
                //sqlcoCommand.Parameters.AddWithValue("@isComercial", 1);
                //sqlcoCommand.Parameters.Clear();
                sqldaDataAdapter.Fill(dtTabla);
            }
            List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>();
            items.Add(new System.Web.UI.WebControls.ListItem(dtTabla.Columns[0].ToString(), dtTabla.Columns[0].ToString()));
            dll_input.DataSource = dtTabla;
            dll_input.DataTextField = str_column_show;
            dll_input.DataValueField = str_colum_return;
            dll_input.DataBind();
            dll_input.SelectedIndex = -1;
            dll_input.Items.Insert(0, str_message_default);
        }

    }
}