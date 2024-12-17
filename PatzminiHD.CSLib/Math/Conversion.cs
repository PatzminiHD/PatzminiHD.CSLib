namespace PatzminiHD.CSLib.Math;

/// <summary>
/// Class containing Methods for converting between units
/// </summary>
public static class Conversion
{
    /// <summary>
    /// Convert degrees to radians
    /// </summary>
    /// <param name="degrees">The degrees you want to convert</param>
    /// <returns>Input converted to radians</returns>
    public static double DegreesToRadians(double degrees)
    {
        return degrees * (System.Math.PI / 180.0);
    }
    
    /// <summary>
    /// Convert degrees to radians
    /// </summary>
    /// <param name="degrees">The degrees you want to convert</param>
    /// <returns>Input converted to radians</returns>
    public static float DegreesToRadians(float degrees)
    {
        return (float)(degrees * (System.Math.PI / 180.0));
    }

    /// <summary>
    /// Convert radians to degrees
    /// </summary>
    /// <param name="radians">The radians you want to convert</param>
    /// <returns>Input converted to degrees</returns>
    public static double RadiansToDegrees(double radians)
    {
        return radians * (180.0 / System.Math.PI);
    }
    /// <summary>
    /// Convert radians to degrees
    /// </summary>
    /// <param name="radians">The radians you want to convert</param>
    /// <returns>Input converted to degrees</returns>
    public static float RadiansToDegrees(float radians)
    {
        return (float)(radians * (180.0 / System.Math.PI));
    }
}