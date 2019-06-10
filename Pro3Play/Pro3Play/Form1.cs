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
            if (textBox1.Text != null)
            {
                letra();
                if (txtURL.Text == "") {
                    MessageBox.Show("Por favor proporcione la URL del vídeo.");
                }
                else if (pictureBox2.Image == null) {
                    MessageBox.Show("Por favor suba una portada para el MP3.");
                }
                else if (textBox1.Text == "")
                {
                    MessageBox.Show("Por favor ingrese la letra de la canción.");
                }
                else {
                    MessageBox.Show("El vídeo se está convirtiendo, por favor espere...");
                    MainAsync();
                    }   
                }
            }

        private async Task MainAsync()
        {
            Biblioteca bib = new Biblioteca();
            var client = new YoutubeClient();
            var videoId = NormalizeVideoId(txtURL.Text);
            var video = await client.GetVideoAsync(videoId);
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videoId);
            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            var fileExtension = streamInfo.Container.GetFileExtension();
            var fileName = $"{video.Title}.{fileExtension}";
            tmrVideo.Enabled = true;
            await client.DownloadMediaStreamAsync(streamInfo, fileName);
            var Convert = new NReco.VideoConverter.FFMpegConverter();
            String SaveMP3File = @"C:\Users\Carlos Escobar\Source\Repos\programacion\Pro3Play\MP3\" + fileName.Replace(".mp4", ".mp3");
            bib.Direccion = SaveMP3File;
            bib.Nombre = fileName;
            Convert.ConvertMedia(fileName, SaveMP3File, "mp3");
            if (ckbAudio.Checked)
            {
                File.Delete(fileName);
            }
            label4.Text = "";
            txtURL.Text = "";
            textBox1.Text = "";
            pictureBox2.Image = null;
            tmrVideo.Enabled = false;
            bib.Letra = direccionLetra;
            bib.Portada = direccionPortada;
            bib.Codigo = i.ToString();
            GuardarBiblioteca(bib);
            MessageBox.Show("Vídeo convertido correctamente. Será redireccionado a la Biblioteca.");
            Reproductor rep = new Reproductor();
            rep.Show();
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
        }

        private void descargarVídeoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reproductorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
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