using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExtractor;
using NReco.VideoConverter;
using VideoLibrary;
using YoutubeExplode;
using Tyrrrz.Extensions;
using YoutubeExplode.Models.MediaStreams;
using System.IO;
using Newtonsoft.Json;

namespace Pro3Play
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string direccionPortada;
        string direccionLetra;
        List<Biblioteca> Biblio = new List<Biblioteca>();
        int i;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=null)
            {

            
            //LeerJson();
            letra();
            if (txtURL.Text == ""){
                MessageBox.Show("Por favor proporcione la URL del vídeo.");
            }
            else {
                MainAsync();
            }
            }
            else
            {
                MessageBox.Show("ingrese la letra");
            }
        }

        private async Task MainAsync()
        {
            Biblioteca bib = new Biblioteca();
            //Nuevo Cliente de YouTube
            var client = new YoutubeClient();
            //Lee la URL de youtube que le escribimos en el textbox.
            var videoId = NormalizeVideoId(txtURL.Text);
            var video = await client.GetVideoAsync(videoId);
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videoId);
            //Busca la mejor resolución en la que está disponible el video.
            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            //Compone el nombre que tendrá el video en base a su título y extensión.
            var fileExtension = streamInfo.Container.GetFileExtension();
            var fileName = $"{video.Title}.{fileExtension}";
            //TODO: Reemplazar los caractéres ilegales del nombre
            //fileName = RemoveIllegalFileNameChars(fileName);
            //Activa el timer para que el proceso funcione de forma asincrona
            tmrVideo.Enabled = true;
            // mensajes indicando que el video se está descargando
            label4.Text = "Descargando el video...";
            //TODO: se pude usar una barra de progreso para ver el avance
            //using (var progress = new ProgressBar());
            //Empieza la descarga.
            await client.DownloadMediaStreamAsync(streamInfo, fileName);
            //Ya descargado se inicia la conversión a MP3
            var Convert = new NReco.VideoConverter.FFMpegConverter();
            //Especificar la carpeta donde se van a guardar los archivos, recordar la \ del final
            String SaveMP3File = @"C:\Users\Carlos Escobar\Source\Repos\programacion\Pro3Play\MP3\" + fileName.Replace(".mp4", ".mp3");
            bib.Direccion = SaveMP3File;
            bib.Nombre = fileName;
            //Guarda el archivo convertido en la ubicación indicada
            Convert.ConvertMedia(fileName, SaveMP3File, "mp3");
            //Si el checkbox de solo audio está chequeado, borrar el mp4 despues de la conversión
            if (ckbAudio.Checked)
            {
                File.Delete(fileName);
            }
            //Indicar que se terminó la conversion
            MessageBox.Show("Vídeo convertido correctamente.");
            label4.Text = "";
            txtURL.Text = "";
            tmrVideo.Enabled = false;
            //TODO: Cargar el MP3 al reproductor o a la lista de reproducción
            //CargarMP3s();
            //Se puede incluir un checkbox para indicar que de una vez se reproduzca el MP3
            //if (ckbAutoPlay.Checked) 
            //  ReproducirMP3(SaveMP3File);
            bib.Letra = direccionLetra;
            bib.Portada = direccionPortada;
            bib.Codigo = i.ToString();
            GuardarBiblioteca(bib);
            return;
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
            //dataGridView2.DataSource = Agregar;
            //dataGridView2.Refresh();
            //Libro lib = Agregar.OrderBy(al => al.Anio1).First();
            //textBox5.Text = lib.Anio1.ToString();
        }

        private void GuardarBiblioteca(Biblioteca biblioteca)
        {
            string salida = JsonConvert.SerializeObject(biblioteca);
            FileStream stream = new FileStream("Biblioteca.json", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(salida);
            writer.Close();
        }


        private static string NormalizeVideoId(string input)
        {
            string videoId = string.Empty;
            return YoutubeClient.TryParseVideoId(input, out videoId)
                ? videoId
                : input;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LeerJson();
            i = Convert.ToInt16(Biblio.OrderByDescending(l => l.Codigo).ElementAt(0).Codigo.ToString());
            i = i + 1;
            // i = Biblio.Count() + 1;
            // i= Convert.ToInt16( Biblio.OrderBy(l => l.Codigo).ElementAt(0).ToString());
        }

        private void descargarVídeoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reproductorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reproductor repro = new Reproductor();
            repro.ShowDialog();
        }

        private void guardarPortadaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
             
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string nombrearchivo;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image f = Image.FromFile(openFileDialog1.FileName);
                pictureBox2.Image = f;
                nombrearchivo = openFileDialog1.FileName.ToString();
                direccionPortada = "C:\\Users\\Carlos Escobar\\Source\\Repos\\programacion\\Pro3Play\\PORTADAS\\" + i + ".png";
                f.Save("C:\\Users\\Carlos Escobar\\Source\\Repos\\programacion\\Pro3Play\\PORTADAS\\" + i + ".png");
                //f.Save(nombrearchivo);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
         
        }
        private void letra()
        {
            direccionLetra = "C:\\Users\\Carlos Escobar\\Source\\Repos\\programacion\\Pro3Play\\LETRAS\\" + i + ".txt";
            FileStream stream = new FileStream(direccionLetra, FileMode.Append, FileAccess.Write);
            StreamWriter write = new StreamWriter(stream);
            write.WriteLine(textBox1.Text);
            write.Close();
        }
    }
}
