using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class HoveringNetworkPlayer : NetworkBehaviour
{
    public NetworkVariableVector3 HoveringCubePosition = new NetworkVariableVector3(new NetworkVariableSettings()
    {
        WritePermission = NetworkVariablePermission.Everyone,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    public void Spawn()
    {
        transform.gameObject.SetActive(false);
    }
}
