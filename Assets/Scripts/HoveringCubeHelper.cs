using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringCubeHelper : MonoBehaviour
{
    public static GameObject GetRootParent(GameObject gameObject)
    {
        return gameObject.transform.root.gameObject;
    }

    public static HoveringParentHierarchy GetHierarchy(GameObject hovering)
    {
        GameObject parent = GetRootParent(hovering);
        return parent.GetComponent<HoveringParentHierarchy>();
    }
}
