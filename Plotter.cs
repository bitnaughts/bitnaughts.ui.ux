using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class Plotter
{
    // public string Plot(string type, float value, float min, float max, int length)
    // {
    //     string output = "";
    //     string style = "░▒▓█" + "─┼├┤" + "╔╚";
    //     float marker;
    //     int i = 0;
    //     switch (type)
    //     {
    //         case "ProgressBar":
    //             marker = Mathf.Clamp(value.Remap(min, max, 0, length), 0, length);
    //             if (float.IsNaN(marker)) return "";
    //             // if (marker == length) return style[3].ToString().Repeat(length);
    //             return (marker % 1 > 0) ? 
    //                 style[3].ToString().Repeat(Mathf.FloorToInt(marker)) + style[(int) (4 * (marker % 1))] + ' '.ToString().Repeat(Mathf.FloorToInt(length - marker)) : 
    //                 style[3].ToString().Repeat(Mathf.FloorToInt(marker)) + ' '.ToString().Repeat(Mathf.FloorToInt(length - marker));
    //         case "Marker":
    //             marker = Mathf.Clamp(value.Remap(min, max, 0, length), 0, length - .01f);
    //             if (float.IsNaN(marker)) return "";
    //             return new StringBuilder(style[4].ToString().Repeat(length)) 
    //             { 
    //                 [(int)marker] = style[5] 
    //             }.ToString();
    //     }
    //     return "";
    // }
}