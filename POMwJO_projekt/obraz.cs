using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POMwJO_projekt
{
    public partial class obraz : Form
    {
        public int numberofSlice{get;set;}
        itk.simple.Image imagetoDisplay;
        public obraz()
        {
            InitializeComponent();
        }
        public obraz(int maxSlide, itk.simple.Image image)
        {
            InitializeComponent();
            this.przekroj.Maximum = maxSlide;
            this.imagetoDisplay = image;
        }

      

        private void przekroj_Scroll(object sender, ScrollEventArgs e)
        {
            //   int slide = this.trackBar1.Value;
            Segmentacja segmentacja = new Segmentacja();
            numberofSlice = this.przekroj.Value;
            Bitmap obraz = segmentacja.konwertujObraz(imagetoDisplay, numberofSlice);
            //   this.pictureBox1.Hide();
            this.pictureBox1.Image = obraz;
            //    this.pictureBox1.Update();
            this.pictureBox1.Refresh();
            this.pictureBox1.Show();

        }
    }
}
