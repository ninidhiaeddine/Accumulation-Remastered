using System.Collections;
using UnityEngine;

public class CameraSize : MonoBehaviour, IEventListener
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public float minOrthographicSize = 7.0f;
    public float maxOrthographicSize = 20.0f;
    public float sizeTransitionDuration = 1.0f;

    // singleton:
    public static CameraSize instance;

    private void Awake()
    {
        // enforce singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        InitializeEventListeners();
    }

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void SetOrthographicSize(GameObject hoveringCubeParent)
    {
        // get child:
        GameObject child = hoveringCubeParent.transform.GetChild(0).gameObject;

        // retrieve child's scale:
        Vector3 hoveringCubeScale = child.transform.localScale;

        // compute size:
        float size = hoveringCubeScale.x > hoveringCubeScale.z ? hoveringCubeScale.x : hoveringCubeScale.z;
        size = Mathf.Clamp(size, minOrthographicSize, maxOrthographicSize);

        // lineraly interpolate size:
        StartCoroutine(LerpSize(size, sizeTransitionDuration));
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        SetOrthographicSize(hoveringCubeParent);
    }

    // coroutines

    private IEnumerator LerpSize(float newSize, float duration)
    {
        float t = 0.0f;

        while (t < 1)
        {
            // set camera's size:
            float currentOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(currentOrthographicSize, newSize, t);

            // wait for end of frame:
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime / duration;
        }
    }
}
