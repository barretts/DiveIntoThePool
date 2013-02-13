using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

public class ImageResizer
{
    private Stream _stream;
    private int _quality;
    private int _width;
    private int _height;

    public ImageResizer(Stream stream, int quality, int width, int height)
    {
        _stream = stream;
        _quality = quality;
        _width = width;
        _height = height;
    }

    public void Save(string saveTo)
    {
        using (Bitmap bmp = new Bitmap(_stream, true))
        {
            using (Bitmap newBmp = new Bitmap(bmp, new Size(_width, _height)))
            {
                long quality = _quality;
                Encoder encoder = Encoder.Quality;
                EncoderParameters parameters = new EncoderParameters(1);
                parameters.Param[0] = new EncoderParameter(encoder, quality);
                newBmp.Save(saveTo, GetEncoderInfo("image/jpeg"), parameters);
            }
        }
    }

    private ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        foreach (ImageCodecInfo imageCodecInfo in ImageCodecInfo.GetImageEncoders())
        {
            if (imageCodecInfo.MimeType.Equals(mimeType))
            {
                return imageCodecInfo;
            }
        }
        return null;
    }
}