namespace Pro3Play
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.descargarVídeoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrVideo = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.ckbAudio = new System.Windows.Forms.CheckBox();
            this.listasDeReproducciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reproductorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Product Sans Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(98, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Conversor de Vídeos a MP3";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(41, 183);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(391, 20);
            this.txtURL.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "URL del vídeo";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Product Sans", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(41, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "Descargar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descargarVídeoToolStripMenuItem,
            this.listasDeReproducciónToolStripMenuItem,
            this.reproductorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(474, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // descargarVídeoToolStripMenuItem
            // 
            this.descargarVídeoToolStripMenuItem.Name = "descargarVídeoToolStripMenuItem";
            this.descargarVídeoToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.descargarVídeoToolStripMenuItem.Text = "Biblioteca";
            this.descargarVídeoToolStripMenuItem.Click += new System.EventHandler(this.descargarVídeoToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 6;
            // 
            // ckbAudio
            // 
            this.ckbAudio.AutoSize = true;
            this.ckbAudio.Location = new System.Drawing.Point(41, 243);
            this.ckbAudio.Name = "ckbAudio";
            this.ckbAudio.Size = new System.Drawing.Size(164, 17);
            this.ckbAudio.TabIndex = 7;
            this.ckbAudio.Text = "Eliminar después de convertir";
            this.ckbAudio.UseVisualStyleBackColor = true;
            // 
            // listasDeReproducciónToolStripMenuItem
            // 
            this.listasDeReproducciónToolStripMenuItem.Name = "listasDeReproducciónToolStripMenuItem";
            this.listasDeReproducciónToolStripMenuItem.Size = new System.Drawing.Size(141, 20);
            this.listasDeReproducciónToolStripMenuItem.Text = "Listas de Reproducción";
            // 
            // reproductorToolStripMenuItem
            // 
            this.reproductorToolStripMenuItem.Name = "reproductorToolStripMenuItem";
            this.reproductorToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.reproductorToolStripMenuItem.Text = "Reproductor";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(173, 79);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(126, 83);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 306);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ckbAudio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Convertir nuevo Vídeo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem descargarVídeoToolStripMenuItem;
        private System.Windows.Forms.Timer tmrVideo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckbAudio;
        private System.Windows.Forms.ToolStripMenuItem listasDeReproducciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reproductorToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

