using UnityEngine;

public class DistanceApproximator : MonoBehaviour, IEventListener
{
    public float distanceThreshold = 0.1f;

    private void Start()
    {
        InitializeEventListeners();
    }

    // helper methods:

    private float ComputeHorizontalDistance(GameObject obj1, GameObject obj2)
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

    private void AlignCubesHorizonally(GameObject droppedCube, GameObject cubeBelowDroppedCube)
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

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.DroppedAndCollidedEvent.AddListener(HandleDroppedAndCollidedEvent);
    }

    // event handlers:

    private void HandleDroppedAndCollidedEvent(GameObject droppedCube, GameObject cubeBelowDroppedCube)
    {
        if (ComputeHorizontalDistance(droppedCube, cubeBelowDroppedCube) < distanceThreshold)
        {
            // align cubes:
            AlignCubesHorizonally(droppedCube, cubeBelowDroppedCube);

            // notify:
            GameEvents.DistanceApproximatedEvent.Invoke(droppedCube);
        }
    }
}
