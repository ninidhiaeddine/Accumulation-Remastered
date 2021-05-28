using UnityEngine;

public class HoveringCubeTracker : MonoBehaviour
{
    public GameObject initialHoveringCubeParent;

    [HideInInspector]
    public GameObject Parent { get; private set; }
    [HideInInspector]
    public GameObject Child { get; private set; }

    // singleton:
    public static HoveringCubeTracker instance;

    private void Awake()
    {
        // enforce singleton:
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        InitializeParent();
        UpdateChild();
        InitializeEventListeners();
    }

    // helper methods:

    private void InitializeParent()
    {
        Parent = initialHoveringCubeParent;
    }

    private void UpdateChild()
    {
        if (Parent != null)
            Child = Parent.transform.GetChild(0).gameObject;
    }

    private void InitializeEventListeners()
    {
        GameEvents.UpdateHoveringCubeReferenceEvent.AddListener(HandleUpdateHoveringCubeReferenceEvent);
    }

    // event handlers:

    private void HandleUpdateHoveringCubeReferenceEvent(GameObject hoveringCubeParent)
    {
        Parent = hoveringCubeParent;
        UpdateChild();
    }
}
