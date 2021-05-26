using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCubeBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameEvents.DroppedAndCollidedEvent.Invoke(this.gameObject);

        // remove rigidbody:
        Destroy(this.gameObject.GetComponent<Rigidbody>());
    }
}
