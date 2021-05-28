using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorManagement
{
    public class ColorHelper
    {
        public static Color GetAverage(Color startColor, Color endColor)
        {
            return Color.Lerp(startColor, endColor, 0.5f);
        }

        public static Color ChangeHSV(Color color, float hueToAdd, float saturationToAdd, float vibranceToAdd)
        {
            // get h, s, v values:
            float h, s, v;
            Color.RGBToHSV(color, out h, out s, out v);

            // modify h, s, v values:
            h += hueToAdd;
            s += saturationToAdd;
            v += vibranceToAdd;

            // return final color:
            return Color.HSVToRGB(h, s, v);
        }
    }
}
