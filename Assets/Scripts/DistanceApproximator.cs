using UnityEngine;

public class DistanceApproximator
{
    public static float ComputeHorizontalDistance(Vector3 position1, Vector3 position2)
    {
        Vector2 firstPos = new Vector2(
            position1.x,
            position1.z
            );
        Vector2 secondPos = new Vector2(
            position2.x,
            position2.z
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

    public static bool IsPerfectDrop(Vector3 droppedCubePos, Vector3 cubeBelowDroppedCubePos, float errorMagnitude)
    {
        return (ComputeHorizontalDistance(droppedCubePos, cubeBelowDroppedCubePos) <= errorMagnitude);
    }
}
