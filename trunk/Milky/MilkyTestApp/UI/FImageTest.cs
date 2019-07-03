using System.Drawing;
using System.Windows.Forms;
using Milky.Drawing;

namespace MilkyTestApp.UI
{
	public partial class FImageTest : Form
	{
		public FImageTest()
		{
			InitializeComponent();

			Invert.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Invert(image);
				return image;
			};

			Gray.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.GrayScale(image);
				return image;
			};

			Brightness.OnGetImage += (value) =>
			{
				var bright = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Brightness(bright, value);
				return bright;
			};

			Contrast.OnGetImage += (value) =>
			{
				var c = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Contrast(c, (sbyte)value);
				return c;
			};

			var g = Properties.Resources.sample.Clone() as Bitmap;
			BitmapFilter.Gamma(g, RTracBar.Value, GTracBar.Value, BTracBar.Value);
			GammaRLabel.Text = RTracBar.Value.ToString();
			GammaGLabel.Text = GTracBar.Value.ToString();
			GammaBLabel.Text = BTracBar.Value.ToString();
			Pict5.Image = g;
			RTracBar.ValueChanged += (_s, _e) =>
			{
				if (Pict5.Image != null)
				{
					Pict5.Image.Dispose();
				}
				var g2 = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Gamma(g2, RTracBar.Value, GTracBar.Value, BTracBar.Value);
				Pict5.Image = g2;
				GammaRLabel.Text = RTracBar.Value.ToString();
			};
			GTracBar.ValueChanged += (_s, _e) =>
			{
				if (Pict5.Image != null)
				{
					Pict5.Image.Dispose();
				}
				var g2 = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Gamma(g2, RTracBar.Value, GTracBar.Value, BTracBar.Value);
				Pict5.Image = g2;
				GammaGLabel.Text = GTracBar.Value.ToString();
			};
			BTracBar.ValueChanged += (_s, _e) =>
			{
				if (Pict5.Image != null)
				{
					Pict5.Image.Dispose();
				}
				var g2 = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Gamma(g2, RTracBar.Value, GTracBar.Value, BTracBar.Value);
				Pict5.Image = g2;
				GammaBLabel.Text = BTracBar.Value.ToString();
			};

			Smooth.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Smooth(image, value);
				return image;
			};

			Blur.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.GaussianBlur(image, value);
				return image;
			};

			Removal.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.MeanRemoval(image, value);
				return image;
			};

			Ave.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.ave(image, value);
				return image;
			};

			Sharpen.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Sharpen(image, value);
				return image;
			};

			Emboss.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.EmbossLaplacian(image);
				return image;
			};

			Edge.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.EdgeDetectQuick(image);
				return image;
			};

			Median.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.MedianFilter(image);
				return image;
			};

			Kuwahara.OnGetImage += (value) =>
			{
				var image = Properties.Resources.sample.Clone() as Bitmap;
				BitmapFilter.Kuwahara(image, value);
				return image;
			};
		}
	}
}