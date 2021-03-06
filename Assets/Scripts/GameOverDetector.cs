using UnityEngine;

public class GameOverDetector : MonoBehaviour, IEventHandler
{
    public Vector3 detectorOffsetPosition;

    void Start()
    {
        InitializeEventHandlers();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            InvokeGameOverEvent();
    }

    // helper methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    private void UpdateDetectorPosition(GameObject hoveringCubeParent)
    {
        // retrieve hovering cube position:
        Vector3 hoveringCubePos = hoveringCubeParent.transform.position;

        // compute new detector's position:
        Vector3 gameOverDetectorPos = hoveringCubePos + detectorOffsetPosition;

        // update position:
        transform.position = gameOverDetectorPos;
    }

    private void InvokeGameOverEvent()
    {
        GameEvents.GameOverEvent.Invoke();
        Debug.Log("Game Over");
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        UpdateDetectorPosition(hoveringCubeParent);
    }
}
