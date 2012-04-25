using System;
using System.IO;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ZoomingPdfViewer;

namespace iTextSharpTest
{
	public partial class iTextSharp_TestViewController : UIViewController
	{
		public iTextSharp_TestViewController () : base ("iTextSharp_TestViewController", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			string filePdf = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "monotouch_rocks.pdf");
			
			string sourceImageFile = Path.Combine (Environment.CurrentDirectory, "monotouch_rocks.jpg");

			var document = new Document(PageSize.A4, 50, 50, 25, 25);
			
			// Create a new PdfWriter object, specifying the output stream
			var output = new FileStream(filePdf, FileMode.Create);
			var writer = PdfWriter.GetInstance(document, output);
			
			// Open the Document for writing
			document.Open();
			
			// Create a new Paragraph object with the text, "Monotouch Rocks!!!"
			var welcomeParagraph = new Paragraph("Monotouch Rocks!!!!", new Font(Font.FontFamily.HELVETICA, 30, Font.NORMAL, BaseColor.DARK_GRAY));
			//Center
			welcomeParagraph.Alignment = 1;
				
				
			// Add the Paragraph object to the document
			document.Add(welcomeParagraph);
			
			var image = Image.GetInstance(sourceImageFile);
			//Center
			image.Alignment = 1;
			document.Add(image);
			
			//Close the Document - this saves the document contents to the output stream
			document.Close();
			
			
			PdfScrollView view = new PdfScrollView (View.Bounds, filePdf);
			View.AddSubview (view);			
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

