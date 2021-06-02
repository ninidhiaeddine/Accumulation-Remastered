using UnityEngine;

public interface IHoveringParentHierarchy
{
    // children references:
    public GameObject ScalerContainer { get; }
    public GameObject AnimatorContainer { get; }
    public GameObject MeshContainer { get; }

    // components references:
    public Animator Animator { get; }
    public Rigidbody Rigidbody { get; }

    // transform references info:
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }
    public Transform EncompassingTransform { get; }

    // useful functions:
    public void DestroyAnimator();
    public void AddRigidbody();
}
