using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorManagement
{
    public class BackgroundPainter : MonoBehaviour, IEventHandler
    {
        // references:
        public PlayerManager playerManager;
        public Camera camera;

        [Header("HSV Settings")]
        public float hueToAdd = 0.0f;
        public float saturationToAdd = 0.0f;
        public float vibranceToAdd = -0.5f;

        [Header("Transition Settings")]
        public float transitionDuration = 5.0f;

        void Start()
        {
            InitializeEventHandlers();
        }

        // helper methods:

        public void InitializeEventHandlers()
        {
            GameEvents.GeneratedPaletteEvent.AddListener(GeneratedPaletteEventHandler);
        }

        private void GetStartAndEndColors(List<Color> colorPalette, out Color startColor, out Color endColor)
        {
            // get size:
            int size = colorPalette.Count;

            // get colors:
            startColor = colorPalette[0];
            endColor = colorPalette[size - 1];
        }

        private Color ComputeAverageBackgroundColor(List<Color> colorPalette)
        {
            // get startColor and endColor
            Color startColor, endColor;
            GetStartAndEndColors(colorPalette, out startColor, out endColor);

            // get color average:
            Color average = ColorHelper.GetAverage(startColor, endColor);

            // tweak HSV values:
            return ColorHelper.ChangeHSV(average, this.hueToAdd, this.saturationToAdd, this.vibranceToAdd);
        }

        private void PaintBackgroundAndFogColor(Color color)
        {
            Color startColor = this.camera.backgroundColor;
            StartCoroutine(LerpBackgroundAndFogColor(startColor, color, this.transitionDuration));
        }

        private void SetBackgroundColor(Color color)
        {
            this.camera.backgroundColor = color;
        }

        private void SetFogColor(Color color)
        {
            RenderSettings.fogColor = color;
        }

        // event handlers:

        private void GeneratedPaletteEventHandler(List<Color> colorPalette, Player sender)
        {
            if (playerManager.EventShouldBeApproved(sender))
            {
                // compute background color:
                Color backgroundColor = ComputeAverageBackgroundColor(colorPalette);

                // paint background and fog color:
                PaintBackgroundAndFogColor(backgroundColor);
            }
        }

        // Coroutines:

        IEnumerator LerpBackgroundAndFogColor(Color startColor, Color endColor, float duration)
        {
            float t = 0.0f;
            Color output;

            while (t < 1)
            {
                // lerp color:
                output = Color.Lerp(startColor, endColor, t);

                // set colors:
                SetBackgroundColor(output);
                SetFogColor(output);

                // wait for end of frame:
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime / duration;
            }
        }
    }
}
