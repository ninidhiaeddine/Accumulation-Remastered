using UnityEngine;

public class AnimatorGetter : MonoBehaviour, IEventHandler
{
    private Animator animator;

    // singleton:
    public static AnimatorGetter Singleton { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.SpawnedPlayerEvent.AddListener(SpawnedPlayerEventHandler);
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void PassAnimatorReference()
    {
        AnimatorSpeedManager.Singleton.animator = this.animator;
    }

    // event handlers:

    private void SpawnedPlayerEventHandler(GameObject spawnedPlayer)
    {
        // get hierarchy:
        IHoveringParentHierarchy hierarchy = spawnedPlayer.GetComponent<IHoveringParentHierarchy>();

        // update reference to animator:
        this.animator = hierarchy.Animator;

        // pass reference:
        PassAnimatorReference();
    }

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        // get reference to hierarchy:
        IHoveringParentHierarchy hierarchy = hoveringCubeParent.GetComponent<IHoveringParentHierarchy>();

        // update reference to animator:
        this.animator = hierarchy.Animator;

        // pass reference:
        PassAnimatorReference();
    }
}
