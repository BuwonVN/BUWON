using ZXing;
using ZXing.Common;

namespace BWERP.Repositories.Services
{
	public class QRCodeGeneratorService
	{
		public string GenerateQRCode(string content, int width, int height)
		{
			var barcodeWriter = new BarcodeWriterSvg
			{
				Format = BarcodeFormat.QR_CODE,
				Options = new EncodingOptions
				{
					Height = height,  // Height for the QR code
					Width = width,    // Width for the QR code
					Margin = 1
				}
			};

			// Generate the QR code as an SVG
			var svgImage = barcodeWriter.Write(content);
			string svgContent = svgImage.Content;

			// Modify the width and height of the <svg> element manually
			svgContent = svgContent.Replace("<svg", $"<svg width=\"{width}\" height=\"{height}\"");

			return svgContent;  // Return the modified SVG with custom size
		}
		
	}
	
}