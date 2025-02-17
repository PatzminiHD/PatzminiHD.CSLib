using System.IO;
using PatzminiHD.CSLib.ExtensionMethods;
using static GCode.GCodeFragments;

/// <summary> Represents a GCode file </summary>
public class GCode
{
    private List<string> _gcode = new();
    private int _hotendTemp = -1;
    private int _bedTemp = -1;
    private double _xMin = -1, _yMin = -1, _xMax = -1, _yMax = -1;

    public GCode(double xMin, double yMin, double xMax, double yMax, int hotendTemp = -1, int bedTemp = -1)
    {
        Init(xMin, yMin, xMax, yMax, hotendTemp, bedTemp);
    }

    public GCode() { }

    /// <summary> Homes all axes, optionally start heating hotend and bed </summary>
    public void Init(double xMin, double yMin, double xMax, double yMax, int hotendTemp = -1, int bedTemp = -1)
    {
        _xMin = xMin; _yMin = yMin; _xMax = xMax; _yMax = yMax;
        SetHeat(hotendTemp, bedTemp);
        HomeAxes();
    }

    /// <summary> Homes all axes, optionally start heating hotend and bed </summary>
    public void Init(int hotendTemp = -1, int bedTemp = -1)
    {
        Init(-1, -1, -1, -1, hotendTemp, bedTemp);
    }

    /// <summary> Home all axes </summary>
    public void HomeAxes()
    {
        _gcode.Add(HOME_AXES);
    }

    /// <summary> Sets the heat of the hotend and bed in °C. -1 to not start heating </summary>
    /// <param name="hotendTemp"> in °C </param>
    /// <param name="bedTemp"> in °C </param>
    public void SetHeat(int hotendTemp = -1, int bedTemp = -1)
    {
        _hotendTemp = hotendTemp;
        _bedTemp = bedTemp;

        if(hotendTemp != -1)
            _gcode.Add(SET_HOTEND_TEMP.Interpolate(hotendTemp));

        if(bedTemp != -1)
            _gcode.Add(SET_BED_TEMP.Interpolate(bedTemp));
    }

    /// <summary> Wait for the heat set in <see cref="SetHeat(int, int)"/> or <see cref="Init(int, int)"/> to be reached </summary>
    public void WaitHeat()
    {
        if(_hotendTemp != -1)
            _gcode.Add(WAIT_HOTEND_TEMP.Interpolate(_hotendTemp));

        if(_bedTemp != -1)
            _gcode.Add(WAIT_BED_TEMP.Interpolate(_bedTemp));
    }

    /// <summary> Set the movement speed </summary>
    public void SetSpeed(int speed)
    {
        _gcode.Add(G0_SET_MOVESPEED.Interpolate(speed));
    }

    /// <summary> Move to a specified X location </summary>
    public void MoveX(double x) { _gcode.Add(G0_MOVE_X.Interpolate(x)); }

    /// <summary> Move to a specified Y location </summary>
    public void MoveY(double y) { _gcode.Add(G0_MOVE_Y.Interpolate(y)); }

    /// <summary> Move to a specified Z location </summary>
    public void MoveZ(double z) { _gcode.Add(G0_MOVE_Z.Interpolate(z)); }

    /// <summary> Move to a specified X, Y location </summary>
    public void MoveXY(double x, double y) { _gcode.Add(G0_MOVE_XY.Interpolate(x, y)); }

    /// <summary> Move to a specified Y, Z location </summary>
    public void MoveYZ(double y, double z) { _gcode.Add(G0_MOVE_YZ.Interpolate(y, z)); }

    /// <summary> Move to a specified X, Z location </summary>
    public void MoveXZ(double x, double z) { _gcode.Add(G0_MOVE_XZ.Interpolate(x, z)); }

    /// <summary> Move to a specified X, Y, Z location </summary>
    public void MoveXYZ(double x, double y, double z) { _gcode.Add(G0_MOVE_XYZ.Interpolate(x, y, z)); }

    /// <summary> Write a message to the LCD Screen </summary>
    public void SetLCDMessage(string message) { _gcode.Add(SET_LCD_MESSAGE.Interpolate(message)); }

    /// <summary> Pause the print </summary>
    public void PausePrint(string message) { _gcode.Add(PAUSE_PRINT.Interpolate(message)); }

    /// <summary> Add custom gcode </summary>
    public void AddCustom(string gcode) { _gcode.Add(gcode); }

    /// <summary> Write all gcode to a file </summary>
    /// <param name="filename">The path and name of the output file</param>
    public void WriteToFile(string filename)
    {
        File.WriteAllLines(filename, _gcode);
    }


    /// <summary>
    /// Contains GCode strings
    /// </summary>
    public static class GCodeFragments
    {
        /// <summary> Home all axis </summary>
        public static readonly string HOME_AXES = "G28";

        /// <summary> G0 F{0} </summary>
        public static readonly string G0_SET_MOVESPEED = "G0 F{0}";

        /// <summary> G0 X{0} </summary>
        public static readonly string G0_MOVE_X = "G0 X{0}";

        /// <summary> G0 Y{0} </summary>
        public static readonly string G0_MOVE_Y = "G0 Y{0}";

        /// <summary> G0 Z{0} </summary>
        public static readonly string G0_MOVE_Z = "G0 Z{0}";

        /// <summary> G0 X{0} Y{1} </summary>
        public static readonly string G0_MOVE_XY = "G0 X{0} Y{1}";

        /// <summary> G0 Y{0} Z{0} </summary>
        public static readonly string G0_MOVE_YZ = "G0 Y{0} Z{1}";

        /// <summary> G0 X{0} Z{1} </summary>
        public static readonly string G0_MOVE_XZ = "G0 X{0} Z{1}";

        /// <summary> G0 X{0} Y{1} Z{2} </summary>
        public static readonly string G0_MOVE_XYZ = "G0 X{0} Y{1} Z{2}";

        /// <summary> M104 S{0} </summary>
        public static readonly string SET_HOTEND_TEMP = "M104 S{0}";

        /// <summary> M140 S{0} </summary>
        public static readonly string SET_BED_TEMP = "M140 S{0}";

        /// <summary> M109 S{0} </summary>
        public static readonly string WAIT_HOTEND_TEMP = "M109 S{0}";

        /// <summary> M190 S{0} </summary>
        public static readonly string WAIT_BED_TEMP = "M190 S{0}";

        /// <summary> M117 {0} </summary>
        public static readonly string SET_LCD_MESSAGE = "M117 {0}";

        /// <summary> M0 {0} </summary>
        public static readonly string PAUSE_PRINT = "M0 {0}";
    }
}