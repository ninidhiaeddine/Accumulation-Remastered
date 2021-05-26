using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaker : MonoBehaviour
{
    public static GameObject CreateCube(string name, Vector3 position, Vector3 scale)
    {
        // create new cube:
        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // set up properties:
        newCube.name = name;
        newCube.transform.position = position;
        newCube.transform.localScale = scale;

        // return reference:
        return newCube;
    }
}
