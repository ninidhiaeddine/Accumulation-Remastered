using System.Collections;
using UnityEngine;

public class CameraSize : MonoBehaviour, IEventHandler
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public float minOrthographicSize = 7.0f;
    public float maxOrthographicSize = 20.0f;
    public float transitionDuration = 1.0f;

    // singleton:
    public static CameraSize Singleton { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void SetOrthographicSize(IHoveringParentHierarchy hoveringParentHierarchy)
    {
        // retrieve scale:
        Vector3 hoveringCubeScale = hoveringParentHierarchy.Scale;

        // compute size:
        float size = hoveringCubeScale.x > hoveringCubeScale.z ? hoveringCubeScale.x : hoveringCubeScale.z;
        size = Mathf.Clamp(size, minOrthographicSize, maxOrthographicSize);

        // lineraly interpolate size:
        StartCoroutine(LerpSize(size, transitionDuration));
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        IHoveringParentHierarchy hierarchy = hoveringCubeParent.GetComponent<IHoveringParentHierarchy>();
        SetOrthographicSize(hierarchy);
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
