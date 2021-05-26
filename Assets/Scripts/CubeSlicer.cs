using UnityEngine;
using System;

public enum Axis
{
    XAxis,
    YAxis,
    ZAxis
};

public class CubeSlicer
{
    /// <summary>
    /// Slices a Cube along either the X, Y, or Z axis, and returns 2 cubes.
    /// </summary>
    /// <param name="cube">The Cube you would like to slice</param>
    /// <param name="axisToSliceAlong">The axis along which you would like to slice</param>
    /// <param name="value">The geometrical value for the line. For instance, if 'axisToSliceAlong' = 'XAxis' and 'value' = 2.0f,
    /// then this will result in the following line: x = 2
    /// </param>
    /// <returns>2 Cubes correctly positioned and scaled.</returns>
    public static GameObject[] SliceCube(GameObject cube, Axis axisToSliceAlong, float value)
    {
        if (!AxisIntersectsCube(cube, axisToSliceAlong, value))
            throw new Exception("The Axis specified does not intersect the cube in any point.");

        // retrieve cube info:
        Vector3 cubePos = cube.transform.position;
        Vector3 cubeScale = cube.transform.localScale;

        // declare 2 cubes:
        GameObject[] slicedCubes = new GameObject[2] {
            GameObject.CreatePrimitive(PrimitiveType.Cube),
            GameObject.CreatePrimitive(PrimitiveType.Cube)
        };
        Vector3[] newPositions = new Vector3[2] { cubePos, cubePos };
        Vector3[] newScales = new Vector3[2] { cubeScale, cubeScale };

        // helper variable:
        float distance;

        switch (axisToSliceAlong)
        {
            case Axis.XAxis:
                // compute distance:
                distance = Mathf.Abs(cubePos.x - value);

                // compute scales:
                newScales[0].x = cubeScale.x / 2.0f + distance;
                newScales[1].x = cubeScale.x / 2.0f - distance;

                // compute positions:
                newPositions[0].x = value - newScales[0].x / 2.0f;
                newPositions[1].x = value + newScales[1].x / 2.0f;

                break;

            case Axis.YAxis:
                // compute distance:
                distance = Mathf.Abs(cubePos.y - value);

                // compute scales:
                newScales[0].y = cubeScale.y / 2.0f + distance;
                newScales[1].y = cubeScale.y / 2.0f - distance;

                // compute positions:
                newPositions[0].y = value - newScales[0].y / 2.0f;
                newPositions[1].y = value + newScales[1].y / 2.0f;

                break;

            case Axis.ZAxis:
                // compute distance:
                distance = Mathf.Abs(cubePos.z - value);

                // compute scales:
                newScales[0].z = cubeScale.z / 2.0f + distance;
                newScales[1].z = cubeScale.z / 2.0f - distance;

                // compute positions:
                newPositions[0].z = value - newScales[0].z / 2.0f;
                newPositions[1].z = value + newScales[1].z / 2.0f;

                break;

            default:
                throw new Exception("Illegal Axis");
        }

        // set positions and scales:
        for (int i = 0; i < 2; i++)
        {
            slicedCubes[i].transform.position = newPositions[i];
            slicedCubes[i].transform.localScale = newScales[i];
        }

        // return sliced cubes:
        return slicedCubes;
    }

    public static bool AxisIntersectsCube(GameObject cube, Axis axisToSliceAlong, float value)
    {
        // compute cube bounds:
        CubeBounds bounds = new CubeBounds(cube);

        switch (axisToSliceAlong)
        {
            case Axis.XAxis:
                return bounds.XInterval.Contains(value);
            case Axis.YAxis:
                return bounds.YInterval.Contains(value);
            case Axis.ZAxis:
                return bounds.ZInterval.Contains(value);
            default:
                throw new Exception("Illegal Axis");
        }
    }
}
