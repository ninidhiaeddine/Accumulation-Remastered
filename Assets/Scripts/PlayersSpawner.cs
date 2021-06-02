using System.Collections.Generic;
using UnityEngine;

public class PlayersSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<Vector3> positions;

    private void Start()
    {
        SpawnPlayersAndNotify();
    }

    private void SpawnPlayersAndNotify()
    {
        foreach (var pos in positions)
        {
            // instantiate:
            GameObject instance = Instantiate(playerPrefab);
            instance.transform.position = pos;

            // raise event:
            GameEvents.SpawnedPlayerEvent.Invoke(instance);
        }
    }
}
