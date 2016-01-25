using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using itk.simple;

namespace POMwJO_projekt
{

    public partial class Form1 : Form
    {
        Stream StreamImage=null;
        itk.simple.Image obrazWys;
        public int numberofSlice;
        System.Windows.Forms.ToolTip ToolTip1;
        itk.simple.Image obraz;
        Segmentacja segm;
        public Form1()
        {
           segm = new Segmentacja();
            InitializeComponent();
            ToolTip1 = new System.Windows.Forms.ToolTip();
            numberofSlice = 0;
        }
        //public Form1(int slice)
        //{

        //}
        private void OtworzPlik_MouseMove(object sender, MouseEventArgs e)
        {
            
            ToolTip1.SetToolTip(this.OtworzPlik, "Otworz plik");
        }

        private void OtworzPlik_MouseLeave(object sender, EventArgs e)
        {
            ToolTip1.Hide(this.OtworzPlik);
        }

        private void Zapisz_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTip1.SetToolTip(this.Zapisz, "Zapisz plik");
        }

        private void Zapisz_MouseLeave(object sender, EventArgs e)
        {
            ToolTip1.Hide(this.Zapisz);
        }

        private void segmentacja_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTip1.SetToolTip(this.segmentacja, "Segmentacja obrazu");
        }

        private void segmentacja_MouseLeave(object sender, EventArgs e)
        {
            ToolTip1.Hide(this.segmentacja);
        }

        private void Podglad_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTip1.SetToolTip(this.Podglad, "Podgląd obrazu");
        }

        private void Podglad_MouseLeave(object sender, EventArgs e)
        {
            ToolTip1.Hide(this.Podglad);
        }

        private void Podglad_Click(object sender, EventArgs e)
        {
            //Bitmap obraz1 = segm.konwertujObraz(obrazWys, numberofSlice);
            //Form forma = new Wyswietlanie(obraz1);
            //forma.Show();
            obraz obrazForm = new obraz((int)(obrazWys.GetDepth()), obrazWys);
            obrazForm.Show();
        }

        private void segmentacja_Click(object sender, EventArgs e)
        {
           // obraz obrazForm = new obraz((int)(obraz.GetDepth()),obraz);
            int ilosc_przekrojow=(int)obraz.GetDepth();
        //    var u = obrazForm.ShowDialog();
              //  numberofSlice = obrazForm.numberofSlice;
                obrazWys = segm.segmentujObraz(0,ilosc_przekrojow);
     //           Bitmap og = segm.konwertujObraz(obraz, numberofSlice);
                this.Podglad.Enabled = true;
                this.Zapisz.Enabled = true;
        }

        private void Zapisz_Click(object sender, EventArgs e)
        {
            segm.zapiszObraz();
        }

        private void OtworzPlik_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "dcm files (*.dcm)|*.dcm";// "dcm files (*.dcm)|*.dcm|All files (*.*)|*.*"
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                StreamImage = openFile.OpenFile();
                if (StreamImage != null)
                {
                  obraz=  segm.wczytajObraz(openFile.FileName);
                  this.segmentacja.Enabled = true;
                }
            }
                        
        }
    }
}
