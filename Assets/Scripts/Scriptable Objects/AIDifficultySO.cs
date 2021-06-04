using UnityEngine;

[CreateAssetMenu(fileName = "New AI Difficulty", menuName = "ScriptableObjects/AI Difficulty", order = 1)]
public class AIDifficultySO : ScriptableObject
{
    public float minErrorMagnitude;
    public float maxErrorMagnitude;

    [Range(0.0f, 1.0f)]
    public float missingProbability = 0.5f;
}
