using System;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Runtime.InteropServices;
namespace System.Drawing
{
	public class Bitmap : IDisposable
	{
		public int Width {get;set;}
		public int Height {get;set;}
		
		private UIImage _backingImage = null;
		byte[] pixelData = new byte[0];
		//int width = 0;
		//int height = 0;
		IntPtr rawData;
		
		public Bitmap (UIImage image)
		{
			_backingImage = image;
			CGImage imageRef = _backingImage.CGImage;
			Width = imageRef.Width;
			Height = imageRef.Height;
			CGColorSpace colorSpace = CGColorSpace.CreateDeviceRGB();
			
			rawData = Marshal.AllocHGlobal(Height*Width*4);
			CGContext context = new CGBitmapContext(
				rawData, Width, Height, 8, 4*Width, colorSpace, CGImageAlphaInfo.PremultipliedLast
			);
			context.DrawImage(new RectangleF(0.0f,0.0f,(float)Width,(float)Height),imageRef);
			
			pixelData = new byte[Height*Width*4];
			Marshal.Copy(rawData,pixelData,0,pixelData.Length);
		}
		
		public Color GetPixel(int x, int y)
		{
			try {				
				byte bytesPerPixel = 4;
				int bytesPerRow = Width * bytesPerPixel;
				int rowOffset = y * bytesPerRow;
				int colOffset = x * bytesPerPixel;
				int pixelDataLoc = rowOffset + colOffset;
				
				Color ret = Color.FromArgb(pixelData[pixelDataLoc+3],pixelData[pixelDataLoc+0],pixelData[pixelDataLoc+1],pixelData[pixelDataLoc+2]);
				return ret;
			} catch (Exception ex) {
				Console.WriteLine("Orig: {0}x{1}", _backingImage.Size.Width,_backingImage.Size.Height);
				Console.WriteLine("Req:  {0}x{1}", x, y);
				throw ex;
			}
		
			return Color.FromArgb(0,0,0,0);
		}
	

		void IDisposable.Dispose ()
		{
			Marshal.FreeHGlobal(rawData);
		}
}
}

