using System.Collections.Generic;
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
    private SmoothResizer[] smoothResizers;

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

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.PerfectDropEvent.AddListener(HandlePerfectDropEvent);
        GameEvents.PerfectDropCounterUpdatedEvent.AddListener(HandlePerfectDropCounterUpdatedEvent);
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    // helper methods:

    private void InitializeComponents()
    {
        smoothResizers = GetComponents<SmoothResizer>();
    }

    private void SmoothResize(GameObject target, SmoothResizer smoothResizer)
    {
        // compute target scale:
        Vector3 currentScale = target.transform.localScale;
        Vector3 targetScale = currentScale + scaleToAdd;

        // smooth resizer:
        smoothResizer.SetSettings(target, targetScale, transitionDuration);
        smoothResizer.StartSmoothResizing();
    }

    // event handlers:

    private void HandlePerfectDropEvent(GameObject staticCube)
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
        SmoothResize(droppedCube, smoothResizers[0]);

        // wait for one frame (wait until hoveringCube data is available)
        yield return new WaitForEndOfFrame();

        // resize hovering cube:
        GameObject hoveringCubeChild = this.hoveringCubeParent.transform.GetChild(0).gameObject;
        SmoothResize(hoveringCubeChild, smoothResizers[1]);
    }
}
