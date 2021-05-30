using UnityEngine;

public class HoveringCubeInstantiator : MonoBehaviour, IEventHandler
{
    public GameObject hoveringCubeParentPrefab;
    public Vector3 offsetFromDroppedCube;

    [HideInInspector]
    public int AnimationIndex { get; private set; }

    // helper variable:
    private bool isGameOver = false;

    // singleton:
    public static HoveringCubeInstantiator Instance { get; private set; }

    private void Awake()
    {
        // enforce singleton:
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        InitializeEventHandlers();
    }

    // interface methods:

    public void InitializeEventHandlers()
    {
        GameEvents.SlicedEvent.AddListener(SlicedEventHandler);
        GameEvents.PerfectDropEvent.AddListener(PerfectDropEventHandler);
        GameEvents.GameOverEvent.AddListener(GameOverEventHandler);
    }

    // event handlers:

    private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube)
    {
        // only instantiate new hovering cubes when game is not over:
        if (!isGameOver)
        {
           InstantiateHoveringCubeAndNotify(staticCube);
        }
    }

    private void PerfectDropEventHandler(GameObject staticCube)
    {
        // only instantiate new hovering cubes when game is not over:
        if (!isGameOver)
        {
            InstantiateHoveringCubeAndNotify(staticCube);
        }
    }

    private void GameOverEventHandler()
    {
        this.isGameOver = true;
    }

    // helper methods:

    private void InstantiateHoveringCubeAndNotify(GameObject staticCube)
    {
        GameObject newHoveringCubeParent = InstantiateHoveringCubeParent(staticCube);
        SetRandomAnimation(newHoveringCubeParent);
        UpdateHoveringParentReference(newHoveringCubeParent);
    }

    private GameObject InstantiateHoveringCubeParent(GameObject staticCube)
    {
        // retrieve position of static cube:
        Vector3 staticCubePos = staticCube.transform.position;

        // calculate new position and scale based on settings:
        Vector3 newPos = staticCubePos + offsetFromDroppedCube;
        Vector3 newScale = staticCube.transform.localScale;

        // instantiate prefab:
        GameObject instance = Instantiate(this.hoveringCubeParentPrefab);

        // retrieve hierarchy of new instance:
        HoveringParentHierarchy newHierarchy = HoveringCubeHelper.GetHierarchy(instance);

        // set position and scale:
        newHierarchy.SetPosition(newPos);
        newHierarchy.SetScale(newScale);

        // return reference:
        return instance;
    }

    private void SetRandomAnimation(GameObject hoveringCubeParent)
    {
        // get reference to hierarchy:
        HoveringParentHierarchy hierarchy = HoveringCubeHelper.GetHierarchy(hoveringCubeParent);

        // get reference to animator in the hierarchy:
        Animator animator = hierarchy.Animator;

        // generate random animation index:
        AnimationIndex = Random.Range(0, 4);

        // set animation:
        animator.SetInteger("index", AnimationIndex);
    }

    private void UpdateHoveringParentReference(GameObject hoveringCubeParent)
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.Invoke(hoveringCubeParent);
    }
}
