using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDifficultySOSelector : MonoBehaviour
{
    [SerializeField] private AIDifficultySO easyAiDifficulty;
    [SerializeField] private AIDifficultySO intermediateAiDifficulty;
    [SerializeField] private AIDifficultySO hardAiDifficulty;

    public AIDifficultySO GetAIDifficultySO(AIDifficulty aiDifficulty)
    {
        switch (aiDifficulty)
        {
            case AIDifficulty.Easy:
                return easyAiDifficulty;
            case AIDifficulty.Intermediate:
                return intermediateAiDifficulty;
            case AIDifficulty.Hard:
                return hardAiDifficulty;
            default:
                throw new System.Exception("Unexpected AI Difficulty Value");
        }
    }
}
