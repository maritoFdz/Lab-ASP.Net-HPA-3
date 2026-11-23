using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace CRUD.Pages
{
    public partial class Index : System.Web.UI.Page
    {
        readonly MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) cargarTabla(); // solo la primera vez
        }

        // read de todos los usuarios activos
        void cargarTabla()
        {
            using (MySqlCommand cmd = new MySqlCommand("sp_load", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvusuarios.DataSource = dt;
                    gvusuarios.DataBind();
                }
                con.Close();
            }
        }

        // manda al crud en modo consulta
        protected void BtnRead_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Response.Redirect("Crud.aspx?id=" + id + "&op=R");
        }

        // manda al crud en modo modificar
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            Response.Redirect("Crud.aspx?id=" + id + "&op=U");
        }

        // delete logico con sp_delete
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string id = ((Button)sender).CommandArgument;
            if (string.IsNullOrEmpty(id)) return;

            using (MySqlCommand cmd = new MySqlCommand("sp_delete", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("_idUsuario", MySqlDbType.Int32).Value = Convert.ToInt32(id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            cargarTabla(); // refresca la tabla
        }
    }
}