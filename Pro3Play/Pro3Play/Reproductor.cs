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
        bool pausa = false;
        List<Biblioteca> Biblio = new List<Biblioteca>();
        public Reproductor()
        {
            InitializeComponent();
        }
        string ubicacionCancion;//Variable que contendrá la ruta de la cancion que se está reproduciendo
        
        private void Reproductor_Load(object sender, EventArgs e)
        {
            repro.settings.volume = Convert.ToInt32(vScrollBar1.Value);
            repro.uiMode = "invisible";
            LeerJson();
        }

        private void LeerJson()
        {
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
            double time = repro.Ctlcontrols.currentPosition; //return always 0 for you, because you pause first and after get the value
            repro.Ctlcontrols.pause();
            if (time > 0 && pausa == true)
            {
                repro.Ctlcontrols.currentPosition = time;
                repro.Ctlcontrols.play();
            }
            else
            {
                string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                ubicacionCancion = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                repro.Ctlcontrols.play();
                Image f = Image.FromFile(ruta);
                pictureBox1.Image = f;
                leerLetra(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            repro.Ctlcontrols.stop();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            try
            {
                int filaActual = dataGridView1.CurrentRow.Index;
                dataGridView1.CurrentCell = dataGridView1.Rows[filaActual + 1].Cells[0];
                string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                repro.Ctlcontrols.play();
                Image f = Image.FromFile(ruta);
                pictureBox1.Image = f;
            }
            catch
            {
                MessageBox.Show("Esta es la ultima cancion de la lista.");
                tmDuracionActual.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ubicacionCancion))
            {
                try
                {
                    int filaActual = dataGridView1.CurrentRow.Index;
                    dataGridView1.CurrentCell = dataGridView1.Rows[filaActual - 1].Cells[0];
                    string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    repro.Ctlcontrols.play();
                    Image f = Image.FromFile(ruta);
                    pictureBox1.Image = f;
                }
                catch
                {
                    MessageBox.Show("Esta es la primera cancion");
                }
            }
        }

        private void repro_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            pausa = true;
            repro.Ctlcontrols.pause();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                File.Delete(@"C:\Users\Carlos Escobar\Source\Repos\programacion\Pro3Play\Pro3Play\bin\Debug\Biblioteca.json");
                File.Delete(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                File.Delete(dataGridView1.CurrentRow.Cells[3].Value.ToString());
                File.Delete(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                string codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                Biblio.RemoveAll(l => l.Codigo == codigo);
                for (int i = 0; i < Biblio.Count; i++)
                {
                    GuardarBiblioteca(Biblio[i]);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Biblio;
                dataGridView1.Refresh();
                checkBox1.Checked = false;
            }
            else { 
            File.Delete(@"C:\Users\Carlos Escobar\Source\Repos\programacion\Pro3Play\Pro3Play\bin\Debug\Biblioteca.json");
            //File.Delete(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            string codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            Biblio.RemoveAll(l => l.Codigo == codigo);
            for (int i = 0; i < Biblio.Count; i++)
            {
                GuardarBiblioteca(Biblio[i]);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Biblio;
            dataGridView1.Refresh();
            }
        }

        private void GuardarBiblioteca(Biblioteca biblioteca)
        {
            string salida = JsonConvert.SerializeObject(biblioteca);
            FileStream stream = new FileStream("Biblioteca.json", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(salida);
            writer.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        public void aleatorio() {
              // get random file from the current list.
            //var random = new Random();
            //int index = random.Next(0, Biblio.Count);
            //return index;
            //listFiles.SelectedIndex = index;
        }

        private void timerMedia_Tick(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
          
        }

        private void hScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            repro.Ctlcontrols.currentPosition = hScrollBar1.Value;
        }

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
              repro.settings.volume = Convert.ToInt32(vScrollBar1.Value);
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
        private void leerLetra(string path)
        {
            textBox1.Text = File.ReadAllText(path);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("ya cambio");
        }
    }
}
