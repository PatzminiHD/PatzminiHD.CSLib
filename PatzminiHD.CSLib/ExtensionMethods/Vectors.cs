using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.ExtensionMethods
{
    /// <summary>
    /// Contains Extension Methods for Vectors
    /// </summary>
    public static class Vectors
    {
        /// <summary>
        /// Smoothly transition a <see cref="Vector3"/> from one Position to another<br/>
        /// When used as an Extension Method, the Vector is modified in place
        /// </summary>
        /// <param name="startPosition">The Start vector</param>
        /// <param name="targetPosition">The target vector</param>
        /// <param name="deltaTime">The delta time, used for animations</param>
        /// <param name="speed">The speed at which the transition should occur, default value is 1.0f</param>
        /// <param name="snapDistance">The difference between the start and target position at which the positions is just snapped</param>
        /// <returns>True if the Transition has finished, otherwise false</returns>
        public static bool TransitionTo(ref this Vector3 startPosition, Vector3 targetPosition, double deltaTime, float speed = 1f, float snapDistance = 0.01f)
        {
            float cameraXOffset = targetPosition.X - startPosition.X;
            float cameraYOffset = targetPosition.Y - startPosition.Y;
            float cameraZOffset = targetPosition.Z - startPosition.Z;

            var internalSpeed = (float)deltaTime / (speed / 500);
            if (internalSpeed < 1)
            {
                internalSpeed = 1;
            }

            startPosition = new Vector3(startPosition.X + cameraXOffset / ((float)deltaTime / (speed / 500)),
                                   startPosition.Y + cameraYOffset / ((float)deltaTime / (speed / 500)),
                                   startPosition.Z + cameraZOffset / ((float)deltaTime / (speed / 500)));


            //Snap to position if offset is small enough
            if (System.Math.Abs(cameraXOffset) < snapDistance)
                startPosition = new Vector3(targetPosition.X, startPosition.Y, startPosition.Z);
            if (System.Math.Abs(cameraYOffset) < snapDistance)
                startPosition = new Vector3(startPosition.X, targetPosition.Y, startPosition.Z);
            if (System.Math.Abs(cameraZOffset) < snapDistance)
                startPosition = new Vector3(startPosition.X, startPosition.Y, targetPosition.Z);

            return startPosition == targetPosition;
        }
    }
}
