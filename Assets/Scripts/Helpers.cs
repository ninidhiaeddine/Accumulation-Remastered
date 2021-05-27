using UnityEngine;

public class Helpers
{
    public static bool GameObjectIsInTheAir(GameObject gameObject)
    {
        Vector3 origin = gameObject.transform.position;
        Vector3 direction = Vector3.down;

        Ray ray = new Ray(origin, direction);

        return !Physics.Raycast(ray, maxDistance: 1.0f);
    }
}
