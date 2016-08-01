using System.Net;
using Android.Graphics;

namespace DangerouslyDelicious.Utilities
{
    public class BitmapDownloader
    {
        public static Bitmap GetRatingStars(string url)
        {
            using (var client = new WebClient())
            {
                var imageRaw = client.DownloadData(url);
                var image = BitmapFactory.DecodeByteArray(imageRaw, 0, imageRaw.Length);

                return image;
            }
        }
    }
}