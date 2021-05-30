using UnityEngine;

public class HoveringCubeTracker : MonoBehaviour, IEventListener
{
    [SerializeField]
    private GameObject initialHoveringCubeParent;

    [HideInInspector]
    public GameObject HoveringCubeParent { get; private set; }

    // singleton:
    public static HoveringCubeTracker Instance { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        InitializeParentReference();
        InitializeEventListeners();
    }

    // interface methods:

    public void InitializeEventListeners()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void InitializeParentReference()
    {
        this.HoveringCubeParent = initialHoveringCubeParent;
    }

    private void UpdateParentReference(GameObject hoveringCubeParent)
    {
        this.HoveringCubeParent = hoveringCubeParent;
    }
    
    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        Debug.Log("UpdatedHoveringParentReferenceEventHandler Called. | hoveringCubeParent = " + hoveringCubeParent);
        UpdateParentReference(hoveringCubeParent);
    }
}
