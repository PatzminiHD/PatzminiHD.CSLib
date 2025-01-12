using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.FileSystem.FileTypeInterfaces.Nbt
{
    public abstract class NamedTag
    {
        public byte tagType;
        public string? name;
        public object? payload;
        public NamedTag() { }
        public NamedTag(string? name)
        {
            this.name = name;
        }
    }
    public abstract class NbtTags
    {
        public class Tag_End : NamedTag
        {
            public Tag_End() : base()
            {
                this.tagType = (byte)TAG_TYPE.TAG_End;
            }
            

        }
    }
}
