using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.FileSystem.FileTypeInterfaces.Nbt
{
    public class NbtParser
    {
        public static NbtFile Parse(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            int read = stream.ReadByte();
            if (read != (byte)TAG_TYPE.TAG_Compound)
            {
                throw new Exception("Invalid NBT file");
            }

            return null;
        }
    }
}
