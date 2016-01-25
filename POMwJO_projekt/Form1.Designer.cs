namespace POMwJO_projekt
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OtworzPlik = new System.Windows.Forms.Button();
            this.Zapisz = new System.Windows.Forms.Button();
            this.segmentacja = new System.Windows.Forms.Button();
            this.Podglad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OtworzPlik
            // 
            this.OtworzPlik.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OtworzPlik.BackgroundImage")));
            this.OtworzPlik.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.OtworzPlik.Location = new System.Drawing.Point(12, 12);
            this.OtworzPlik.Name = "OtworzPlik";
            this.OtworzPlik.Size = new System.Drawing.Size(50, 50);
            this.OtworzPlik.TabIndex = 7;
            this.OtworzPlik.UseVisualStyleBackColor = true;
            this.OtworzPlik.Click += new System.EventHandler(this.OtworzPlik_Click);
            this.OtworzPlik.MouseLeave += new System.EventHandler(this.OtworzPlik_MouseLeave);
            this.OtworzPlik.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OtworzPlik_MouseMove);
            // 
            // Zapisz
            // 
            this.Zapisz.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Zapisz.BackgroundImage")));
            this.Zapisz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Zapisz.Enabled = false;
            this.Zapisz.Location = new System.Drawing.Point(68, 12);
            this.Zapisz.Name = "Zapisz";
            this.Zapisz.Size = new System.Drawing.Size(50, 50);
            this.Zapisz.TabIndex = 6;
            this.Zapisz.UseVisualStyleBackColor = true;
            this.Zapisz.Click += new System.EventHandler(this.Zapisz_Click);
            this.Zapisz.MouseLeave += new System.EventHandler(this.Zapisz_MouseLeave);
            this.Zapisz.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Zapisz_MouseMove);
            // 
            // segmentacja
            // 
            this.segmentacja.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("segmentacja.BackgroundImage")));
            this.segmentacja.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.segmentacja.Enabled = false;
            this.segmentacja.Location = new System.Drawing.Point(124, 12);
            this.segmentacja.Name = "segmentacja";
            this.segmentacja.Size = new System.Drawing.Size(50, 50);
            this.segmentacja.TabIndex = 5;
            this.segmentacja.UseVisualStyleBackColor = true;
            this.segmentacja.Click += new System.EventHandler(this.segmentacja_Click);
            this.segmentacja.MouseLeave += new System.EventHandler(this.segmentacja_MouseLeave);
            this.segmentacja.MouseMove += new System.Windows.Forms.MouseEventHandler(this.segmentacja_MouseMove);
            // 
            // Podglad
            // 
            this.Podglad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Podglad.BackgroundImage")));
            this.Podglad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Podglad.Enabled = false;
            this.Podglad.Location = new System.Drawing.Point(180, 12);
            this.Podglad.Name = "Podglad";
            this.Podglad.Size = new System.Drawing.Size(50, 50);
            this.Podglad.TabIndex = 4;
            this.Podglad.UseVisualStyleBackColor = true;
            this.Podglad.Click += new System.EventHandler(this.Podglad_Click);
            this.Podglad.MouseLeave += new System.EventHandler(this.Podglad_MouseLeave);
            this.Podglad.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Podglad_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 72);
            this.Controls.Add(this.OtworzPlik);
            this.Controls.Add(this.Zapisz);
            this.Controls.Add(this.segmentacja);
            this.Controls.Add(this.Podglad);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(251, 110);
            this.MinimumSize = new System.Drawing.Size(251, 110);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OtworzPlik;
        private System.Windows.Forms.Button Zapisz;
        private System.Windows.Forms.Button segmentacja;
        private System.Windows.Forms.Button Podglad;
    }
}

