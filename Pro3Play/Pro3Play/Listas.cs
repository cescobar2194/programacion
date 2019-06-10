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
    public partial class Listas : Form
    {
        public Listas()
        {
            InitializeComponent();
        }
        bool pausa = false;
        double duracion;
        string nombreLista;
        string ubicacionCancion;
        List<Biblioteca> Biblio = new List<Biblioteca>();
        List<Biblioteca> ListaRep = new List<Biblioteca>();
        List<ListaRep> Playlist = new List<ListaRep>();
        List<Biblioteca> ListaActual = new List<Biblioteca>();

        private void Listas_Load(object sender, EventArgs e)
        {
            tmDuracionActual.Enabled = true;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            button5.Visible = false;
            //Estilo del Primer DataGridview.
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 191, 45);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Estilo del Segundo DataGridview.
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 191, 45);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //Estilo del Tercer DataGridview.
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.FromArgb(153, 191, 45);
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.BackgroundColor = Color.White;
            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            LeerJson();
            LeerJsonListas();
            comboBox1.DisplayMember= "Nombre1";
            comboBox1.ValueMember = "Direccion1";
            comboBox1.DataSource = Playlist;
            comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
        }

        private void LeerJsonListas()
        {
            FileStream stream = new FileStream("Listas.json", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                string lectura = reader.ReadLine();
                ListaRep libroLeido = JsonConvert.DeserializeObject<ListaRep>(lectura);
                Playlist.Add(libroLeido);
            }
            reader.Close();
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

        private void GuardarCancionesdeLista(Biblioteca biblioteca)
        {
            nombreLista = "C:\\Users\\Carlos Escobar\\Source\\Repos\\programacion\\Pro3Play\\Listas\\" + textBox1.Text + ".json";
            string salida = JsonConvert.SerializeObject(biblioteca);
            FileStream stream = new FileStream( nombreLista, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(salida);
            writer.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Biblioteca cancion = new Biblioteca();
            cancion.Codigo= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cancion.Nombre= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cancion.Direccion = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cancion.Portada= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cancion.Letra = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            if (ListaRep.Any(item => item.Codigo == cancion.Codigo))
            {
                MessageBox.Show("La canción ya existe en el Playlist actual.", "" , MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else {
                ListaRep.Add(cancion);
            }
            GuardarCancionesdeLista(cancion);
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = ListaRep;
            dataGridView2.Refresh();
        }

        private void GuardarListas(ListaRep biblioteca)
        {
            string salida = JsonConvert.SerializeObject(biblioteca);
            FileStream stream = new FileStream("Listas.json", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(salida);
            writer.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Debe ingresar un nombre al Playlist.");
            }
            else {
                ListaRep lista = new ListaRep();
                lista.Nombre1 = textBox1.Text;
                lista.Direccion1 = nombreLista;
                GuardarListas(lista);
                Playlist.Add(lista);
                dataGridView2.DataSource = null;
                textBox1.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListaRep seleccion = comboBox1.SelectedItem as ListaRep;
            if (seleccion == null)
                return;
            LeerJsonCanciones(seleccion.Direccion1);
            repro.Ctlcontrols.stop();
            button1.Visible = true;
            button5.Visible = false;
        }

        private void LeerJsonCanciones(string  path)
        {
            ListaActual.Clear();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                string lectura = reader.ReadLine();
                Biblioteca libroLeido = JsonConvert.DeserializeObject<Biblioteca>(lectura);
                ListaActual.Add(libroLeido);
            }
            reader.Close();
            dataGridView3.DataSource = null;
            dataGridView3.DataSource = ListaActual;
            dataGridView3.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void GetRandom()
        {
            var random = new Random();
            int index = random.Next(0, dataGridView3.RowCount);
            dataGridView3.CurrentCell = dataGridView3.Rows[index].Cells[0];
            string ruta = dataGridView3.CurrentRow.Cells[3].Value.ToString();
            repro.URL = dataGridView3.CurrentRow.Cells[2].Value.ToString();
            repro.Ctlcontrols.play();
            Image f = Image.FromFile(ruta);
            pictureBox1.Image = f;
            leerLetra(dataGridView3.CurrentRow.Cells[4].Value.ToString());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkBox2.Checked)
                {
                    int totalcanciones = dataGridView3.RowCount;
                    int filaActual = dataGridView3.CurrentRow.Index;
                    if (filaActual < totalcanciones - 1)
                    {
                        dataGridView3.CurrentCell = dataGridView3.Rows[filaActual + 1].Cells[0];
                        string ruta = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                        repro.URL = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                        repro.Ctlcontrols.play();
                        Image f = Image.FromFile(ruta);
                        pictureBox1.Image = f;
                        leerLetra(dataGridView3.CurrentRow.Cells[4].Value.ToString());
                    }
                    else
                    {
                        dataGridView3.CurrentCell = dataGridView3.Rows[0].Cells[0];
                        string ruta = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                        repro.URL = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                        repro.Ctlcontrols.play();
                        Image f = Image.FromFile(ruta);
                        pictureBox1.Image = f;
                        leerLetra(dataGridView3.CurrentRow.Cells[4].Value.ToString());
                    }
                }
                else
                {
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
                        int totalcanciones = dataGridView3.RowCount;
                        int filaActual = dataGridView3.CurrentRow.Index;
                        if (filaActual == 0)
                        {
                            dataGridView3.CurrentCell = dataGridView3.Rows[totalcanciones - 1].Cells[0];
                            string ruta = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                            repro.URL = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                            repro.Ctlcontrols.play();
                            Image f = Image.FromFile(ruta);
                            pictureBox1.Image = f;
                            leerLetra(dataGridView3.CurrentRow.Cells[4].Value.ToString());
                        }
                        else
                        {
                            dataGridView3.CurrentCell = dataGridView3.Rows[filaActual - 1].Cells[0];
                            string ruta = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                            repro.URL = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                            repro.Ctlcontrols.play();
                            Image f = Image.FromFile(ruta);
                            pictureBox1.Image = f;
                            leerLetra(dataGridView3.CurrentRow.Cells[4].Value.ToString());
                        }
                    }
                    else
                    {
                        GetRandom();
                    }
                }
                catch
                {

                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
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
                    string ruta = dataGridView3.CurrentRow.Cells[3].Value.ToString();
                    repro.URL = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                    ubicacionCancion = dataGridView3.CurrentRow.Cells[2].Value.ToString();
                    repro.Ctlcontrols.play();
                    Image f = Image.FromFile(ruta);
                    pictureBox1.Image = f;
                    leerLetra(dataGridView3.CurrentRow.Cells[4].Value.ToString());
                }
                else
                {
                    GetRandom();
                }
            }
        }

        private void leerLetra(string path)
        {
            textBox2.Text = File.ReadAllText(path);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button5.Visible = false;
            repro.Ctlcontrols.stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            button5.Visible = false;
            pausa = true;
            repro.Ctlcontrols.pause();
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

        private void trackMedia_Scroll(object sender, EventArgs e)
        {
            repro.Ctlcontrols.currentPosition = trackMedia.Value;
        }
    }
}
