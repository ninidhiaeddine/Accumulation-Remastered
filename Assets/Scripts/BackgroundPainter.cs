using System.Collections.Generic;
using UnityEngine;

namespace ColorManagement
{
    public class BackgroundPainter : MonoBehaviour
    {
        [Header("HSV Settings")]
        public float hueToAdd = 0.0f;
        public float saturationToAdd = 0.0f;
        public float vibranceToAdd = -0.8f;

        // singleton:
        public static BackgroundPainter instance;

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
            InitializeEventListeners();
        }

        // helper methods:

        private void InitializeEventListeners()
        {
            GameEvents.GeneratedPaletteEvent.AddListener(HandleGeneratedPaletteEvent);
        }

        private void GetStartAndEndColors(List<Color> colorPalette, out Color startColor, out Color endColor)
        {
            // get size:
            int size = colorPalette.Count;

            // get colors:
            startColor = colorPalette[0];
            endColor = colorPalette[size - 1];
        }

        private Color ComputeBackgroundColor(List<Color> colorPalette)
        {
            // get startColor and endColor
            Color startColor, endColor;
            GetStartAndEndColors(colorPalette, out startColor, out endColor);

            // get color average:
            Color average = ColorHelper.GetAverage(startColor, endColor);

            // tweak HSV values:
            return ColorHelper.ChangeHSV(average, this.hueToAdd, this.saturationToAdd, this.vibranceToAdd);
        }

        private void PaintBackgroundColor(Color color)
        {
            Camera.main.backgroundColor = color;
        }

        private void PaintFogColor(Color color)
        {
            RenderSettings.fogColor = color;
        }

        // event handlers:

        private void HandleGeneratedPaletteEvent(List<Color> colorPalette)
        {
            // compute background color:
            Color backgroundColor = ComputeBackgroundColor(colorPalette);

            // paint background color:
            PaintBackgroundColor(backgroundColor);

            // paint fog color:
            PaintFogColor(backgroundColor);
        }
    }
}
