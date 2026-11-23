using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace CRUD.Pages
{
    public partial class Crud : System.Web.UI.Page
    {
        readonly MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);
        string sID = "";
        string sOP = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // la url sigue con id y op en los postback asi que se leen siempre
            if (Request.QueryString["id"] != null) sID = Request.QueryString["id"].ToString();
            if (Request.QueryString["op"] != null) sOP = Request.QueryString["op"].ToString();
            if (IsPostBack) return;

            if (sID != "") CargarDatos();

            switch (sOP)
            {
                case "C":
                    lblTitulo.Text = "Crear Usuario";
                    btnCreate.Visible = true;
                    break;
                case "R":
                    lblTitulo.Text = "Consultar Usuario";
                    SoloLectura();
                    break;
                case "U":
                    lblTitulo.Text = "Modificar Usuario";
                    btnUpdate.Visible = true;
                    break;
                default:
                    Response.Redirect("Index.aspx"); // sin operacion no hay nada que hacer aqui
                    break;
            }
        }

        // trae el usuario con sp_read y llena el formulario
        void CargarDatos()
        {
            con.Open();
            using (MySqlDataAdapter da = new MySqlDataAdapter("sp_read", con))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("_idUsuario", MySqlDbType.Int32).Value = Convert.ToInt32(sID);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0) Response.Redirect("Index.aspx"); // el id no existe
                DataRow row = dt.Rows[0];
                txtNombre.Text = row["Nombre"].ToString();
                txtCorreo.Text = row["Correo"].ToString();
                txtEdad.Text = row["Edad"].ToString();
                DateTime d = Convert.ToDateTime(row["FechaNacimiento"]);
                txtFechaNacimiento.Text = d.ToString("yyyy-MM-dd"); // formato que exige el control date
            }
        }

        void SoloLectura()
        {
            txtNombre.ReadOnly = true;
            txtCorreo.ReadOnly = true;
            txtEdad.ReadOnly = true;
            txtFechaNacimiento.ReadOnly = true;
        }

        bool Validar(out int edad, out DateTime fecha)
        {
            edad = 0;
            fecha = DateTime.MinValue;
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text) || !int.TryParse(txtEdad.Text, out edad) || !DateTime.TryParse(txtFechaNacimiento.Text, out fecha))
            {
                Mostrar("Completa todos los campos con datos validos");
                return false;
            }
            return true;
        }

        void Mostrar(string mensaje)
        {
            lblMensaje.Text = Server.HtmlEncode(mensaje);
            lblMensaje.Visible = true;
        }

        // create con insert_user
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int edad;
            DateTime fecha;
            if (!Validar(out edad, out fecha)) return;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("insert_user", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add("_nombre", MySqlDbType.VarChar).Value = txtNombre.Text.Trim();
                    cmd.Parameters.Add("_edad", MySqlDbType.Int32).Value = edad;
                    cmd.Parameters.Add("_correo", MySqlDbType.VarChar).Value = txtCorreo.Text.Trim();
                    cmd.Parameters.Add("_fechanacimiento", MySqlDbType.Date).Value = fecha;
                    cmd.ExecuteNonQuery();
                }
                Response.Redirect("Index.aspx", false);
            }
            catch (MySqlException ex) when (ex.Number == 1062) { Mostrar("Ya existe un usuario con ese correo"); }
            catch (Exception ex) { Mostrar("Error: " + ex.Message); }
            finally { con.Close(); }
        }

        // update con sp_update
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int edad;
            DateTime fecha;
            if (!Validar(out edad, out fecha)) return;
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("sp_update", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.Add("_idUsuario", MySqlDbType.Int32).Value = Convert.ToInt32(sID);
                    cmd.Parameters.Add("_nombre", MySqlDbType.VarChar).Value = txtNombre.Text.Trim();
                    cmd.Parameters.Add("_edad", MySqlDbType.Int32).Value = edad;
                    cmd.Parameters.Add("_correo", MySqlDbType.VarChar).Value = txtCorreo.Text.Trim();
                    cmd.Parameters.Add("_fechanacimiento", MySqlDbType.Date).Value = fecha;
                    cmd.ExecuteNonQuery();
                }
                Response.Redirect("Index.aspx", false);
            }
            catch (MySqlException ex) when (ex.Number == 1062) { Mostrar("Ya existe un usuario con ese correo"); }
            catch (Exception ex) { Mostrar("Error: " + ex.Message); }
            finally { con.Close(); }
        }
    }
}