using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holography.Core
{
	class Settings
	{
		public double w = 0.03; //длина волны источника света
		public double z = 50; //расстояние между пластинами
		public double sensitivityHologram = 50; //чувствительность голограммы
		public double sensitivityOutput = 0.01; //чувствительность выходной пластины
		public int p = -150; //смещение исходной пластинки
		public string sourceFile = @"C:\source.bmp";
		public string destinationFile = @"C:\hologram.bmp";
		public int destinationWidth = 300;
		public int destinationHeight = 300;
	}
}
