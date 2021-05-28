using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorOrder
{
    Current,
    Next
};

public class CubesPainter : MonoBehaviour
{
    [Header("Initial Cubes References")]
    public List<GameObject> initialCubesToColor;

    [Header("Color Settings")]
    public int numberOfColors = 15;     // recommended value for this game
    public float saturation = 0.75f;    // recommended value for this game
    public float vibrance = 1.0f;       // recommended value for this game

    // helper variables:
    private List<Color> colors;
    private int colorIndex;

    void Start()
    {
        InitializeEventListeners();
        InitializeColors();
        ColorInitialCubes();
    }

    // helper methods:

    private void InitializeEventListeners()
    {
        GameEvents.DroppedAndSlicedEvent.AddListener(HandleDroppedAndSlicedEvent);
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    private void InitializeColors()
    {
        GenerateNewPalette();
    }

    private void ColorInitialCubes()
    {
        for (int i = 0; i < initialCubesToColor.Count; i++)
        {
            ColorCube(initialCubesToColor[i], ColorOrder.Next);
        }
    }

    private Color GetCurrentColor()
    {
        return colors[colorIndex - 1];
    }

    private Color GetNextColor()
    {
        if (this.colorIndex >= this.colors.Count)
            throw new System.Exception("Color Index is out of bounds!");

        Color result = this.colors[this.colorIndex];
        this.colorIndex++;

        return result;
    }

    private Color GetColor(ColorOrder colorOrder)
    {
        switch (colorOrder)
        {
            case ColorOrder.Current:
                return GetCurrentColor();

            case ColorOrder.Next:
                try
                {
                    return GetNextColor();
                }
                catch (System.Exception)
                {
                    GenerateNewPalette();

                    // attempt to get next color again:
                    return GetNextColor();
                }

            default:
                throw new System.Exception("Unexpected value.");
        }
    }

    private void GenerateNewPalette()
    {
        this.colors = GradientPaletteGenerator.GenerateRandom(numberOfColors, saturation, vibrance);
        this.colorIndex = 0;
    }

    private void ColorCube(GameObject cube, ColorOrder colorOrder)
    {
        cube.GetComponent<Renderer>().material.color = GetColor(colorOrder);
    }

    // event handlers:

    private void HandleDroppedAndSlicedEvent(GameObject staticCube, GameObject fallingCube)
    {
        ColorCube(staticCube, ColorOrder.Current);
        ColorCube(fallingCube, ColorOrder.Current);
    }

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject hoveringCubeParent)
    {
        GameObject child = hoveringCubeParent.transform.GetChild(0).gameObject;
        ColorCube(child, ColorOrder.Next);
    }
}
