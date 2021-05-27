using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedCubeSlicer : MonoBehaviour
{
    private GameObject[] slicedCubes;

    void Start()
    {
        InitializeEventListeners();
    }

    // helper methods:

    void InitializeEventListeners()
    {
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

            default:
                throw new System.Exception("Unexpected Animation Index");
        }

        // slice cube:
        slicedCubes = CubeSlicer.SliceCube(droppedCube, axisToSliceAlong, value);
    }

    private void ProcessSlicedCubes()
    {
        if (Helpers.GameObjectIsInTheAir(slicedCubes[0]))
        {
            // color cubes temporarily:
            slicedCubes[1].GetComponent<Renderer>().material.color = Color.green;
            slicedCubes[0].GetComponent<Renderer>().material.color = Color.red;

            // attach rigid body to the sliced cube in the air so that it falls
            slicedCubes[0].AddComponent<Rigidbody>();

            // invoke event:
            GameEvents.DroppedAndSlicedEvent.Invoke(slicedCubes[1], slicedCubes[0]);
        }
        else if (Helpers.GameObjectIsInTheAir(slicedCubes[1]))
        {
            // color cubes temporarily:
            slicedCubes[0].GetComponent<Renderer>().material.color = Color.green;
            slicedCubes[1].GetComponent<Renderer>().material.color = Color.red;

            // attach rigid body to the sliced cube in the air so that it falls
            slicedCubes[1].AddComponent<Rigidbody>();

            // invoke event:
            GameEvents.DroppedAndSlicedEvent.Invoke(slicedCubes[0], slicedCubes[1]);
        }
        else
        {
            throw new System.Exception("Unexpected situation. Both cubes are not in the air?");
        }
    }
    
    // event handlers:

    private void HandleDroppedAndCollidedEvent(GameObject droppedCube, GameObject cubeBelowDroppedCube)
    {
        // slice dropped cube
        SliceDroppedCube(droppedCube, cubeBelowDroppedCube);

        // destroy cube:
        Destroy(droppedCube.transform.parent.gameObject);

        // process sliced cubes:
        ProcessSlicedCubes();
    }
}
