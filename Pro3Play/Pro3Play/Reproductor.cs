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
        double duracion;
        List<Biblioteca> Biblio = new List<Biblioteca>();
        public Reproductor()
        {
            InitializeComponent();
        }
        string ubicacionCancion;
        
        private void Reproductor_Load(object sender, EventArgs e)
        {
            button5.Visible = false;
            tmDuracionActual.Enabled = true; 
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 191, 45);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            repro.settings.volume = Convert.ToInt32(trackVolume.Value);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button5.Visible = true;
            double time = repro.Ctlcontrols.currentPosition;
            repro.Ctlcontrols.pause();
            if (time > 0 && pausa == true)
            {
                repro.Ctlcontrols.currentPosition = time;
                repro.Ctlcontrols.play();
            }
            else
            {
                if (!checkBox2.Checked)
                {
                    string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    ubicacionCancion = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    repro.Ctlcontrols.play();
                    Image f = Image.FromFile(ruta);
                    pictureBox1.Image = f;
                    leerLetra(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                }
                else
                {
                    GetRandom();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button5.Visible = false;
            repro.Ctlcontrols.stop();
        }

        private void GetRandom()
        {
            var random = new Random();
            int index = random.Next(0, dataGridView1.RowCount);
            dataGridView1.CurrentCell = dataGridView1.Rows[index].Cells[0];
            string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            repro.Ctlcontrols.play();
            Image f = Image.FromFile(ruta);
            pictureBox1.Image = f;
            leerLetra(dataGridView1.CurrentRow.Cells[4].Value.ToString());
        }
        private void button2_Click(object sender, EventArgs e)
        { 
            try
            {
                if (!checkBox2.Checked)
                {
                    int totalcanciones = dataGridView1.RowCount;
                    int filaActual = dataGridView1.CurrentRow.Index;
                    if (filaActual < totalcanciones-1)
                    { 
                        dataGridView1.CurrentCell = dataGridView1.Rows[filaActual + 1].Cells[0];
                        string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                        repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        repro.Ctlcontrols.play();
                        Image f = Image.FromFile(ruta);
                        pictureBox1.Image = f;
                        leerLetra(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                    }
                    else{
                        dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                        string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                        repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        repro.Ctlcontrols.play();
                        Image f = Image.FromFile(ruta);
                        pictureBox1.Image = f;
                        leerLetra(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                    }
                }
                else{
                    GetRandom();
                }
            }
            catch
            {
                tmDuracionActual.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ubicacionCancion))
            {
                try
                {
                    if (!checkBox2.Checked)
                    {
                        int totalcanciones = dataGridView1.RowCount;
                        int filaActual = dataGridView1.CurrentRow.Index;
                        if (filaActual == 0)
                        {
                            dataGridView1.CurrentCell = dataGridView1.Rows[totalcanciones - 1].Cells[0];
                            string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                            repro.Ctlcontrols.play();
                            Image f = Image.FromFile(ruta);
                            pictureBox1.Image = f;
                            leerLetra(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                        }
                        else
                        {
                            dataGridView1.CurrentCell = dataGridView1.Rows[filaActual - 1].Cells[0];
                            string ruta = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            repro.URL = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                            repro.Ctlcontrols.play();
                            Image f = Image.FromFile(ruta);
                            pictureBox1.Image = f;
                            leerLetra(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                        }
                    }
                    else {
                        GetRandom();
                    }
                }
                catch
                {
                    
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
            button1.Visible = true;
            button5.Visible = false;
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

        }

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
              
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
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Listas frm = new Listas();
            frm.Show();
            this.Close();
        }

        private void trackVolume_Scroll(object sender, EventArgs e)
        {
            repro.settings.volume = Convert.ToInt32(trackVolume.Value);
        }

        private void trackMedia_Scroll(object sender, EventArgs e)
        {
            repro.Ctlcontrols.currentPosition = trackMedia.Value;
        }

        private void tmDuracionActual_Tick(object sender, EventArgs e)
        {
            try
            {
                duracion = repro.currentMedia.duration;
                trackMedia.Maximum = Convert.ToInt32(duracion);
                double mediatime = repro.Ctlcontrols.currentPosition;
                trackMedia.Value = Convert.ToInt32(mediatime);
                if (repro.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    button1.Visible = true;
                    button5.Visible = false;
                }
            }
            catch (Exception)
            {

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
