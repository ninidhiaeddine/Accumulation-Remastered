using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientPaletteGenerator
{
    public static List<Color> GenerateRandom(int numberOfColors, float saturation, float vibrance)
    {
        Color startColor = Color.HSVToRGB(H: Random.Range(0.0f, 1.0f), S: saturation, V: vibrance);
        Color endColor = Color.HSVToRGB(H: Random.Range(0.0f, 1.0f), S: saturation, V: vibrance);

        return GetGradientColors(startColor, endColor, numberOfColors);
    }

    public static List<Color> GetGradientColors(Color startColor, Color endColor, int steps)
    {
        // verification:
        if (steps < 2)
            throw new System.Exception("Cannot generate gradient with 1 color or less. Number of colors must be > 1");

        // declaration:
        List<Color> colors = new List<Color>();
        float r, g, b, coeff;

        for (int i = 0; i < steps + 1; i++)
        {
            // linear interpolation:
            coeff = (float)i / (float)steps;
            r = startColor.r + coeff * (endColor.r - startColor.r);
            g = startColor.g + coeff * (endColor.g - startColor.g);
            b = startColor.b + coeff * (endColor.b - startColor.b);

            // construct color using computed RGB:
            Color color = new Color(r, g, b);
            

            // add color to list:
            colors.Add(color);
        }

        // return list:
        return colors;
    }
}
