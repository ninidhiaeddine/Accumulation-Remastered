using System.Collections;
using UnityEngine;

public class SmoothResizer : MonoBehaviour
{
    public GameObject TargetGameObject { get; set; }
    public Vector3 TargetScale { get; set; }
    public float TransitionDuration { get; set; }

    // helper variables:
    private bool isInitialized;
    private Vector3 initalScale;

    public void SetSettings(GameObject target, Vector3 targetScale, float transitionDuration)
    {
        TargetGameObject = target;
        TargetScale = targetScale;
        TransitionDuration = transitionDuration;

        initalScale = TargetGameObject.gameObject.transform.localScale;
        isInitialized = true;
    }

    public void StartSmoothResizing()
    {
        if (isInitialized)
            StartCoroutine(Resize());
        else
            throw new System.Exception("Settings are not initialized yet!");
    }

    // private coroutine:

    private IEnumerator Resize()
    {
        float elapsed = 0.0f;
        float t = 0.0f;

        while (elapsed < TransitionDuration)
        {
            // retrieve current scale:
            Vector3 currentScale = TargetGameObject.transform.localScale;

            // compute new scale:
            Vector3 newScale = Vector3.Lerp(initalScale, TargetScale, t);

            // set new scale:
            TargetGameObject.transform.localScale = newScale;

            // wait for end of frame:
            yield return new WaitForEndOfFrame();
            elapsed += Time.deltaTime;
            t = elapsed / TransitionDuration;
        }
    }
}
