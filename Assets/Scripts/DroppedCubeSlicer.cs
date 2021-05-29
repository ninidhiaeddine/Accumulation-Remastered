using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCubeSlicer : MonoBehaviour, IEventListener
{
    public float errorMagnitude = 0.1f; 

    // helper variables:
    private GameObject[] slicedCubes;
    private bool isGameOver = false;

    // singleton:
    public static DroppedCubeSlicer instance;

    private void Awake()
    {
        // enforce singleton:
        if (instance == null)
            instance = this;
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
        GameEvents.GameOverEvent.AddListener(HandleGameOverEvent);
        GameEvents.DroppedAndCollidedEvent.AddListener(HandleDroppedAndCollidedEvent);
    }

    private void SliceDroppedCube(GameObject droppedCube, GameObject cubeBelowDroppedCube)
    {
        // helper variables:
        int animIndex = HoveringCubeInstantiator.instance.AnimationIndex;
        CubeBounds cubeBelowDroppedCubeBounds = new CubeBounds(cubeBelowDroppedCube);

        // figure out the axis along which to slice, and value of axis:
        Axis axisToSliceAlong;
        float distance;
        float value;

        switch (animIndex)
        {
            case 0:
                axisToSliceAlong = Axis.XAxis;
                distance = droppedCube.transform.position.x - cubeBelowDroppedCube.transform.position.x;

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
                distance = droppedCube.transform.position.z - cubeBelowDroppedCube.transform.position.z;

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
                distance = droppedCube.transform.position.x - cubeBelowDroppedCube.transform.position.x;

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
                distance = droppedCube.transform.position.z - cubeBelowDroppedCube.transform.position.z;

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
        slicedCubes = CubeSlicer.SliceCube(droppedCube, axisToSliceAlong, value);
    }

    private void ProcessAndNotifySlicedCubes()
    {
        if (RaycastHelper.GameObjectIsInTheAir(slicedCubes[0]))
        {
            // attach rigid body to the sliced cube in the air so that it falls
            slicedCubes[0].AddComponent<Rigidbody>();

            // invoke event:
            InvokeSlicedCubesEvent(slicedCubes[1], slicedCubes[0]);
        }
        else if (RaycastHelper.GameObjectIsInTheAir(slicedCubes[1]))
        {
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
        GameEvents.DroppedAndSlicedEvent.Invoke(staticCube, fallingCube);
    }

    // event handlers:

    private void HandleGameOverEvent()
    {
        this.isGameOver = true;
    }

    private void HandleDroppedAndCollidedEvent(GameObject droppedCube, GameObject cubeBelowDroppedCube)
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
                // slice dropped cube
                SliceDroppedCube(droppedCube, cubeBelowDroppedCube);

                // destroy cube:
                Destroy(droppedCube.transform.parent.gameObject);

                // process sliced cubes:
                ProcessAndNotifySlicedCubes();
            }
        }
    }    
}
