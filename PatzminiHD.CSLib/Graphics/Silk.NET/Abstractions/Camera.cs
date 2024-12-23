using System.Numerics;
using PatzminiHD.CSLib.ExtensionMethods;

namespace PatzminiHD.CSLib.Graphics.Silk.NET.Abstractions;

/// <summary>
/// Abstraction for a Camera<br/><br/>
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
public class Camera
{
    private Vector3 position;
    /// <summary> Position of the camera </summary>
    public Vector3 Position { get { return position; } set { position = value; } }
    /// <summary> Vector Pointing to the front of the camera </summary>
    public Vector3 Front { get; set; }
    /// <summary> Vector pointing upwards from the camera </summary>
    public Vector3 Up { get; set; }
    /// <summary> Aspect ratio of the screen </summary>
    public float AspectRatio { get; set; }
    
    /// <summary> Yaw of the camera </summary>
    public float Yaw { get; set; } = -90f;
    /// <summary> Pitch of the camera </summary>
    public float Pitch { get; set; }

    /// <summary> True if the <see cref="MoveToPosition(double, Vector3, float, float)"/> Method has been called and the camera
    ///           has not yet fully returned to center </summary>
    public bool IsMovingToPosition { get; private set; }

    /// <summary> Zoom of the camera </summary>
    private float _zoom = 45f;

    /// <summary>
    /// Create a new camera object
    /// </summary>
    /// <param name="position">Position of the camera</param>
    /// <param name="front">Vector pointing to the front of the camera</param>
    /// <param name="up">Vector pointing upwards from the camera</param>
    /// <param name="aspectRatio">Aspect ratio of the screen</param>
    public Camera(Vector3 position, Vector3 front, Vector3 up, float aspectRatio)
    {
        Position = position;
        Front = front;
        Up = up;
        AspectRatio = aspectRatio;
    }

    /// <summary>
    /// Modify the zoom of the camera
    /// </summary>
    /// <param name="zoomAmount">The amount of zoom</param>
    public void ModifyZoom(float zoomAmount)
    {
        _zoom = System.Math.Clamp(_zoom - zoomAmount, 1.0f, 45f);
    }

    /// <summary>
    /// Modify the direction the camera is looking in
    /// </summary>
    /// <param name="xOffset">How much to change the Yaw</param>
    /// <param name="yOffset">How much to change the Pitch</param>
    public void ModifyDirection(float xOffset, float yOffset)
    {
        Yaw += xOffset;
        Pitch += yOffset;

        Pitch = System.Math.Clamp(Pitch, -89f, 89f);

        var cameraDirection = Vector3.Zero;
        cameraDirection.X = MathF.Cos(Math.Conversion.DegreesToRadians(Yaw)) *
                            MathF.Cos(Math.Conversion.DegreesToRadians(Pitch));
        cameraDirection.Y = MathF.Sin(Math.Conversion.DegreesToRadians(Pitch));
        cameraDirection.Z = MathF.Sin(Math.Conversion.DegreesToRadians(Yaw)) *
                            MathF.Cos(Math.Conversion.DegreesToRadians(Pitch));

        Front = Vector3.Normalize(cameraDirection);
    }
    /// <summary>
    /// Get the View Matrix
    /// </summary>
    /// <returns>The View Matrix</returns>
    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(Position, Position + Front, Up);
    }
    /// <summary>
    /// Get the Projection Matrix
    /// </summary>
    /// <returns>The Projection Matrix</returns>
    public Matrix4x4 GetProjectionMatrix()
    {
        var test = Matrix4x4.CreateOrthographicOffCenter(-AspectRatio, AspectRatio, -1f, 1f, -1f, 1f);
        var returnValue = Matrix4x4.CreatePerspectiveFieldOfView(Math.Conversion.DegreesToRadians(_zoom), AspectRatio, 0.1f, 100f);
        return returnValue;
        //return Matrix4x4.CreateOrthographicOffCenter(-AspectRatio, AspectRatio, -1f, 1f, -1f, 1f);
    }


    /// <summary>
    /// Move the camera smoothly to a target position. Needs to be called in a Update function until <see cref="IsMovingToPosition"/> is false
    /// </summary>
    /// <param name="deltaTime">The delta time of the Update function</param>
    /// <param name="targetPosition">The target position of the movement</param>
    /// <param name="speed">How fast the camera should move</param>
    /// <param name="snapDistance"></param>
    /// <returns></returns>
    public bool MoveToPosition(double deltaTime, Vector3 targetPosition, float speed = 1f, float snapDistance = 0.01f)
    {
        IsMovingToPosition = true;

        if(position.TransitionTo(targetPosition, deltaTime, speed, snapDistance))
        {
            IsMovingToPosition = false;
            return true;
        }

        return false;
    }
}