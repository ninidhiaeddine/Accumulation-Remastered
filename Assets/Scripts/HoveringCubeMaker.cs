using UnityEngine;

public class HoveringCubeMaker : CubeMaker
{
    public static GameObject CreateHoveringCube(string name, Vector3 position, Vector3 scale)
    {
        // create empty parent:
        string parentName = $"{name}Parent";
        GameObject parent = new GameObject(parentName);

        // set parent's position:
        Vector3 parentPos = position;
        parent.transform.position = parentPos;

        // create child:
        string childName = $"{name}Child";
        Vector3 childPos = Vector3.zero;
        Vector3 childScale = scale;
        GameObject child = CreateCube(childName, childPos, childScale);

        // set tag:
        child.tag = "Player";   // THIS IS IMPORTANT FOR DETECTING GAME OVER

        // set parent:
        child.transform.parent = parent.transform;

        // set child's positon:
        child.transform.localPosition = Vector3.zero;
        
        // return reference:
        return parent;
    }
}
