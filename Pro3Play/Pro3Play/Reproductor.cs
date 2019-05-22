using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace Pro3Play
{
    public partial class Reproductor : Form
    {
        public Reproductor()
        {
            InitializeComponent();
        }

        private void Reproductor_Load(object sender, EventArgs e)
        {
            repro.uiMode = "invisible";
            LeerJson();
        }

        private void LeerJson()
        {
            List<Biblioteca> Biblio = new List<Biblioteca>();
            FileStream stream = new FileStream("Biblioteca.json", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                string lectura = reader.ReadLine();
                Biblioteca libroLeido = JsonConvert.DeserializeObject<Biblioteca>(lectura);
                Biblio.Add(libroLeido);
            }
            reader.Close();
            dataGridView1.DataSource = Biblio;
            dataGridView1.Refresh();
            //Libro lib = Agregar.OrderBy(al => al.Anio1).First();
            //textBox5.Text = lib.Anio1.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           repro.Ctlcontrols.play();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            repro.Ctlcontrols.stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            repro.Ctlcontrols.next();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            repro.Ctlcontrols.previous();
        }

        private void repro_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
