using System;
using System.Drawing;
using System.IO;

namespace Presentacion.Base.Varios
{
    public class Imagen
    {
        /// <summary>
        /// Metodo para convertir una imagen en byte
        /// </summary>
        /// <param name="img">Imagen</param>
        /// <returns></returns>
        public static byte[] Convertir_Imagen_Bytes(Image img)
        {
            var sTemp = Path.GetTempFileName();
            var fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            var imgLength = Convert.ToInt32(fs.Length);
            var bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }

        /// <summary>
        /// Metodo para convertir byte en Imagen
        /// </summary>
        /// <param name="bytes">Byte</param>
        /// <returns></returns>
        public static Image Convertir_Bytes_Imagen(byte[] bytes)
        {
            if (bytes == null) return null;

            var ms = new MemoryStream(bytes);
            Bitmap bm = null;
            try
            {
                bm = new Bitmap(ms);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return bm;
        }
    }
}
