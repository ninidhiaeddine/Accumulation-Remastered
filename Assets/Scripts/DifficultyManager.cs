using UnityEngine;

public class DifficultyManager : MonoBehaviour, IEventHandler
{
    // settings:
    public float animationSpeed = 1.0f;
    [SerializeField] private Animator hoveringAnimator;

    // singleton:
    public static DifficultyManager Singleton { get; private set; }

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
        SetAnimationSpeed();
        InitializeEventHandlers();
    }

    private void OnValidate()
    {
        SetAnimationSpeed();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.AddListener(UpdatedHoveringParentReferenceEventHandler);
    }

    // helper methods:

    private void SetAnimationSpeed()
    {
        if (hoveringAnimator != null)
            hoveringAnimator.speed = animationSpeed;
    }

    private void UpdateAnimatorReference(Animator newAnimator)
    {
        // update reference:
        hoveringAnimator = newAnimator;

        // set speed again:
        SetAnimationSpeed();
    }

    // event handlers:

    private void UpdatedHoveringParentReferenceEventHandler(GameObject hoveringCubeParent)
    {
        // get reference to hierarchy:
        HoveringParentHierarchy hierarchy = hoveringCubeParent.GetComponent<HoveringParentHierarchy>();

        // update reference to animator:
        UpdateAnimatorReference(hierarchy.Animator);
    }
}
