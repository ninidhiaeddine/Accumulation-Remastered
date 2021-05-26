using UnityEngine;

public class CubeSlicerTester : MonoBehaviour
{
    public Vector3 scale = new Vector3(5, 1, 5);
    public Axis axis = Axis.XAxis;
    public float value = 1.0f;

    private GameObject cube;

    void Start()
    {
        CreateCube();
        SliceCube();
    }

    void CreateCube()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(5, 1, 5);
    }

    void SliceCube()
    {
        // slice cubes:
        GameObject[] slicedCubes = CubeSlicer.SliceCube(this.cube, this.axis, this.value);

        // destroy current cube:
        Destroy(cube);

        // color cubes to distinguish them:
        slicedCubes[0].GetComponent<Renderer>().material.color = Color.red;
        slicedCubes[1].GetComponent<Renderer>().material.color = Color.yellow;
    }
}
