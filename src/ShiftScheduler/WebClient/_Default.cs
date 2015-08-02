using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class _Default : System.Web.UI.Page
    {
        void PostImage(string id)
        {
            byte[] image = FindImage(id);
            WriteBinaryImage(image);
        }
        void WriteBinaryImage(byte[] image)
        {
            if (image != null)
            {
                Response.ContentType = "image/jpeg";
                using (MemoryStream ms = new MemoryStream(image))
                {
                    using (Bitmap bmp = (Bitmap)Bitmap.FromStream(ms))
                    {
                        int width = bmp.Width * 50 / bmp.Height;
                        Bitmap thumb = new Bitmap(bmp, new Size(width, 50));
                        thumb.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                }
            }
            else
            {
                Response.ContentType = "image/gif";
            }
            Response.End();
        }
        byte[] FindImage(string id)
        {                        
            
            string virtualPath = String.Format("~/Content/Images/{0}.jpg", id);
            string path = Page.MapPath(virtualPath);
            byte[] result = null;
             
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                result = bytes;
            }
            catch(FileNotFoundException ex)
            {
                virtualPath = String.Format("~/Content/Images/unknownUser.png", id);
                    path = Page.MapPath(virtualPath);
                using (FileStream newstream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[newstream.Length];
                    newstream.Read(bytes, 0, (int)newstream.Length);
                    result = bytes;
                }

            }
            finally
            {
                if (stream != null) ((IDisposable)stream).Dispose();
            }


            return result;
        }
    }
}