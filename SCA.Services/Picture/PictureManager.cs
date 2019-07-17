using Microsoft.AspNetCore.Http;
using SCA.Common.Result;
using SCA.Services.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SCA.Services
{
    public class PictureManager : IPictureManager
    {
        public PictureManager()
        {
        }

        public string SaveImage(string ImgStr, string ImgName)

        {
            string imagePath = "";
            try
            {

                var webRoot = "";
                var PathWithFolderName = System.IO.Path.Combine(webRoot, "UserPhotos");


                if (!Directory.Exists(PathWithFolderName))
                {
                    DirectoryInfo di = Directory.CreateDirectory(PathWithFolderName);
                }

                string Base64String = ImgStr.Replace("data:image/jpeg;base64,", "");//eventMaster.BannerImage.Replace("data:image/png;base64,", "");

                byte[] bytes = Convert.FromBase64String(Base64String);
                ConvertImageToByteArray(bytes);
            }
            catch (Exception ex)
            {

                throw;
            }



            return imagePath;
        }

        public static byte[] ConvertImageToByteArray(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            {
                ImageFormat format;
                Image image = Image.FromStream(ms);

                switch (image.GetType().ToString())
                {
                    case "image/png":
                        format = ImageFormat.Png;
                        break;
                    case "image/gif":
                        format = ImageFormat.Gif;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }

                image.Save(ms, format);
                return ms.ToArray();
            }
        }
    }
}
