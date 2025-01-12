using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.FileSystem.FileTypeInterfaces.Nbt
{
    public enum TAG_TYPE : byte
    {
        TAG_End = 0,
        TAG_Byte = 1,
        TAG_Short = 2,
        TAG_Int = 3,
        TAG_Long = 4,
        TAG_Float = 5,
        TAG_Double = 6,
        TAG_Byte_Array = 7,
        TAG_String = 8,
        TAG_List = 9,
        TAG_Compound = 10,
    }
    public class Named_Tag
    {
        public byte tagType;
        public string? name;
        public object[]? payload;

        public override string ToString()
        {
            return ToString(0);
        }
        public string ToString(int level)
        {
            string result = "";
            string tab = "";
            
            for(int i = 0; i < level; i++)
            {
                tab += "   ";
            }

            result += $"{tab}{(TAG_TYPE)tagType}";
            if(name != null)
                result += $"(\"{name}\"):";
            else
                result += $":";

            if (payload?.Length > 1 && tagType != (byte)TAG_TYPE.TAG_Byte_Array)
                result += $" {payload.Length} entries";

            if(tagType == (byte)TAG_TYPE.TAG_List && payload != null && payload.Length > 0)
            {
                result += $" of type {((TAG_TYPE)((Named_Tag)payload[0]).tagType).ToString()}";
            }

            if (tagType == (byte)TAG_TYPE.TAG_Compound || tagType == (byte)TAG_TYPE.TAG_List)
                result += $"\n{tab}{{\n";

            if (payload != null)
            {
                foreach (var obj in payload)
                {
                    if (obj is Named_Tag)
                    {
                        result += ((Named_Tag)obj).ToString(level + 1);
                    }
                    else if(tagType == (byte)TAG_TYPE.TAG_Byte_Array)
                    {
                        result += $" {obj},";
                    }
                    else if(tagType == (byte)TAG_TYPE.TAG_List)
                    {
                        result += $"";
                    }
                    else
                    {
                        result += $" {obj}\n";
                    }
                }
                if (tagType == (byte)TAG_TYPE.TAG_Byte_Array)
                {
                    result = result.Remove(result.Length - 1);
                    result += "\n";
                }
            }
            else
            {
                result += $"{tab}null\n";
            }

            if (tagType == (byte)TAG_TYPE.TAG_Compound || tagType == (byte)TAG_TYPE.TAG_List)
                result += $"{tab}}}\n";

            return result;
        }
    }

    public class NbtFile : Named_Tag
    {
        public NbtFile(Named_Tag[]? payload, string? filename = null)
        {
            this.tagType = (byte)TAG_TYPE.TAG_Compound;
            this.name = filename;
            this.payload = payload;
            Named_Tag tag = new();
        }
    }
}
