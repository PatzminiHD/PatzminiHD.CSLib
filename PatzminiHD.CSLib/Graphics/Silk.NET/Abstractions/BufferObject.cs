using Silk.NET.OpenGL;

namespace PatzminiHD.CSLib.Graphics.Silk.NET.Abstractions;


/// <summary>
/// Our buffer object abstraction.<br/><br/>
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
/// <typeparam name="TDataType"></typeparam>
public class BufferObject<TDataType> : IDisposable
    where TDataType : unmanaged
{
    //Our handle, buffertype and the GL instance this class will use, these are private because they have no reason to be public.
    //Most of the time you would want to abstract items to make things like this invisible.
    private uint _handle;
    private BufferTargetARB _bufferType;
    private GL _gl;

    /// <summary>
    /// Constructor for a Buffer Object
    /// </summary>
    /// <param name="gl">Reference to the OpenGL API</param>
    /// <param name="data">Data of the Buffer Object</param>
    /// <param name="bufferType">Type of the buffer</param>
    public unsafe BufferObject(GL gl, Span<TDataType> data, BufferTargetARB bufferType)
    {
        //Setting the gl instance and storing our buffer type.
        _gl = gl;
        _bufferType = bufferType;

        //Getting the handle, and then uploading the data to said handle.
        _handle = _gl.GenBuffer();
        Bind();
        fixed (void* d = data)
        {
            _gl.BufferData(bufferType, (nuint) (data.Length * sizeof(TDataType)), d, BufferUsageARB.StaticDraw);
        }
    }

    /// <summary>
    /// Bind the buffer object
    /// </summary>
    public void Bind()
    {
        //Binding the buffer object, with the correct buffer type.
        _gl.BindBuffer(_bufferType, _handle);
    }

    /// <summary>
    /// Dispose used resources
    /// </summary>
    public void Dispose()
    {
        //Remember to delete our buffer.
        _gl.DeleteBuffer(_handle);
    }
}