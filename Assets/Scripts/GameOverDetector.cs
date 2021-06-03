using UnityEngine;

public class GameOverDetector : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;

    public Vector3 detectorOffsetPosition;

    void Start()
    {
        InitializeEventHandlers();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Player1") || other.CompareTag("Player2"))
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
        GameEvents.GameOverEvent.Invoke(playerManager.playerToLookFor);
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            UpdateDetectorPosition(hoveringCubeParent);
    }
}
