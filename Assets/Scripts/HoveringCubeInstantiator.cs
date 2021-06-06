using UnityEngine;

public class HoveringCubeInstantiator : MonoBehaviour, IEventHandler
{
    // references:
    public PlayerManager playerManager;
    public GameObject hoveringCubePrefab;
    public Vector3 offsetFromDroppedCube;

    [HideInInspector]
    public int AnimationIndex { get; private set; }

    // helper variable:
    private bool isGameOver = false;

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

    private void SlicedEventHandler(GameObject staticCube, GameObject fallingCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            // only instantiate new hovering cubes when game is not over:
            if (!isGameOver)
            {
                InstantiateHoveringCubeAndNotify(staticCube, sender);
            }
        }

    }

    private void PerfectDropEventHandler(GameObject staticCube, Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
        {
            // only instantiate new hovering cubes when game is not over:
            if (!isGameOver)
            {
                InstantiateHoveringCubeAndNotify(staticCube, sender);
            }
        }
    }

    private void GameOverEventHandler(Player sender)
    {
        if (playerManager.EventShouldBeApproved(sender))
            this.isGameOver = true;
    }

    // helper methods:

    private void SetTag(GameObject gameObject, Player sender)
    {
        string tag = "";
        switch (sender)
        {
            case Player.SinglePlayer:
                tag = "Player";
                break;
            case Player.Player1:
                tag = "Player1";
                break;
            case Player.Player2:
                tag = "Player2";
                break;
            case Player.AI:
                tag = "PlayerAI";
                break;
            default:
                break;
        }
        gameObject.tag = tag;
    }

    private void InstantiateHoveringCubeAndNotify(GameObject staticCube, Player sender)
    {
        GameObject newHoveringCubeParent = InstantiateHoveringCubeParent(staticCube);
        SetTag(newHoveringCubeParent, sender);
        IHoveringParentHierarchy hierarchy = newHoveringCubeParent.GetComponent<IHoveringParentHierarchy>();
        SetRandomAnimation(hierarchy);
        SetPlayerManagerReference(hierarchy);
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
        GameObject instance = Instantiate(this.hoveringCubePrefab);

        // retrieve hierarchy of new instance:
        HoveringParentHierarchy newHierarchy = HoveringCubeHelper.GetHierarchy(instance);

        // set position and scale:
        newHierarchy.Position = newPos;
        newHierarchy.Scale = newScale;

        // return reference:
        return instance;
    }

    private void SetRandomAnimation(IHoveringParentHierarchy hoveringParentHierarchy)
    {
        // get reference to animator in the hierarchy:
        Animator animator = hoveringParentHierarchy.Animator;

        // generate random animation index:
        AnimationIndex = Random.Range(0, 4);

        // set animation:
        animator.SetInteger("index", AnimationIndex);
    }

    private void SetPlayerManagerReference(IHoveringParentHierarchy hoveringParentHierarchy)
    {
        // get script reference:
        DroppedCubeBehavior script = hoveringParentHierarchy.MeshContainer.GetComponent<DroppedCubeBehavior>();

        // set reference:
        script.playerManager = this.playerManager;
    }

    private void UpdateHoveringParentReference(GameObject hoveringCubeParent)
    {
        GameEvents.UpdatedHoveringParentReferenceEvent.Invoke(hoveringCubeParent, playerManager.playerToLookFor);
    }
}
