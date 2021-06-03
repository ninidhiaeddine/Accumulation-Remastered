using UnityEngine;

public class DroppedCubeBehavior : MonoBehaviour
{
    // references:
    public PlayerManager playerManager;

    private void OnCollisionEnter(Collision collision)
    {
        // invoke event:
        GameEvents.DroppedAndCollidedEvent.Invoke(this.gameObject, collision.gameObject, playerManager.playerToLookFor);

        // remove components:
        RemoveHoveringCubeComponents();
    }

    private void RemoveHoveringCubeComponents()
    {
        // remove rigidbody:
        Destroy(this.gameObject.GetComponent<Rigidbody>());

        // remove script:
        Destroy(this.gameObject.GetComponent<DroppedCubeBehavior>());
    }
}
