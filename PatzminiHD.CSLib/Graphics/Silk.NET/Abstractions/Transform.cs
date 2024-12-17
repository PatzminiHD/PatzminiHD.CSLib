using System.Numerics;

namespace PatzminiHD.CSLib.Graphics.Silk.NET.Abstractions;

/// <summary>
/// Abstraction for a Transformation<br/><br/>
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
public class Transform
{
    /// <summary>
    /// Position Transformation
    /// </summary>
    public Vector3 Position { get; set; } = new Vector3(0, 0, 0);
    /// <summary>
    /// Scale Transformation
    /// </summary>
    public float Scale { get; set; } = 1f;
    /// <summary>
    /// Rotation Transformation
    /// </summary>
    public Quaternion Rotation { get; set; } = Quaternion.Identity;
    
    /// <summary>
    /// View Matrix
    /// </summary>
    public Matrix4x4 ViewMatrix => Matrix4x4.Identity * Matrix4x4.CreateFromQuaternion(Rotation) *
                                   Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateTranslation(Position);
}