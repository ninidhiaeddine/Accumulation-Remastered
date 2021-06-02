using UnityEngine;

public class DistanceApproximator
{
    // helper methods:

    public static float ComputeHorizontalDistance(Transform transform1, Transform transform2)
    {
        Vector2 firstPos = new Vector2(
            transform1.transform.position.x,
            transform1.transform.position.z
            );
        Vector2 secondPos = new Vector2(
            transform2.transform.position.x,
            transform2.transform.position.z
            );
        return Vector2.Distance(firstPos, secondPos);
    }

    public static void AlignCubesHorizonally(Transform droppedCubeTransform, Transform cubeBelowDroppedCubeTransform)
    {
        // retrieve positions:
        Vector3 droppedCubePos = droppedCubeTransform.position;
        Vector3 cubeBelowDroppedCubePos = cubeBelowDroppedCubeTransform.position;

        // compute new position for dropped cube:
        Vector3 droppedCubeNewPos = new Vector3(
            cubeBelowDroppedCubePos.x,
            droppedCubePos.y,
            cubeBelowDroppedCubePos.z
            );

        // set new position:
        droppedCubeTransform.position = droppedCubeNewPos;
    }

    public static bool IsPerfectDrop(Transform droppedCubeTransform, Transform cubeBelowDroppedCubeTransform, float errorMagnitude)
    {
        return (ComputeHorizontalDistance(droppedCubeTransform, cubeBelowDroppedCubeTransform) <= errorMagnitude);
    }
}
