using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace AnimatedOverlayMaker;

public class Program
{
	public static void Main(string[] args)
	{ 
		var ship = Image.Load(File.OpenRead("ship.png"));
		var overlayfull = Image.Load(File.OpenRead("overlay.png"));
		var animation = ship.CloneAs<Rgba32>();
		animation.Metadata.GetFormatMetadata(GifFormat.Instance).ColorTableMode = GifColorTableMode.Local;
		var rect = new Rectangle(0, 0, overlayfull.Width / 2, overlayfull.Height);
		for (int x = 0; x < overlayfull.Width / 2; x++)
		{
			rect.X = x;
			var overlay = overlayfull.Clone(i => i.Crop(rect));
			var frame = ship.Clone(i => i.DrawImage(overlay, PixelColorBlendingMode.Overlay, 1f));
			frame.SaveAsPng($"shipoverlay-{x}.png");
			//animation.Frames.AddFrame(frame.Frames.RootFrame);
		}

		animation.SaveAsGif("shipoverlay.gif");
		foreach (var frame in animation.Frames)
		{
			//ImageExtensions.SaveAsPng(frame.Metadata.);
		}
	}
}