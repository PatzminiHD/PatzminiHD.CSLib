using System.Numerics;
using Silk.NET.OpenGL;

namespace PatzminiHD.CSLib.Graphics.Silk.NET.Abstractions;

/// <summary>
/// Abstraction for a Shader<br/><br/>
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
public class Shader : IDisposable
    {
        //Our handle and the GL instance this class will use, these are private because they have no reason to be public.
        //Most of the time you would want to abstract items to make things like this invisible.
        private uint _handle;
        private GL _gl;

        /// <summary>
        /// Constructor for a shader object
        /// </summary>
        /// <param name="gl">Reference to the OpenGL API</param>
        /// <param name="vertexPath">Filepath to a file containing the vertex shader code</param>
        /// <param name="fragmentPath">Filepath to a file containing the fragment shader code</param>
        /// <exception cref="Exception">Is thrown when the shaders failed to compile or to link</exception>
        public Shader(GL gl, string vertexPath, string fragmentPath)
        {
            _gl = gl;

            //Load the individual shaders.
            uint vertex = LoadShader(ShaderType.VertexShader, vertexPath);
            uint fragment = LoadShader(ShaderType.FragmentShader, fragmentPath);
            //Create the shader program.
            _handle = _gl.CreateProgram();
            //Attach the individual shaders.
            _gl.AttachShader(_handle, vertex);
            _gl.AttachShader(_handle, fragment);
            _gl.LinkProgram(_handle);
            //Check for linking errors.
            _gl.GetProgram(_handle, GLEnum.LinkStatus, out var status);
            if (status == 0)
            {
                throw new Exception($"Program failed to link with error: {_gl.GetProgramInfoLog(_handle)}");
            }
            //Detach and delete the shaders
            _gl.DetachShader(_handle, vertex);
            _gl.DetachShader(_handle, fragment);
            _gl.DeleteShader(vertex);
            _gl.DeleteShader(fragment);
        }

        /// <summary>
        /// Use the shader
        /// </summary>
        public void Use()
        {
            //Using the program
            _gl.UseProgram(_handle);
        }
        /// <summary>
        /// Uniforms are properties that applies to the entire geometry
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        /// <param name="value"></param>
        /// <exception cref="Exception">When the uniform is not found to the shader</exception>
        public unsafe void SetUniform(string name, Matrix4x4 value)
        {
            int location = _gl.GetUniformLocation(_handle, name);
            if (location == -1)
                throw new Exception($"{name} uniform not found on shader.");

            _gl.UniformMatrix4(location, 1, false, (float*)&value);
        }
        /// <summary>
        /// Uniforms are properties that applies to the entire geometry
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        /// <param name="value"></param>
        /// <exception cref="Exception">When the uniform is not found to the shader</exception>
        public unsafe void SetUniform(string name, Vector3 value)
        {
            int location = _gl.GetUniformLocation(_handle, name);
            if (location == -1)
                throw new Exception($"{name} uniform not found on shader.");

            _gl.Uniform3(location, value.X, value.Y, value.Z);
        }
        /// <summary>
        /// Uniforms are properties that applies to the entire geometry 
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        /// <param name="value"></param>
        /// <exception cref="Exception">When the uniform is not found to the shader</exception>
        public void SetUniform(string name, int value)
        {
            //Setting a uniform on a shader using a name.
            int location = _gl.GetUniformLocation(_handle, name);
            if (location == -1) //If GetUniformLocation returns -1 the uniform is not found.
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Uniforms are properties that applies to the entire geometry 
        /// </summary>
        /// <param name="name">Name of the uniform</param>
        /// <param name="value"></param>
        /// <exception cref="Exception">When the uniform is not found to the shader</exception>
        public void SetUniform(string name, float value)
        {
            int location = _gl.GetUniformLocation(_handle, name);
            if (location == -1)
            {
                throw new Exception($"{name} uniform not found on shader.");
            }
            _gl.Uniform1(location, value);
        }

        /// <summary>
        /// Dispose used resources
        /// </summary>
        public void Dispose()
        {
            //Remember to delete the program when we are done.
            _gl.DeleteProgram(_handle);
        }

        private uint LoadShader(ShaderType type, string path)
        {
            //To load a single shader we need to:
            //1) Load the shader from a file.
            //2) Create the handle.
            //3) Upload the source to opengl.
            //4) Compile the shader.
            //5) Check for errors.
            string src = File.ReadAllText(path);
            uint handle = _gl.CreateShader(type);
            _gl.ShaderSource(handle, src);
            _gl.CompileShader(handle);
            string infoLog = _gl.GetShaderInfoLog(handle);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            return handle;
        }
    }