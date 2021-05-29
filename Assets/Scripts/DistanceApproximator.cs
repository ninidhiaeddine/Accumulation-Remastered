using UnityEngine;

public class DistanceApproximator
{
    // helper methods:

    public static float ComputeHorizontalDistance(GameObject obj1, GameObject obj2)
    {
        Vector2 firstPos = new Vector2(
            obj1.transform.position.x,
            obj1.transform.position.z
            );
        Vector2 secondPos = new Vector2(
            obj2.transform.position.x,
            obj2.transform.position.z
            );
        return Vector2.Distance(firstPos, secondPos);
    }

    public static void AlignCubesHorizonally(GameObject droppedCube, GameObject cubeBelowDroppedCube)
    {
        // retrieve positions:
        Vector3 droppedCubePos = droppedCube.transform.position;
        Vector3 cubeBelowDroppedCubePos = cubeBelowDroppedCube.transform.position;

        // compute new position for dropped cube:
        Vector3 droppedCubeNewPos = new Vector3(
            cubeBelowDroppedCubePos.x,
            droppedCubePos.y,
            cubeBelowDroppedCubePos.z
            );

        // set new position:
        droppedCube.transform.position = droppedCubeNewPos;
    }

    public static bool IsPerfectDrop(GameObject droppedCube, GameObject cubeBelowDroppedCube, float errorMagnitude)
    {
        return (ComputeHorizontalDistance(droppedCube, cubeBelowDroppedCube) <= errorMagnitude);
    }
}
