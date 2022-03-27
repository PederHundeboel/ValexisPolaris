using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TestSpawner : NetworkBehaviour
{
    public GameObject spawnedItem;
    // Start is called before the first frame update
    void Start()
    {
        SpawnServerRPC();
    }

    [ServerRpc]
    private void SpawnServerRPC()
    {
        GameObject go = Instantiate(spawnedItem, Vector3.zero, Quaternion.identity);
        go.GetComponent<NetworkObject>().Spawn();
    }

    [ClientRpc]
    private void InitializeClientRPC(ulong clientId, ulong objectId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            GameObject spawnedObjert = NetworkManager.SpawnManager.SpawnedObjects[objectId].gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
