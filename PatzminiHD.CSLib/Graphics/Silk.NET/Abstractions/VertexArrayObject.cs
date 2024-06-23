using Silk.NET.OpenGL;

namespace PatzminiHD.CSLib.Graphics.Silk.NET.Abstractions;

/// <summary>
/// The vertex array object abstraction<br/><br/>
///
/// The code in this class was originally taken from the Silk.NET project (https://github.com/dotnet/Silk.NET)<br/>
/// Silk.NET is licensed under the MIT License:<br/><br/>
///
///Copyright (c) 2019-2020 Ultz Limited<br/>
///Copyright (c) 2021- .NET Foundation and Contributors<br/><br/>
///
///Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:<br/>
///
///The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
/// </summary>
public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
    where TVertexType : unmanaged
    where TIndexType : unmanaged
{
    //Our handle and the GL instance this class will use, these are private because they have no reason to be public.
    //Most of the time you would want to abstract items to make things like this invisible.
    private uint _handle;
    private GL _gl;

    /// <summary>
    /// Constructor for a vertex array object
    /// </summary>
    /// <param name="gl">Reference to the OpenGL API</param>
    /// <param name="vbo">Reference to the Vertex Buffer Object</param>
    /// <param name="ebo">Reference to the Element Buffer Object</param>
    public VertexArrayObject(GL gl, BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
    {
        //Saving the GL instance.
        _gl = gl;

        //Setting out handle and binding the VBO and EBO to this VAO.
        _handle = _gl.GenVertexArray();
        Bind();
        vbo.Bind();
        ebo.Bind();
    }

    /// <summary>
    /// Set up a vertex attribute pointer
    /// </summary>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <param name="type"></param>
    /// <param name="vertexSize"></param>
    /// <param name="offSet"></param>
    public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet)
    {
        //Setting up a vertex attribute pointer
        _gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint) sizeof(TVertexType), (void*) (offSet * sizeof(TVertexType)));
        _gl.EnableVertexAttribArray(index);
    }

    /// <summary>
    /// Bind the vertex array
    /// </summary>
    public void Bind()
    {
        //Binding the vertex array.
        _gl.BindVertexArray(_handle);
    }

    /// <summary>
    /// Dispose used resources
    /// </summary>
    public void Dispose()
    {
        //Remember to dispose this object so the data GPU side is cleared.
        //We dont delete the VBO and EBO here, as you can have one VBO stored under multiple VAO's.
        _gl.DeleteVertexArray(_handle);
    }
}