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

    /// <summary>
    /// Deletes all parents of this gameobject and only keeps the object by itself:
    /// </summary>
    /// <param name="hovering"></param>
    public static void DeleteParents(GameObject hovering)
    {
        HoveringParentHierarchy hierarchy = GetHierarchy(hovering);
        if (hierarchy != null)
        {
            // store root parent:
            GameObject rootParent = GetRootParent(hovering);

            // take object out of nested gameobject:
            hovering.transform.parent = null;

            // delete parent:
            Destroy(rootParent);
        }
    }
}
