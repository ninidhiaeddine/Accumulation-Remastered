using UnityEngine;

public class HoveringParentHierarchy : MonoBehaviour
{
    // children references:
    public GameObject ScalerContainer { get { return transform.GetChild(0).gameObject; } }
    public GameObject AnimatorContainer { get { return ScalerContainer.transform.GetChild(0).gameObject; } }
    public GameObject MeshContainer { get { return AnimatorContainer.transform.GetChild(0).gameObject; } }

    // components references:
    public Animator Animator { get { return AnimatorContainer.GetComponent<Animator>(); } }
    public Rigidbody Rigidbody { get { return MeshContainer.GetComponent<Rigidbody>(); } }

    // transform references info:
    public Vector3 Position
    {
        get
        {
            return MeshContainer.transform.position;
        }
        set
        {
            transform.position = value;
        }
    }
    public Vector3 Scale
    {
        get
        {
            return ScalerContainer.transform.localScale;
        }
        set
        {
            ScalerContainer.transform.localScale = value;
        }
    }
    public Transform EncompassingTransform
    {
        get
        {
            // make temporary gameobject with position and scale:
            GameObject temp = new GameObject("temp");
            temp.transform.position = this.Position;
            temp.transform.localScale = this.Scale;

            // extract result:
            Transform result = temp.transform;

            // return reference:
            return result;
        }
    }
}
