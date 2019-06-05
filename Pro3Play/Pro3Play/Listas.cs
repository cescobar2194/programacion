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
        string nombreLista;
        List<Biblioteca> Biblio = new List<Biblioteca>();
        List<Biblioteca> ListaRep = new List<Biblioteca>();
        List<ListaRep> Playlist = new List<ListaRep>();
        List<Biblioteca> ListaActual = new List<Biblioteca>();

        private void Listas_Load(object sender, EventArgs e)
        {
            LeerJson();
            LeerJsonListas();
            //for (int i = 0; i < Playlist.Count; i++)
            //{
                comboBox1.DisplayMember= "Nombre1";
                comboBox1.ValueMember = "Direccion1";
                comboBox1.DataSource = Playlist;
                comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);

            //}
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
           // dataGridView1.DataSource = Biblio;
            //dataGridView1.Refresh();
            //Libro lib = Agregar.OrderBy(al => al.Anio1).First();
            //textBox5.Text = lib.Anio1.ToString();
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
        private void GuardarCancionesdeLista(Biblioteca biblioteca)
        {
           
            if (textBox1.Text=="")
            {
                MessageBox.Show("Por favor ingrese un nombre de la lista");
            }
            else { 
             nombreLista = "C:\\Users\\Carlos Escobar\\Source\\Repos\\programacion\\Pro3Play\\Listas\\" + textBox1.Text + ".json";
            string salida = JsonConvert.SerializeObject(biblioteca);
            FileStream stream = new FileStream( nombreLista, FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(salida);
            writer.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Biblioteca cancion = new Biblioteca();
            cancion.Codigo= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cancion.Nombre= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cancion.Direccion = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cancion.Portada= dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cancion.Letra= dataGridView1.CurrentRow.Cells[4].Value.ToString();
            ListaRep.Add(cancion);
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
            ListaRep lista = new ListaRep();
            lista.Nombre1 = textBox1.Text;
            lista.Direccion1 = nombreLista;
            GuardarListas(lista);
            Playlist.Add(lista);
            //dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            textBox1.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("wecosa");
            ListaRep seleccion = comboBox1.SelectedItem as ListaRep;

            if (seleccion == null)
                return;

            LeerJsonCanciones(seleccion.Direccion1);
        }

        private void LeerJsonCanciones(string  path)
        {
            //ListaActual.RemoveAll(x => x == null);
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
            //Libro lib = Agregar.OrderBy(al => al.Anio1).First();
            //textBox5.Text = lib.Anio1.ToString();
        }
    }
}
