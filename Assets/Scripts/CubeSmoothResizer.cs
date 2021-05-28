using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SmoothResizer))]
public class CubeSmoothResizer : MonoBehaviour, IEventListener
{
    // settings:
    public int resizeCubeAfterPerfectDrops = 2;
    public Vector3 scaleToAdd;
    public float transitionDuration;

    // helper variable:
    private GameObject droppedCube;
    private GameObject hoveringCubeParent;
    private SmoothResizer smoothResizer;

    // singleton:
    public static CubeSmoothResizer Instance { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        InitializeComponents();
        InitializeEventListeners();
    }

    // helper methods:

    private void InitializeComponents()
    {
        smoothResizer = GetComponent<SmoothResizer>();
    }

    private void SmoothResize(GameObject target)
    {
        // compute target scale:
        Vector3 currentScale = target.transform.localScale;
        Vector3 targetScale = currentScale + scaleToAdd;

        // smooth resizer:
        smoothResizer.SetSettings(target, targetScale, transitionDuration);
        smoothResizer.StartSmoothResizing();
    }

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.DistanceApproximatedEvent.AddListener(HandleDistanceApproximatedEvent);
        GameEvents.PerfectDropCounterUpdatedEvent.AddListener(HandlePerfectDropCounterUpdatedEvent);
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    // event handlers:

    private void HandleDistanceApproximatedEvent(GameObject staticCube)
    {
        this.droppedCube = staticCube;
    }

    private void HandlePerfectDropCounterUpdatedEvent(int count)
    {
        if (count >= this.resizeCubeAfterPerfectDrops)
            StartCoroutine(ResizeCubes());
    }

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject hoveringCubeParent)
    {
        this.hoveringCubeParent = hoveringCubeParent;
    }

    // Couroutines:

    IEnumerator ResizeCubes()
    {
        // resize dropped cube:
        SmoothResize(droppedCube);

        // wait for one frame (wait until hoveringCube data is available)
        // TODO: THERE'S A BUG HERE:
        yield return new WaitForEndOfFrame();

        // resize hovering cube:
        GameObject hoveringCubeChild = this.hoveringCubeParent.transform.GetChild(0).gameObject;
        SmoothResize(hoveringCubeChild);
    }
}
