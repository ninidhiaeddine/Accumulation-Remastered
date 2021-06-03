using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SmoothResizer))]
public class CubeSmoothResizer : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;

    // settings:
    public int resizeCubeAfterPerfectDrops = 2;
    public Vector3 scaleToAdd;
    public float transitionDuration;

    // helper variable:
    private GameObject droppedCube;
    private GameObject hoveringCubeParent;
    private SmoothResizer[] smoothResizers;

    void Start()
    {
        InitializeComponents();
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.PerfectDropEvent.AddListener(PerfectDropEventHandler);
        GameEvents.PerfectDropCounterUpdatedEvent.AddListener(PerfectDropCounterUpdatedEventHandler);
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
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

    private void PerfectDropEventHandler(GameObject staticCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            this.droppedCube = staticCube;
    }

    private void PerfectDropCounterUpdatedEventHandler(int count, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            if (count >= this.resizeCubeAfterPerfectDrops)
                StartCoroutine(ResizeCubes());
        }
    }

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
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
        IHoveringParentHierarchy hierarchy = this.hoveringCubeParent.GetComponent<IHoveringParentHierarchy>();
        GameObject scaler = hierarchy.ScalerContainer;
        SmoothResize(scaler, smoothResizers[1]);
    }
}
