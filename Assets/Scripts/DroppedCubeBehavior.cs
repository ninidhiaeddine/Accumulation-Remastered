using UnityEngine;

public class DroppedCubeBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // invoke event:
        GameEvents.DroppedAndCollidedEvent.Invoke(this.gameObject, collision.gameObject);

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
