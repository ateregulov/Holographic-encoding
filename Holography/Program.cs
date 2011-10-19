using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;


namespace Holography
{
    class DataVariables
    {
        public double w = 0.03; //длина волны источника света
        public double z = 50; //расстояние между пластинами
        public double sensitivityHologram = 50; //чувствительность голограммы
        public double sensitivityOutput = 0.01; //чувствительность выходной пластины
        public int p = -150; //смещение исходной пластинки
        public string sourceFile = @"C:\Users\1\Desktop\source.bmp";

    }

    class Program
    {


        static void Main(string[] args)
        {
            DataVariables dv = new DataVariables();

            Bitmap source; 
            //Bitmap output = new Bitmap(source.Width, source.Height);
            Bitmap hologram = new Bitmap(300, 300);
            //Bitmap hologram = new Bitmap(source.Width, source.Height);

            do
            {
                source = new Bitmap(dv.sourceFile, true);
                
                hologram = WriteHologram.Write(source, hologram, dv);

                //hologram = TruncHalf.Cut(hologram);

                hologram.Save(@"C:\Users\1\Desktop\hologram.bmp");

                //hologram = new Bitmap(@"C:\Users\1\Desktop\hologram.bmp",true);
                //output = ReadHologram.Read(hologram, output, dv); 
                //output.Save(@"C:\Users\1\Desktop\output.bmp");

                source.Dispose();

            } while (Console.ReadKey().ToString() != "q");
        }


    }
}
