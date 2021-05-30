using UnityEngine;

public class HoveringParentHierarchy : MonoBehaviour
{
    // children references:
    public GameObject ScalerContainer { get; private set; }
    public GameObject AnimatorContainer { get; private set; }
    public GameObject MeshContainer { get; private set; }

    // componenents references:
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    // other relevent info:
    public Vector3 Position
    {
        get
        {
            return MeshContainer.transform.position;
        }
    }
    public Vector3 Scale { get { return ScalerContainer.transform.localScale; } }

    private void Awake()
    {
        GetChildrenReferences();
        GetComponentsReferences();
    }

    // public methods:

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetScale(Vector3 newScale)
    {
        ScalerContainer.transform.localScale = newScale;
    }

    public Transform MakeEncompassingTransform()
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

    // helper methods:    

    private void GetChildrenReferences()
    {
        GetScalerContainer();
        GetAnimatorContainer();
        GetMeshContainer();
    }

    private void GetComponentsReferences()
    {
        GetAnimator();
        GetRigidbody();
    }

    private void GetScalerContainer()
    {
        ScalerContainer = transform.GetChild(0).gameObject;
    }

    private void GetAnimatorContainer()
    {
        AnimatorContainer = ScalerContainer.transform.GetChild(0).gameObject;
    }

    private void GetMeshContainer()
    {
        MeshContainer = AnimatorContainer.transform.GetChild(0).gameObject;
    }

    private void GetAnimator()
    {
        Animator = AnimatorContainer.GetComponent<Animator>();
    }

    private void GetRigidbody()
    {
        Rigidbody = MeshContainer.GetComponent<Rigidbody>();
    }
}
