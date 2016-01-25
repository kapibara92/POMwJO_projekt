using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using itk.simple;
using System.Windows.Input;
using System.Drawing;
using Image = itk.simple.Image;
using System.Runtime.InteropServices;


namespace POMwJO_projekt
{
    public class Segmentacja
    {
        private Image obraz;
        private Image obrazWysegmentowany;
        private Bitmap obrazBitmap;
        private string nazwaObrazu;
        public Image wczytajObraz(string sciezka)
        {
            ImageFileReader r = new ImageFileReader();
            r.SetFileName(sciezka);
            r.SetOutputPixelType(itk.simple.PixelIDValueEnum.sitkInt16);
            var image = r.Execute();
            nazwaObrazu = Path.GetFileName(r.GetFileName());
            obraz = image;
            return obraz;
        }
        public void zapiszObraz()
        {
            try
            {
                ImageFileWriter writer = new ImageFileWriter();
                string sciezka = Directory.GetCurrentDirectory();
                string pathString = Path.Combine(sciezka, nazwaObrazu);
                if (Directory.Exists(pathString) == false)
                {
                    Directory.CreateDirectory(pathString);
                }
                pathString = Path.Combine(pathString, nazwaObrazu);

                //     writer.SetFileName(p);
                writer.Execute(obraz, pathString, false);
            }
            catch(ArgumentNullException e)
            {
                throw new Exception("Przed zapisem wybierz plik");
            }
        }
        public void zapiszObraz(Image obraz1)
        {
            try {
                ImageFileWriter writer = new ImageFileWriter();
                string sciezka = Directory.GetCurrentDirectory();
                string pathString = Path.Combine(sciezka, "Wyniki");
                Random r = new Random();
                string e = r.Next(1,100).ToString();
                string nazwaPliku1 = "segmentacja" + "_" + e+".dcm";
                if (Directory.Exists(pathString) == false)
                {
                    Directory.CreateDirectory(pathString);
                }
                pathString = Path.Combine(pathString, nazwaPliku1);

                //     writer.SetFileName(p);
                writer.Execute(obraz1, pathString, false);
            }
            catch
            {
                throw new Exception("Blad podczas zapisu");
            }
        }
        public Image segmentujObraz(int numerPrzekroj, int numerMaxPrzekroju=0)
        {
            try {
                
                Image image;
               
                if (numerMaxPrzekroju == 0)
                {
                    image = WybierzPrzekroj(obraz, numerPrzekroj);
                }
                else
                {
                    image = WybierzPrzekroj(obraz, numerPrzekroj, numerMaxPrzekroju);
                }

                itk.simple.Image imgWstepnePrzetwarzanie = this.WstepnePrzetwarzanie(image);
                itk.simple.Image imgSegmentowanaSledziona = this.SegmetacjaSledziony(imgWstepnePrzetwarzanie);
                itk.simple.Image imgNalozenieObrazow = this.NalozObrazy(image, imgSegmentowanaSledziona);

                itk.simple.Image obrazWysegmentowany = imgNalozenieObrazow;
                uint e = obraz.GetDimension();
                uint j = obraz.GetWidth();
                uint k = obraz.GetDepth();
                uint o = obrazWysegmentowany.GetDepth();
                
                return obrazWysegmentowany;
                //zapiszObraz(obrazWysegmentowany);
                
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentNullException("sprawdz czy wczytales obraz");
            }
            catch(Exception e)
            {
                //throw new Exception("blad podczas segmentacji obrazu");
                throw new Exception(e.Message);
            }
        }
        public itk.simple.Image WstepnePrzetwarzanie(itk.simple.Image image)
        {
            itk.simple.BinaryMorphologicalClosingImageFilter closer = new BinaryMorphologicalClosingImageFilter();
            closer.SetKernelType(KernelEnum.sitkCross);
            closer.SetKernelRadius(1);
            closer.SetForegroundValue(511);
            itk.simple.Image imClose = closer.Execute(image);


            itk.simple.SmoothingRecursiveGaussianImageFilter gauss = new SmoothingRecursiveGaussianImageFilter();
            gauss.SetSigma(1);
            itk.simple.Image gausImage = gauss.Execute(imClose);

            itk.simple.CastImageFilter caster = new CastImageFilter();
            caster.SetOutputPixelType(itk.simple.PixelIDValueEnum.sitkInt16);
            itk.simple.Image castImage = caster.Execute(gausImage);

            itk.simple.ImageFileWriter writer = new itk.simple.ImageFileWriter();
            writer.SetFileName("WstepnePrzetwarzanie.dcm");
            writer.Execute(image);

            return castImage;
        }

        public itk.simple.Image SegmetacjaSledziony(itk.simple.Image image)
        {
            itk.simple.MorphologicalGradientImageFilter gradientFilter = new MorphologicalGradientImageFilter();
            gradientFilter.SetKernelType(KernelEnum.sitkCross);
            gradientFilter.SetKernelRadius(1);
            itk.simple.Image gradientImage = gradientFilter.Execute(image);
            
            itk.simple.SigmoidImageFilter sigmoidFilter = new SigmoidImageFilter();
            sigmoidFilter.SetOutputMinimum(0);
            sigmoidFilter.SetOutputMaximum(1);
            sigmoidFilter.SetAlpha(1.5);
            sigmoidFilter.SetBeta(100);
            itk.simple.Image sigmoidImage = sigmoidFilter.Execute(gradientImage);
          
            itk.simple.BinaryThresholdImageFilter threshFilter = new BinaryThresholdImageFilter();
            threshFilter.SetLowerThreshold(100);
            threshFilter.SetUpperThreshold(180);
            threshFilter.SetOutsideValue(0);
            threshFilter.SetInsideValue(1);
            itk.simple.Image threshImage = threshFilter.Execute(image);
            itk.simple.CastImageFilter castThresh = new CastImageFilter();
            castThresh.SetOutputPixelType(itk.simple.PixelIDValueEnum.sitkInt16);
            itk.simple.Image castThreshImage = castThresh.Execute(threshImage);

            itk.simple.SubtractImageFilter substractFilter = new SubtractImageFilter();
            itk.simple.Image substractImage = substractFilter.Execute(castThreshImage, sigmoidImage);

            itk.simple.ThresholdImageFilter threshFilter2 = new ThresholdImageFilter();
            threshFilter2.SetLower(0);
            threshFilter2.SetUpper(0);
            threshFilter2.SetOutsideValue(1);
            itk.simple.Image threshImage2 = threshFilter2.Execute(substractImage);

            itk.simple.BinaryErodeImageFilter erodeFilter = new BinaryErodeImageFilter();
            erodeFilter.SetForegroundValue(1);
            erodeFilter.SetBackgroundValue(0);
            erodeFilter.SetKernelType(itk.simple.KernelEnum.sitkCross);
            erodeFilter.SetKernelRadius(5);
            itk.simple.Image erodeImage = erodeFilter.Execute(threshImage2);

            itk.simple.ConnectedComponentImageFilter connFilter = new ConnectedComponentImageFilter();
            connFilter.SetFullyConnected(true);
            itk.simple.Image connImage = connFilter.Execute(erodeImage);

            itk.simple.RelabelComponentImageFilter relabelFilter = new RelabelComponentImageFilter();
            relabelFilter.SetMinimumObjectSize(5000);
            itk.simple.Image relabelImage = relabelFilter.Execute(connImage);

            itk.simple.BinaryThresholdImageFilter threshFilter3 = new BinaryThresholdImageFilter();
            threshFilter3.SetInsideValue(1);
            threshFilter3.SetOutsideValue(0);
            threshFilter3.SetLowerThreshold(1);
            threshFilter3.SetUpperThreshold(1);
            itk.simple.Image threshImage3 = threshFilter3.Execute(relabelImage);

            itk.simple.DilateObjectMorphologyImageFilter closeReconstructFilter = new DilateObjectMorphologyImageFilter();
            closeReconstructFilter.SetKernelType(itk.simple.KernelEnum.sitkCross);
            closeReconstructFilter.SetKernelRadius(5);
            itk.simple.Image closeReconstructImage = closeReconstructFilter.Execute(threshImage3);
            itk.simple.ImageFileWriter writer = new ImageFileWriter();
            writer.SetFileName("SegmentujSledzione.dcm");
            writer.Execute(closeReconstructImage);

            return closeReconstructImage;
        }

        public itk.simple.Image NalozObrazy(itk.simple.Image original, itk.simple.Image segmented)
        {
            itk.simple.MinimumMaximumImageFilter maxminFilter = new MinimumMaximumImageFilter();
            maxminFilter.Execute(original);
            double max = maxminFilter.GetMaximum();

            itk.simple.CastImageFilter caster1 = new CastImageFilter();
            caster1.SetOutputPixelType(itk.simple.PixelIDValueEnum.sitkInt16);
            itk.simple.Image castImg = caster1.Execute(segmented);

            itk.simple.MultiplyImageFilter multiplyFilter = new MultiplyImageFilter();
            itk.simple.Image multiplyImage = multiplyFilter.Execute(castImg, max);
            
            itk.simple.MaximumImageFilter maximumFilter = new MaximumImageFilter();
            itk.simple.Image maximumImage = maximumFilter.Execute(original, multiplyImage);
            
            //itk.simple.IntensityWindowingImageFilter windower = new IntensityWindowingImageFilter();
            //windower.SetWindowMinimum(0);
            //windower.SetWindowMaximum(255);
            //windower.SetOutputMinimum(0);
            //windower.SetOutputMaximum(1);
            //itk.simple.Image window = windower.Execute(maximumImage);
            itk.simple.ImageFileWriter writer = new ImageFileWriter();
            writer.SetFileName("Wynik.dcm");
            writer.Execute(maximumImage);

            return maximumImage;
        }
        public Bitmap konwertujObraz(Image obraz1, int przekroj = 0)
        {
            uint r = obraz1.GetWidth();
            //  VectorUInt32 w = new VectorUInt32(new[] { r, 512, 4 + 1 });

            VectorInt32 start = new VectorInt32(new[]{ 0, 0, 0 });
            VectorInt32 size1 = new VectorInt32(new[]{ 512, 512, 1 });
                obraz1 = WybierzPrzekroj(obraz1, przekroj);
                IntensityWindowingImageFilter normalize = new IntensityWindowingImageFilter();
                normalize.SetOutputMinimum(0);
                normalize.SetOutputMaximum(255);
                obraz1 = normalize.Execute(obraz1);
            
            PixelIDValueEnum u = PixelIDValueEnum.sitkFloat32;
            int len = 1;
            Image input= SimpleITK.Cast(obraz1, u);
            VectorUInt32 size = input.GetSize();
            for (int dim = 0; dim < input.GetDimension(); dim++)
            {
                len *= (int)size[dim];
            }
            IntPtr buffer = input.GetBufferAsFloat();
            float bufferPtr = (float)buffer.ToInt32();
            float[] bufferAsArray = new float[len]; 
            float[,] newData = new float[size[0], size[1]];
            Marshal.Copy(buffer, bufferAsArray, 0, len);
            obrazBitmap = new Bitmap(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
            for (int j = 0; j < size[1]; j++)
            {
                for (int i = 0; i < size[0]; i++)
                {
                    var bur = bufferAsArray[j * size[1] + i];
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb((int)bur, 0, 0, 0);
                    obrazBitmap.SetPixel(j, i, newColor);
                }
            }
           Color s= obrazBitmap.GetPixel(34, 56);
            return obrazBitmap;
        }
        public Image WybierzPrzekroj(Image obraz3d, int numerPrzekroju)
        {
            int numberSlices = (int)obraz3d.GetDepth();
            if (numberSlices < numerPrzekroju)
            {
                return obraz3d;
            }
            else
            {
                SliceImageFilter sliceFilter = new SliceImageFilter();
                sliceFilter.SetStart(new VectorInt32(new[] { 1, 1, numerPrzekroju }));
                sliceFilter.SetStop(new VectorInt32(new[] { 512, 512, numerPrzekroju + 1 }));
                var obraz2d = sliceFilter.Execute(obraz3d);
                sliceFilter.Dispose();
                return obraz2d;
            }
        }
        public Image WybierzPrzekroj(Image obraz3d, int przekrojMin, int PrzekrojMax)
        {
            int numberSlices = (int)obraz3d.GetDepth();
            if (numberSlices < przekrojMin)
            {
                return obraz3d;
            }
            else
            {
                Image obraz2d = new Image();
                if (przekrojMin < PrzekrojMax)
                {
                    if (PrzekrojMax-1 > numberSlices)
                    {
                        PrzekrojMax = numberSlices;
                    }
                    SliceImageFilter sliceFilter = new SliceImageFilter();
                    sliceFilter.SetStart(new VectorInt32(new[] { 1, 1, przekrojMin }));
                    sliceFilter.SetStop(new VectorInt32(new[] { 512, 512, PrzekrojMax }));
                    obraz2d = sliceFilter.Execute(obraz3d);
                    sliceFilter.Dispose();
                }
                return obraz2d;
            }
        }

        private float Min(itk.simple.Image image)
        {
            itk.simple.MinimumMaximumImageFilter minimumFilter = new MinimumMaximumImageFilter();
            minimumFilter.Execute(image);
            float min = (float) minimumFilter.GetMinimum();
            return min;
        }
        private float Max(itk.simple.Image image)
        {
            itk.simple.MinimumMaximumImageFilter minimumFilter = new MinimumMaximumImageFilter();
            minimumFilter.Execute(image);
            float min = (float)minimumFilter.GetMaximum();
            return min;
        }
        private float Min(float[] array)
        {
            if (array.Length > 0)
            {
                float min = array[0];
                for (int i = 1; i < array.Length; i++)
                {
                    if (array[i] < min)
                    {
                        min = array[i];
                    }
                }
                return min;
            }
            else
            {
                return 0;
            }
        }
        private float Max(float[] array)
        {
            if (array.Length > 0)
            {
                float max = array[0];
                for (int i = 1; i < array.GetLength(0); i++)
                {
                    if (array[i] > max)
                    {
                        max = array[i];
                    }
                }
                return max;
            }
            else { return 0; }
        }
    }
  
}
