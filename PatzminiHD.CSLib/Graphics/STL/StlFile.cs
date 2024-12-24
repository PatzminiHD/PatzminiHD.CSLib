namespace PatzminiHD.CSLib.Graphics.STL;

/// <summary>
/// This class contains Methods for reading and writing STL files
/// </summary>
public static class StlFile
{                                                               //"s"   "o"   "l"   "i"   "d"   " "
    private static readonly byte[] AsciiSignature = new byte[6] { 0x73, 0x6f, 0x6c, 0x69, 0x64, 0x20 };
    /// <summary>
    /// Read a STL file from a stream
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static StlObject Read(string path)
    {
        bool isAscii = false;
        using (var stream = new FileStream(path, FileMode.Open))
        {
            byte[] buffer = new byte[6];
            stream.Seek(0, SeekOrigin.Begin);
            stream.ReadExactly(buffer, 0, 6);
            
            if(buffer.SequenceEqual(AsciiSignature))
                isAscii = true;
        }
        
        if(isAscii)
            return ReadAscii(path);
        else
            return ReadBinary(path);
    }

    private static StlObject ReadAscii(string path)
    {
        string[] lines = File.ReadAllLines(path);
        string tmpLine = "";
        List<float> vertices = new List<float>();
        int lineNumber = 0;
        float[] tmpVertices = new float[6*3]; //Z, Y, X and Normals, 3 times
        float[] facetNormal = new float[3];
        bool inFacet = false, inLoop = false;
        byte currentVertex = 0;

        while (lineNumber < lines.Length)
        {
            if (lines[lineNumber].Trim().StartsWith("facet normal "))
            {
                inFacet = true;
                tmpLine = lines[lineNumber].Trim().Replace("facet normal ", "");
                for (int i = 0; i < facetNormal.Length; i++)
                {
                    facetNormal[i] = float.Parse(tmpLine.Substring(0, tmpLine.IndexOf(' ') > 0 ? tmpLine.IndexOf(' ') : tmpLine.Length));
                    tmpLine = tmpLine.Substring(tmpLine.IndexOf(' ') + 1);
                }
            }
            
            if(inFacet && lines[lineNumber].Trim().StartsWith("outer loop"))
                inLoop = true;

            if (inFacet && inLoop && lines[lineNumber].Trim().StartsWith("vertex ") && currentVertex < 3)
            {
                tmpLine = lines[lineNumber].Trim().Replace("vertex ", "");
                for (int i = 0; i < 3; i++)
                {
                    tmpVertices[i + currentVertex*6] = float.Parse(tmpLine.Substring(0, tmpLine.IndexOf(' ') > 0 ? tmpLine.IndexOf(' ') : tmpLine.Length));
                    tmpLine = tmpLine.Substring(tmpLine.IndexOf(' ') + 1);
                }

                Array.Copy(facetNormal, 0, tmpVertices, 3+currentVertex*6, facetNormal.Length);
                currentVertex++;
            }

            if (inFacet && inLoop && lines[lineNumber].Trim().StartsWith("endloop"))
            {
                inLoop = false;
                vertices.AddRange(tmpVertices);
            }
            
            if (inFacet && lines[lineNumber].Trim().StartsWith("endfacet"))
            {
                inFacet = false;
            }
            
            lineNumber++;
        }

        StlObject stlObject = new StlObject();
        stlObject.vertices = vertices.ToArray();
        return stlObject;
    }

    private static StlObject ReadBinary(string path)
    {
        return new();
    }
}