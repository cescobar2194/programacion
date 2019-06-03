using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TeoriaInfo
{
    public partial class Form1 : Form
    {
        private MySqlConnection conexion = new MySqlConnection();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.ConnectionString = "Server = " + txtServer.Text + "; Port=3306; Database = " + txtDatabase.Text + "; Uid ="+ txtUser.Text+ "; Pwd = " + txtPass.Text + ";";
            try
            {
                conexion.Open();
                MessageBox.Show("Conexion Exitosa");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open) {
                    conexion.Dispose();
                    MessageBox.Show("Desconectado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
