using UnityEngine;

public class InvisibleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // every cube that falls and hits this trigger gets destroyed
        Destroy(other.gameObject);
    }
}
