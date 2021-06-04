using UnityEngine;

public class WithinDroppingDistanceState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // figure out whether it should miss:
        float missingProbability = animator.GetFloat("MissingProbability");
        bool shouldMiss = RandomBooleanGenerator.GenerateRandomBoolean(missingProbability);

        // set value:
        animator.SetBool("ShouldMiss", shouldMiss);

        if (shouldMiss)
            return;

        // decide a random place within the dropping distance:
        float distanceToDrop = Random.value * animator.GetFloat("Distance");

        // set value:
        animator.SetFloat("DistanceToDrop", distanceToDrop);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = animator.GetFloat("Distance");
        float distanceToDrop = animator.GetFloat("DistanceToDrop");
        bool isWithinDroppingDistance = animator.GetBool("IsWithinDroppingDistance");

        if (isWithinDroppingDistance && distance <= distanceToDrop)
        {
            animator.SetBool("ShouldDrop", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
