using Silk.NET.OpenGL;
using StbImageSharp;

namespace PatzminiHD.CSLib.Graphics.Silk.NET.Abstractions;

/// <summary>
/// Abstraction for a Texture<br/><br/>
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
public class Texture : IDisposable
    {
        private uint _handle;
        private GL _gl;

        /// <summary>
        /// Constructor for a texture object from an image
        /// </summary>
        /// <param name="gl">Reference to the OpenGL API</param>
        /// <param name="path">Filepath to the image that should be used as the texture</param>
        public unsafe Texture(GL gl, string path)
        {
            //Saving the gl instance.
            _gl = gl;

            //Generating the opengl handle;
            _handle = _gl.GenTexture();
            Bind();
            
            // Load the image from memory.
            ImageResult result = ImageResult.FromMemory(File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);
            
            fixed (byte* ptr = result.Data)
            {
                // Create our texture and upload the image data.
                _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint) result.Width, 
                    (uint) result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
            }

            SetParameters();
        }

        /// <summary>
        /// Constructor for a texture object from other data
        /// </summary>
        /// <param name="gl">Reference to the OpenGL API</param>
        /// <param name="data">Raw bytes of texture data</param>
        /// <param name="width">Width of the texture</param>
        /// <param name="height">Height of the texture</param>
        public unsafe Texture(GL gl, Span<byte> data, uint width, uint height)
        {
            //Saving the gl instance.
            _gl = gl;

            //Generating the opengl handle;
            _handle = _gl.GenTexture();
            Bind();

            //We want the ability to create a texture using data generated from code aswell.
            fixed (void* d = &data[0])
            {
                //Setting the data of a texture.
                _gl.TexImage2D(TextureTarget.Texture2D, 0, (int) InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
                SetParameters();
            }
        }

        private void SetParameters()
        {
            //Setting some texture perameters so the texture behaves as expected.
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) GLEnum.ClampToEdge);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) GLEnum.ClampToEdge);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) GLEnum.LinearMipmapLinear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) GLEnum.Linear);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBaseLevel, 0);
            _gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, 8);
            
            //Generating mipmaps.
            _gl.GenerateMipmap(TextureTarget.Texture2D);
        }

        /// <summary>
        /// Bind the texture to a texture slot
        /// </summary>
        /// <param name="textureSlot">Texture Slot to bind to</param>
        public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
        {
            //When we bind a texture we can choose which textureslot we can bind it to.
            _gl.ActiveTexture(textureSlot);
            _gl.BindTexture(TextureTarget.Texture2D, _handle);
        }

        /// <summary>
        /// Dispose used resources
        /// </summary>
        public void Dispose()
        {
            //In order to dispose we need to delete the opengl handle for the texure.
            _gl.DeleteTexture(_handle);
        }
    }