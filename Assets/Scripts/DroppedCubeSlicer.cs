using UnityEngine;

public class DroppedCubeSlicer : MonoBehaviour, IEventListener
{
    public float errorMagnitude = 0.1f; 

    // helper variables:
    public GameObject[] slicedCubes;
    private bool isGameOver = false;

    // singleton:
    public static DroppedCubeSlicer Instance { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        InitializeEventListeners();
    }

    // helper methods:

    public void InitializeEventListeners()
    {
        GameEvents.GameOverEvent.AddListener(GameOverEventHandler);
        GameEvents.DroppedAndCollidedEvent.AddListener(DroppedAndCollidedEventHandler);
    }

    private void SliceDroppedCube(Transform droppedCubeTransform, Transform cubeBelowDroppedCubeTransform)
    {
        // helper variables:
        int animIndex = HoveringCubeInstantiator.Instance.AnimationIndex;
        CubeBounds cubeBelowDroppedCubeBounds = new CubeBounds(cubeBelowDroppedCubeTransform);

        // figure out the axis along which to slice, and value of axis:
        Axis axisToSliceAlong;
        float distance;
        float value;

        switch (animIndex)
        {
            case 0:
                axisToSliceAlong = Axis.XAxis;
                distance = droppedCubeTransform.position.x - cubeBelowDroppedCubeTransform.position.x;

                if (distance > 0)
                    value = cubeBelowDroppedCubeBounds.XInterval.UpperBound;
                else if (distance < 0)
                    value = cubeBelowDroppedCubeBounds.XInterval.LowerBound;
                else
                {
                    // no slicing needed
                    return;
                }

                break;
            case 1:
                axisToSliceAlong = Axis.ZAxis;
                distance = droppedCubeTransform.position.z - cubeBelowDroppedCubeTransform.position.z;

                if (distance > 0)
                    value = cubeBelowDroppedCubeBounds.ZInterval.UpperBound;
                else if (distance < 0)
                    value = cubeBelowDroppedCubeBounds.ZInterval.LowerBound;
                else
                {
                    // no slicing needed
                    return;
                }

                break;

            case 2:
                axisToSliceAlong = Axis.XAxis;
                distance = droppedCubeTransform.position.x - cubeBelowDroppedCubeTransform.position.x;

                if (distance > 0)
                    value = cubeBelowDroppedCubeBounds.XInterval.UpperBound;
                else if (distance < 0)
                    value = cubeBelowDroppedCubeBounds.XInterval.LowerBound;
                else
                {
                    // no slicing needed
                    return;
                }

                break;

            case 3:
                axisToSliceAlong = Axis.ZAxis;
                distance = droppedCubeTransform.position.z - cubeBelowDroppedCubeTransform.position.z;

                if (distance > 0)
                    value = cubeBelowDroppedCubeBounds.ZInterval.UpperBound;
                else if (distance < 0)
                    value = cubeBelowDroppedCubeBounds.ZInterval.LowerBound;
                else
                {
                    // no slicing needed
                    return;
                }

                break;

            default:
                throw new System.Exception("Unexpected Animation Index: " + animIndex);
        }

        // slice cube:
        slicedCubes = CubeSlicer.SliceCube(droppedCubeTransform, axisToSliceAlong, value);
    }

    private void ProcessAndNotifySlicedCubes()
    {
        if (RaycastHelper.GameObjectIsInTheAir(slicedCubes[0]))
        {
            // give meaningful names:
            slicedCubes[1].name = "StaticCube";
            slicedCubes[0].name = "FallingCube";

            // attach rigid body to the sliced cube in the air so that it falls
            slicedCubes[0].AddComponent<Rigidbody>();

            // invoke event:
            InvokeSlicedCubesEvent(slicedCubes[1], slicedCubes[0]);
        }
        else if (RaycastHelper.GameObjectIsInTheAir(slicedCubes[1]))
        {
            // give meaningful names:
            slicedCubes[0].name = "StaticCube";
            slicedCubes[1].name = "FallingCube";

            // attach rigid body to the sliced cube in the air so that it falls
            slicedCubes[1].AddComponent<Rigidbody>();

            // invoke event:
            InvokeSlicedCubesEvent(slicedCubes[0], slicedCubes[1]);
        }
        else
        {
            throw new System.Exception("Unexpected situation. Both cubes are not in the air?");
        }
    }

    private void InvokeSlicedCubesEvent(GameObject staticCube, GameObject fallingCube)
    {
        GameEvents.SlicedEvent.Invoke(staticCube, fallingCube);
    }

    // event handlers:

    private void GameOverEventHandler()
    {
        this.isGameOver = true;
    }

    private void DroppedAndCollidedEventHandler(GameObject droppedCube, GameObject cubeBelowDroppedCube)
    {
        if (!isGameOver)
        {
            if (DistanceApproximator.IsPerfectDrop(droppedCube, cubeBelowDroppedCube, errorMagnitude))
            {
                // perfect drop:

                // align cubes:
                DistanceApproximator.AlignCubesHorizonally(droppedCube, cubeBelowDroppedCube);

                // notify:
                GameEvents.PerfectDropEvent.Invoke(droppedCube);
            }
            else
            {
                // retrieve hierarchy:
                HoveringParentHierarchy hierarchy = HoveringCubeHelper.GetHierarchy(droppedCube);
                Transform droppedCubeTransform = hierarchy.MakeEncompassingTransform();

                // slice dropped cube
                SliceDroppedCube(droppedCubeTransform, cubeBelowDroppedCube.transform);

                // destroy hovering cube parent:
                Destroy(HoveringCubeHelper.GetRootParent(droppedCube));

                // process sliced cubes:
                ProcessAndNotifySlicedCubes();
            }
        }
    }    
}
