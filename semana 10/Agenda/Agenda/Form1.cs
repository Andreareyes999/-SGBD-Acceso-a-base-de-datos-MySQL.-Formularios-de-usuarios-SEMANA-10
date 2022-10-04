using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Agenda
{
    public partial class frmusuarios : Form
    {
        public string cadena_conexion = "Database=agenda;Data Source=localhost;User Id=dba;Password=dba"; //cadena de conexión a mysql

        public void rellenargrid()
        {
            string consulta = "select * from contactos"; //consulta a la base de datos
            MySqlConnection conexion = new MySqlConnection(cadena_conexion); //se crea la conexión
            conexion.Open();

            MySqlDataAdapter comando = new MySqlDataAdapter(consulta, conexion); // se ejecuta la consulta
            System.Data.DataSet ds = new System.Data.DataSet(); //Se crea el dataset

            comando.Fill(ds, "agenda"); //se rellena el origen de la base de datos
            dataGridView1.DataSource = ds; //se crea el contenedor de datos para la grid
            dataGridView1.DataMember = "agenda"; //se establece el origen para la grid el dataset creado anteriormente


        }

        public void cancelarboton()
        {
            txtusuario.Enabled = false;
            txtclave.Enabled = false;
            txtcumple.Enabled = false;
            cmbnivel.Enabled = false;
            txtusuario.Text = "";
            txtclave.Text = "";
            txtcumple.Text = "";
            cmbnivel.Text = "";
            btnnuevo.Text = "Nuevo";
            btnsalir.Text = "Salir";
            btnmodificar.Text = "Modificar";
            btnmodificar.Enabled = true;
            btneliminar.Enabled = true;
            btnnuevo.Enabled = true;
            //txtbuscar.Enabled = false;
            //btnbuscar.Enabled = false;

        }

        public frmusuarios()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(178, 226, 242);

           

            try
            {
                rellenargrid();
                MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                string mySelectQuery = "SELECT * From contactos";
                MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
                myConnection.Open();
                MySqlDataReader myReader;
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    string fecha_cumple = myReader.GetString(3);
                    string[] fecha_cumple2 = fecha_cumple.Split('/');
                    string nueva_fecha_cumple = fecha_cumple2[0] + "/" + fecha_cumple2[1] + "/" + DateTime.Now.ToString("yyyy");

                    string fecha_actual = DateTime.Now.ToString("dd/MM/yyyy");

                    DateTime fechaUno = Convert.ToDateTime(nueva_fecha_cumple);
                    DateTime fechaDos = Convert.ToDateTime(fecha_actual);

                    TimeSpan difFechas = fechaUno - fechaDos;


                    int dias = difFechas.Days;


                    if (dias > 0 && dias <= 3)
                    {
                        MessageBox.Show("El Usuario: " + myReader.GetString(1).ToUpper() + " Esta a " + dias + " Dias de su cumpleaños", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (MySqlException)
            {
                MessageBox.Show("Error de conexion", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           

        }
        //El boton nuevo realmente solo habilita el formulario para agregar un nuevo registro
        private void Btnnuevo_Click(object sender, EventArgs e)
        {
            //varaiable para la validacion del boton
            String verificar = btnnuevo.Text;

            if (verificar == "Nuevo") // si tiene nuevo hace lo siguiente
            {
                txtusuario.Enabled = true;
                txtclave.Enabled = true;
                cmbnivel.Enabled = true;
                txtcumple.Enabled = true;
                txtusuario.Text = "";
                txtclave.Text = "";
                cmbnivel.Text = "Seleccione nivel";
                txtusuario.Focus();
                //btnnuevo.Visible = false;
                btnnuevo.Text = "Guardar";
                btnsalir.Text = "Cancelar";
                btnmodificar.Enabled = false;
                btneliminar.Enabled = false;
             
            }
            else if (verificar == "Guardar") //si tiene el texto de guardar procede al agregar el registro
            {

                if (txtusuario.Text == "" || txtclave.Text == "" || cmbnivel.Text == "")
                {
                    MessageBox.Show("Debe de completar los datos para poder agregar el registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                        string myInsertQuery = "INSERT INTO contactos(nombre,clave,fecha,nivel) Values(?nombre,?clave,?fecha,?nivel)";
                        MySqlCommand myCommand = new MySqlCommand(myInsertQuery);

                        myCommand.Parameters.Add("?nombre", MySqlDbType.VarChar, 75).Value = txtusuario.Text.ToUpper();
                        myCommand.Parameters.Add("?clave", MySqlDbType.VarChar, 75).Value = txtclave.Text.ToUpper();
                        myCommand.Parameters.Add("?fecha", MySqlDbType.VarChar, 50).Value = txtcumple.Text.ToUpper();
                        myCommand.Parameters.Add("?nivel", MySqlDbType.Int32, 50).Value = cmbnivel.Text.ToUpper();

                        myCommand.Connection = myConnection;
                        myConnection.Open();
                        myCommand.ExecuteNonQuery();
                        myCommand.Connection.Close();

                        MessageBox.Show("Usuario agregado con éxito", "Ok", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                        rellenargrid();

                    }
                    catch (MySqlException)
                    {
                        MessageBox.Show("Ya existe el usuario", "Alerta!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    cancelarboton();
                }  
            }



        }

        private void Btnsalir_Click(object sender, EventArgs e)
        {
            String verificarsalir = btnsalir.Text;

            if (verificarsalir == "Salir")
            {
                DialogResult resultado = MessageBox.Show("Esta Seguro que desea Salir","Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (resultado == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
            else if (verificarsalir == "Cancelar")
            {
                cancelarboton();              
            }

        }

        private void Btnmodificar_Click(object sender, EventArgs e)
        {
            String verificarmodificar = btnmodificar.Text;

            if (verificarmodificar == "Modificar")
            {
                txtusuario.Enabled = true;
                txtclave.Enabled = true;
                txtcumple.Enabled = true;
                cmbnivel.Enabled = true;
                txtbuscar.Enabled = true;
                btnbuscar.Enabled = true;

                txtbuscar.Focus();
                btnmodificar.Text = "Actualizar";
                btnsalir.Text = "Cancelar";
                btnnuevo.Enabled = false;
                btneliminar.Enabled = false;

                //usuario_modificar = txtusuario.Text.ToString();
            }
            else if (verificarmodificar == "Actualizar")
            {

                if (txtusuario.Text == "" || txtclave.Text == "" || cmbnivel.Text == "")
                {
                    MessageBox.Show("Debe de completar los datos para poder hacer la modificacion del registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult resultado = MessageBox.Show("Esta Seguro que desea modificar el registro", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (resultado == DialogResult.OK)
                    {

                        try
                        {

                            MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                            string myInsertQuery = "UPDATE contactos SET nombre=?nombre, clave=?clave, fecha=?fecha, nivel=?nivel Where codigo = " + txtbuscar.Text + "";
                            MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                            myCommand.Parameters.Add("?nombre", MySqlDbType.VarChar, 75).Value = txtusuario.Text.ToUpper();
                            myCommand.Parameters.Add("?clave", MySqlDbType.VarChar, 75).Value = txtclave.Text.ToUpper();
                            myCommand.Parameters.Add("?fecha", MySqlDbType.VarChar, 50).Value = txtcumple.Text.ToUpper();
                            myCommand.Parameters.Add("?nivel", MySqlDbType.VarChar, 50).Value = cmbnivel.Text.ToUpper();
                            myCommand.Connection = myConnection;
                            myConnection.Open();
                            myCommand.ExecuteNonQuery();
                            myCommand.Connection.Close();

                            MessageBox.Show("El Registro fue modificado con exito", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            rellenargrid();
                            cancelarboton();
                        }
                        catch (System.Exception)
                        {

                            MessageBox.Show("No se cuando da este error", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }



                }               


            }


        }

        private void Btnbuscar_Click(object sender, EventArgs e)
        {

         
                try
                {
                    MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                    string mySelectQuery = "SELECT * From contactos Where codigo=" + txtbuscar.Text + "";
                    MySqlCommand myCommand = new MySqlCommand(mySelectQuery, myConnection);
                    myConnection.Open();
                    MySqlDataReader myReader;
                    myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        txtusuario.Text = (myReader.GetString(1));
                        txtclave.Text = (myReader.GetString(2));
                        txtcumple.Text = (myReader.GetString(3));
                    cmbnivel.Text = (myReader.GetString(4));
                    }
                    else
                    {
                        MessageBox.Show("El Registro No Existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       
                    }
                    myReader.Close();
                    myConnection.Close();
                }
                catch (System.Exception)
                {
                    MessageBox.Show("La busqueda tiene que ser con el Codigo del Usuario Solo se aceptan Numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            
       

        }

        private void Btneliminar_Click(object sender, EventArgs e)
        {

            DialogResult resultado = MessageBox.Show("Esta Seguro que desea eliminar el registro", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                try
                {

                    MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                    string myInsertQuery = "DELETE FROM contactos Where codigo=" + txtbuscar.Text + "";
                    MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                    myCommand.Connection = myConnection;
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myCommand.Connection.Close();

                    MessageBox.Show("Eliminado Con Éxito", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    rellenargrid();


                }
                catch (System.Exception)
                {
                    if (txtusuario.Text == "" || txtclave.Text == "")
                    {
                        MessageBox.Show("No debe dejar Campos Vacio para poder hacer la Modificacion", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
            }
        }

        private void NuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String verificar = btnnuevo.Text;

            if (verificar == "Nuevo") // si tiene nuevo hace lo siguiente
            {
                txtusuario.Enabled = true;
                txtclave.Enabled = true;
                txtcumple.Enabled = true;
                cmbnivel.Enabled = true;
                txtusuario.Text = "";
                txtclave.Text = "";
                cmbnivel.Text = "Seleccione nivel";
                txtusuario.Focus();
                //btnnuevo.Visible = false;
                btnnuevo.Text = "Guardar";
                btnsalir.Text = "Cancelar";
                btnmodificar.Enabled = false;
                btneliminar.Enabled = false;

               
            }
        }

        private void ModificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String verificarmodificar = btnmodificar.Text;

            if (verificarmodificar == "Modificar")
            {
                txtusuario.Enabled = true;
                txtclave.Enabled = true;
                txtcumple.Enabled = true;
                cmbnivel.Enabled = true;
                txtbuscar.Enabled = true;
                btnbuscar.Enabled = true;

                txtbuscar.Focus();
                btnmodificar.Text = "Actualizar";
                btnsalir.Text = "Cancelar";
                btnnuevo.Enabled = false;
                btneliminar.Enabled = false;

                //usuario_modificar = txtusuario.Text.ToString();
            }
        }

        private void EliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Esta Seguro que desea eliminar el registro", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                try
                {

                    MySqlConnection myConnection = new MySqlConnection(cadena_conexion);
                    string myInsertQuery = "DELETE FROM contactos Where codigo=" + txtbuscar.Text + "";
                    MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
                    myCommand.Connection = myConnection;
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                    myCommand.Connection.Close();

                    MessageBox.Show("Eliminado Con Éxito", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    rellenargrid();


                }
                catch (System.Exception)
                {
                    if (txtusuario.Text == "" || txtclave.Text == "")
                    {
                        MessageBox.Show("No debe dejar Campos Vacio para poder hacer la Modificacion", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
            }
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Esta Seguro que desea Salir", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                Application.Exit();
            }


        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acerca_de about = new Acerca_de();
            about.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
    }
