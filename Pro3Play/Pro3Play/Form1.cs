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

namespace Pro3Play
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtURL.Text == "")
            {
                MessageBox.Show("Por favor proporcione la URL del vídeo.");
            }
            else {
                MainAsync();
            }
        }

        private async Task MainAsync()
        {
            //nuevo cliente de Youtube
            var client = new YoutubeClient();
            //lee la dirección de youtube que le escribimos en el textbox
            var videoId = NormalizeVideoId(txtURL.Text);
            var video = await client.GetVideoAsync(videoId);
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(videoId);
            // Busca la mejor resolución en la que está disponible el video
            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            // Compone el nombre que tendrá el video en base a su título y extensión
            var fileExtension = streamInfo.Container.GetFileExtension();
            var fileName = $"{video.Title}.{fileExtension}";
            //TODO: Reemplazar los caractéres ilegales del nombre
            //fileName = RemoveIllegalFileNameChars(fileName);
            //Activa el timer para que el proceso funcione de forma asincrona
            tmrVideo.Enabled = true;
            // mensajes indicando que el video se está descargando
            label4.Text = "Descargando el video ... ";
            //TODO: se pude usar una barra de progreso para ver el avance
            //using (var progress = new ProgressBar())
            //Empieza la descarga
            await client.DownloadMediaStreamAsync(streamInfo, fileName);
            //Ya descargado se inicia la conversión a MP3
            var Convert = new NReco.VideoConverter.FFMpegConverter();
            //Especificar la carpeta donde se van a guardar los archivos, recordar la \ del final
            String SaveMP3File = @"C:\Users\Carlos Escobar\Source\Repos\programacion\Pro3Play\MP3\" + fileName.Replace(".mp4", ".mp3");
            //Guarda el archivo convertido en la ubicación indicada
            Convert.ConvertMedia(fileName, SaveMP3File, "mp3");
            //Si el checkbox de solo audio está chequeado, borrar el mp4 despues de la conversión
            if (ckbAudio.Checked)
                File.Delete(fileName);
            //Indicar que se terminó la conversion
            MessageBox.Show("Vídeo convertido correctamente.");
            label4.Text = "";
            tmrVideo.Enabled = false;
            //TODO: Cargar el MP3 al reproductor o a la lista de reproducción
            //CargarMP3s();
            //Se puede incluir un checkbox para indicar que de una vez se reproduzca el MP3
            //if (ckbAutoPlay.Checked) 
            //  ReproducirMP3(SaveMP3File);
            return;
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

        }

        private void descargarVídeoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
