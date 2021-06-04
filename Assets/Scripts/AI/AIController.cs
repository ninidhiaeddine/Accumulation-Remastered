using UnityEngine;

public class AIController : MonoBehaviour
{
    // inspector references:
    public PlayerManager playerManager;
    public HoveringCubeTracker hoveringCubeTracker;
    public AIDifficultySO aiDifficultySO;
    public AIDifficultySOSelector aiDifficultySOSelector;

    private Animator fsm;   // finite state machine:

    private void Awake()
    {
        InitializeAIDifficultySO();
    }

    private void Start()
    {
        InitializeFSM();
    }

    private void LateUpdate()
    {
        float distance = ComputeDistanceBetweenCubes();
        bool isWithinDroppingDistance = CheckWithinDroppingDistance(distance);
        UpdateFSMState(distance, isWithinDroppingDistance);
    }

    // public methods:

    public void DropAIHoveringCube()
    {
        InputEvents.AIPlayerDroppedInputEvent.Invoke(playerManager.playerToLookFor);
    }

    // helper methods:

    private bool CheckWithinDroppingDistance(float distance)
    {
        return (distance >= aiDifficultySO.minErrorMagnitude
            && distance <= aiDifficultySO.maxErrorMagnitude);
    }

    private void UpdateFSMState(float distance, bool isWithinDroppingDistance)
    {
        fsm.SetFloat("Distance", distance);
        // only update when value is different:
        if (fsm.GetBool("IsWithinDroppingDistance") != isWithinDroppingDistance)
            fsm.SetBool("IsWithinDroppingDistance", isWithinDroppingDistance);
    }

    private void InitializeFSM()
    {
        fsm = GetComponent<Animator>();
        fsm.SetFloat("MissingProbability", aiDifficultySO.missingProbability);
    }

    private void InitializeAIDifficultySO()
    {
        int aiDifficulty = PlayerPrefs.GetInt("AIDifficulty", -1);
        if (aiDifficulty != -1)
            aiDifficultySO = aiDifficultySOSelector.GetAIDifficultySO((AIDifficulty)aiDifficulty);
    }

    private float ComputeDistanceBetweenCubes()
    {
        // retrieve positions:
        Vector3 hoveringCubePos = GetHoveringCubePosition();
        Vector3 cubeBeneathHoveringCubePos = hoveringCubeTracker.CubeBeneathHoveringCube.transform.position;

        // compute distance between hovering cube and cube beneath it:
        float distance = DistanceApproximator.ComputeHorizontalDistance(
            hoveringCubePos,
            cubeBeneathHoveringCubePos
            );

        return distance;
    }

    private Vector3 GetHoveringCubePosition()
    {
        // get hovering cube parent:
        GameObject hoveringCubeParent = hoveringCubeTracker.HoveringCubeParent;

        // get hierarchy of hovering cube parent:
        IHoveringParentHierarchy hoveringParentHierarchy = hoveringCubeParent.GetComponent<IHoveringParentHierarchy>();

        // get transform::
        return hoveringParentHierarchy.Position;
    }
}
