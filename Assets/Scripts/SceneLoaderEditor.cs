using UnityEditor;

[CustomEditor(typeof(SceneLoader))]
public class SceneLoaderEditor : Editor
{
    string[] labels = new string[] { "Scene Type", "Multiplayer Type", "AI Difficulty" };

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // reference to scene loader class:
        SceneLoader sceneLoader = target as SceneLoader;

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
}
