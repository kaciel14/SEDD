using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace SEDD.Scripts
{
    public class Documento
    {
        public DateTime docTime;
        public byte[] fotoBytes;

        public Documento(Image i)
        {
            docTime = DateTime.Now;
            fotoBytes = ImageToByte(i);
        }

        public Documento(byte[] array)
        {
            this.fotoBytes = array;
        }


        public static MemoryStream ByteToImage(byte[] array)
        {
            MemoryStream ms = new MemoryStream((byte[])array);
            return ms;
        }


        public static byte[] ImageToByte(Image imagenIn)
        {
            MemoryStream ms = new MemoryStream();
            imagenIn.Save(ms, ImageFormat.Jpeg);

            if (ms.ToArray().Length <= 500000)
                return ms.ToArray();
            else
                return null;
        }
    }
}
