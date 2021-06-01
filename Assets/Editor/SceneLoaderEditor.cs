using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderEditor : Editor
{
    private SceneLoader sceneLoader;
    private string[] labels = new string[] { "Scene Type", "Multiplayer Type", "AI Difficulty" };

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // reference to scene loader class:
        sceneLoader = target as SceneLoader;

        ConditionalDropDowns();
        DisplayButton();       
    }

    private void ConditionalDropDowns()
    {
        // show scene type dropdown:
        sceneLoader.sceneType = (SceneType)EditorGUILayout.EnumPopup(labels[0], sceneLoader.sceneType);

        switch (sceneLoader.sceneType)
        {
            case SceneType.Multiplayer:
                // show multiplayer type dropdown:
                sceneLoader.multiplayerType = (MultiplayerType)EditorGUILayout.EnumPopup(labels[1], sceneLoader.multiplayerType);

                switch (sceneLoader.multiplayerType)
                {
                    case MultiplayerType.AI:
                        // show ai difficulty dropdown:
                        sceneLoader.aiDifficulty = (AIDifficulty)EditorGUILayout.EnumPopup(labels[2], sceneLoader.aiDifficulty);
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
    }

    private void DisplayButton()
    {
        if (GUILayout.Button("Start Game"))
        {
            sceneLoader.StartGame(
                sceneLoader.sceneType,
                sceneLoader.multiplayerType,
                sceneLoader.aiDifficulty
                );
        }
    }
}
