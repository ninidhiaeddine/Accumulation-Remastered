using MLAPI;
using UnityEngine;

public class HoveringNetworkManager : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            StatusLabels();
        }
        SpawnPlayer();

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }

    static void SpawnPlayer()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            if (GUILayout.Button("Disable Player1"))
            {
                HoveringNetworkPlayer player = GameObject.FindGameObjectWithTag("Player1").GetComponent<HoveringNetworkPlayer>();
                if (player)
                {
                    player.Spawn();
                }
            }
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            if (GUILayout.Button("Disable Player2"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                    out var networkedClient))
                {
                    var player = networkedClient.PlayerObject.GetComponent<HoveringNetworkPlayer>();
                    if (player)
                    {
                        player.Spawn();
                    }
                }
            }
        }
    }
}
