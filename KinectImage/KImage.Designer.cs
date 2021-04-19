namespace KinectImage
{
    partial class KImage
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
            this.bCapture = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.pbColor = new System.Windows.Forms.PictureBox();
            this.pbInfrared = new System.Windows.Forms.PictureBox();
            this.pbDepth = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfrared)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDepth)).BeginInit();
            this.SuspendLayout();
            // 
            // bCapture
            // 
            this.bCapture.Location = new System.Drawing.Point(1049, 154);
            this.bCapture.Name = "bCapture";
            this.bCapture.Size = new System.Drawing.Size(100, 50);
            this.bCapture.TabIndex = 1;
            this.bCapture.Text = "Capture";
            this.bCapture.UseVisualStyleBackColor = true;
            this.bCapture.Click += new System.EventHandler(this.bCapture_Click);
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Location = new System.Drawing.Point(1049, 232);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(100, 50);
            this.bStop.TabIndex = 2;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // pbColor
            // 
            this.pbColor.Location = new System.Drawing.Point(0, 0);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(640, 480);
            this.pbColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbColor.TabIndex = 3;
            this.pbColor.TabStop = false;
            // 
            // pbInfrared
            // 
            this.pbInfrared.Location = new System.Drawing.Point(640, 480);
            this.pbInfrared.Name = "pbInfrared";
            this.pbInfrared.Size = new System.Drawing.Size(640, 480);
            this.pbInfrared.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbInfrared.TabIndex = 4;
            this.pbInfrared.TabStop = false;
            // 
            // pbDepth
            // 
            this.pbDepth.Location = new System.Drawing.Point(0, 480);
            this.pbDepth.Name = "pbDepth";
            this.pbDepth.Size = new System.Drawing.Size(640, 480);
            this.pbDepth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDepth.TabIndex = 5;
            this.pbDepth.TabStop = false;
            // 
            // KImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 964);
            this.Controls.Add(this.pbDepth);
            this.Controls.Add(this.pbInfrared);
            this.Controls.Add(this.pbColor);
            this.Controls.Add(this.bStop);
            this.Controls.Add(this.bCapture);
            this.Name = "KImage";
            this.Text = "Kinect Imaging";
            this.Load += new System.EventHandler(this.KImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInfrared)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDepth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bCapture;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.PictureBox pbColor;
        private System.Windows.Forms.PictureBox pbInfrared;
        private System.Windows.Forms.PictureBox pbDepth;
    }
}

