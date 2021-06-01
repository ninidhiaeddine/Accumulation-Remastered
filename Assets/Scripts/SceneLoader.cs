using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public enum SceneType
{
    Single = 0,
    Multiplayer = -1
}

public enum MultiplayerType
{
    AI = 1,
    Local = 2,
    Remote = 3
}

public enum AIDifficulty
{
    Easy,
    Intermediate,
    Hard
}

public class SceneLoader : MonoBehaviour
{
    public List<string> SceneNames;
    private Dictionary<int, string> SceneTypeNameDictionary;

    // visiblity controlled by SceneLoaderEditor class:
    [HideInInspector] public SceneType sceneType;
    [HideInInspector] public MultiplayerType multiplayerType;
    [HideInInspector] public AIDifficulty aiDifficulty;

    private void OnValidate()
    {
        InitializeSceneTypeNameDictionary();
    }

    private void Start()
    {
        InitializeSceneTypeNameDictionary();
    }

    // public methods:

    public void StartGame(SceneType sceneType, MultiplayerType multiplayerType, AIDifficulty aiDifficulty)
    {
        switch (sceneType)
        {
            case SceneType.Single:
                StartSingle();
                break;
            case SceneType.Multiplayer:
                StartMultiplayer(multiplayerType, aiDifficulty);
                break;
            default:
                break;
        }
    }

    // helper methods:

    private void InitializeSceneTypeNameDictionary()
    {
        SceneTypeNameDictionary = new Dictionary<int, string>();

        SceneTypeNameDictionary.Add(key: 0, value: SceneNames[0]);
        SceneTypeNameDictionary.Add(key: 1, value: SceneNames[1]);
        SceneTypeNameDictionary.Add(key: 2, value: SceneNames[2]);
        SceneTypeNameDictionary.Add(key: 3, value: SceneNames[3]);
    }

    private void StartSingle()
    {
        // figure out scene name:
        string sceneName = SceneTypeToName((int)SceneType.Single);

        // load scene:
        LoadScene(sceneName);
    }

    private void StartMultiplayer(MultiplayerType multiplayerType, AIDifficulty aiDifficulty)
    {
        switch (multiplayerType)
        {
            case MultiplayerType.AI:
                StartAI(aiDifficulty);
                break;
            case MultiplayerType.Local:
                StartMultiplayerLocal();
                break;
            case MultiplayerType.Remote:
                StartMultiplayerRemote();
                break;
            default:
                break;
        }
    }

    private void SetAIDifficultyPlayerPrefs(AIDifficulty aiDifficulty)
    {
        PlayerPrefs.SetInt("AIDifficulty", (int)aiDifficulty);
    }

    private void StartAI(AIDifficulty aiDifficulty)
    {
        // save ai difficulty in player prefs (for usage in the next scene)
        SetAIDifficultyPlayerPrefs(aiDifficulty);

        // figure out scene name:
        string sceneName = SceneTypeToName((int)MultiplayerType.AI);

        // load scene:
        LoadScene(sceneName);
    }

    private void StartMultiplayerLocal()
    {
        // figure out scene name:
        string sceneName = SceneTypeToName((int)MultiplayerType.Local);

        // load scene:
        LoadScene(sceneName);
    }

    private void StartMultiplayerRemote()
    {
        // figure out scene name:
        string sceneName = SceneTypeToName((int)MultiplayerType.Remote);

        // load scene:
        LoadScene(sceneName);
    }

    private string SceneTypeToName(int sceneTypeKey)
    {
        return this.SceneTypeNameDictionary[sceneTypeKey];
    }

    private void LoadScene(string sceneName)
    {
        if (!Application.isEditor)
            SceneManager.LoadScene(sceneName);
#if UNITY_EDITOR
        else
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
#endif
    }
}
