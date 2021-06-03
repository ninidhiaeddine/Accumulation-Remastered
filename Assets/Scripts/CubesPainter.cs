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
        // references:
        public PlayerManager playerManager;

        [Header("Initial Cubes References")]
        public List<Renderer> initialCubesToColor;
        public float initialColoringDuration = 0.5f;

        [Header("Color Settings")]
        public int numberOfColors = 15;     // recommended value for this game
        public float saturation = 0.75f;    // recommended value for this game
        public float vibrance = 1.0f;       // recommended value for this game
        public bool useOldEndColor = true;  // this implies that the new generated palette will use the latest endColor of the previous palette as a starting point for the new palette
        public int emitColorsAfterPerfectDrops = 2;

        // helper variables:
        private List<Color> colors;
        private int colorIndex;

        private GameObject perfectDropCube;

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
            GameEvents.PerfectDropEvent.AddListener(PerfectDropEventHandler);
            GameEvents.PerfectDropCounterUpdatedEvent.AddListener(PerfectDropCounterUpdatedEventHandler);
            GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
        }

        // helper methods:

        private void InitializeColors()
        {
            GenerateNewPaletteAndNotify();
        }

        private void InvokeEvents()
        {
            GameEvents.GeneratedPaletteEvent.Invoke(this.colors, playerManager.playerToLookFor);
        }

        private void ColorInitialCubes()
        {
            for (int i = 0; i < initialCubesToColor.Count; i++)
            {
                StartCoroutine(LerpCubeColor(
                    cubeRenderer: initialCubesToColor[i],
                    startColor: Color.white,
                    endColor: GetColor(ColorOrder.Next),
                    duration: initialColoringDuration
                    ));
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

        private void ColorCube(Renderer cubeRenderer, ColorOrder colorOrder)
        {
            if (cubeRenderer != null)
                cubeRenderer.material.color = GetColor(colorOrder);
        }

        private void MakeCubeEmissive(Renderer cubeRenderer)
        {
            // retrieve color:
            Color color = cubeRenderer.material.color;

            // make emission color (a darker one than the original color):
            Color emissionColor = ColorHelper.ChangeHSV(color, 0, 0, -0.5f);

            // set emissive color:
            cubeRenderer.material.SetColor("_EmissionColor", emissionColor);

            // enable emission:
            cubeRenderer.material.EnableKeyword("_EMISSION");
        }

        // event handlers:

        private void PerfectDropEventHandler(GameObject staticCube, Player sender)
        {
            if (playerManager.EventShouldBeApproved(sender))
            {
                // store reference to the cube:
                this.perfectDropCube = staticCube;
            }
        }

        private void PerfectDropCounterUpdatedEventHandler(int count, Player sender)
        {
            if (playerManager.EventShouldBeApproved(sender))
            {
                if (count >= this.emitColorsAfterPerfectDrops)
                {
                    // make material emissive:
                    MakeCubeEmissive(this.perfectDropCube.GetComponent<Renderer>());
                }
            }
        }

        private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube, Player sender)
        {
            if (playerManager.EventShouldBeApproved(sender))
            {
                ColorCube(staticCube.GetComponent<Renderer>(), ColorOrder.Current);
                ColorCube(fallingCube.GetComponent<Renderer>(), ColorOrder.Current);
            }
        }

        private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent, Player sender)
        {
            if (playerManager.EventShouldBeApproved(sender))
            {
                // get hierarchy:
                HoveringParentHierarchy hierarchy = HoveringCubeHelper.GetHierarchy(hoveringCubeParent);

                // get reference to cube renderer:
                Renderer cubeRenderer = hierarchy.MeshContainer.GetComponent<Renderer>();
                ColorCube(cubeRenderer, ColorOrder.Next);
            }
        }

        // coroutines:

        IEnumerator LerpCubeColor(Renderer cubeRenderer, Color startColor, Color endColor, float duration)
        {
            float t = 0.0f;
            Color output;

            while (t < 1)
            {
                // lerp color:
                output = Color.Lerp(startColor, endColor, t);

                // set color:
                cubeRenderer.material.color = output;

                // wait for end of frame:
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime / duration;
            }
        }
    }
}