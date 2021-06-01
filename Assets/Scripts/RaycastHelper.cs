using UnityEngine;

public class RaycastHelper
{
    public static bool IsInTheAir(Transform transform, float maxDistance)
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        Ray ray = new Ray(origin, direction);

        return !Physics.Raycast(ray, maxDistance);
    }
}
