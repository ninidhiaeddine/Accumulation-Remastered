using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorManagement
{
    public enum ColorOrder
    {
        Current,
        Next
    };

    public class CubesPainter : MonoBehaviour, IEventHandler
    {
        [Header("Initial Cubes References")]
        public List<GameObject> initialCubesToColor;

        [Header("Color Settings")]
        public int numberOfColors = 15;     // recommended value for this game
        public float saturation = 0.75f;    // recommended value for this game
        public float vibrance = 1.0f;       // recommended value for this game
        public bool useOldEndColor = true;  // this implies that the new generated palette will use the latest endColor of the previous palette as a starting point for the new palette

        // helper variables:
        private List<Color> colors;
        private int colorIndex;

        // singleton:
        public static CubesPainter instance;

        private void Awake()
        {
            // enforce singleton:
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        void Start()
        {
            InitializeEventHandlers();
            InitializeColors();
            ColorInitialCubes();
        }

        // interface methods:

        public void InitializeEventHandlers()
        {
            GameEvents.SlicedEvent.AddListener(SlicedEventHandler);
            GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
        }

        // helper methods:

        private void InitializeColors()
        {
            GenerateNewPaletteAndNotify();
        }

        private void InvokeEvents()
        {
            GameEvents.GeneratedPaletteEvent.Invoke(this.colors);
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
                        GenerateNewPaletteAndNotify();

                        // attempt to get next color again:
                        return GetNextColor();
                    }

                default:
                    throw new System.Exception("Unexpected value.");
            }
        }

        private void GenerateNewPaletteAndNotify()
        {
            // generate new palette:
            if (useOldEndColor && this.colors != null)
            {
                int size = this.colors.Count;
                Color oldEndColor = this.colors[size - 1];
                GenerateNewPalette(oldEndColor);
            }
            else
            {
                GenerateNewPalette();
            }
            
            // notify:
            InvokeEvents();
        }

        private void GenerateNewPalette()
        {
            this.colors = GradientPaletteGenerator.GenerateRandom(numberOfColors, saturation, vibrance);
            this.colorIndex = 0;
        }

        private void GenerateNewPalette(Color startColor)
        {
            this.colors = GradientPaletteGenerator.Generate(startColor, numberOfColors, saturation, vibrance);
            this.colorIndex = 0;
        }

        private void ColorCube(GameObject cube, ColorOrder colorOrder)
        {
            if (cube != null)
                cube.GetComponent<Renderer>().material.color = GetColor(colorOrder);
        }

        // event handlers:

        private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube)
        {
            ColorCube(staticCube, ColorOrder.Current);
            ColorCube(fallingCube, ColorOrder.Current);
        }

        private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
        {
            // get hierarchy:
            HoveringParentHierarchy hierarchy = HoveringCubeHelper.GetHierarchy(hoveringCubeParent);

            // get reference to mesh:
            GameObject mesh = hierarchy.MeshContainer;
            ColorCube(mesh, ColorOrder.Next);
        }
    }
}