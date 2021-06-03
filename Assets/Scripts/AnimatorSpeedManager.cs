using UnityEngine;

public class AnimatorSpeedManager : MonoBehaviour
{
    // settings:
    public Animator animator { get; set; }
    public float AnimationSpeed 
    { 
        get
        {
            if (this.animator != null)
                return this.animator.speed;
            else
                throw new System.Exception("Null Reference Exception: Animator is null!");
        }
        set
        {
            if (this.animator != null)
                this.animator.speed = value;
            else
                throw new System.Exception("Null Reference Exception: Animator is null!");
        }
    }
}
