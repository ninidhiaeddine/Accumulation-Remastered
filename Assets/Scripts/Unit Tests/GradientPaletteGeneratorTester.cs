using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientPaletteGeneratorTester : MonoBehaviour
{
    [Header("Input")]
    public int numberOfColors = 20;     // recommended value for this game
    public float saturation = 0.75f;    // recommended value for this game
    public float vibrance = 1.0f;       // recommended value for this game

    [Header("Output")]
    public List<Color> colors;

    private void OnValidate()
    {
        GenerateColors();
    }

    private void GenerateColors()
    {
        colors = GradientPaletteGenerator.GenerateRandom(numberOfColors, saturation, vibrance);
    }
}
