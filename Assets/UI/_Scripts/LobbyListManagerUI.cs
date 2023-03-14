using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyListManagerUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Referencia o componente que receberá a lista de lobbies")]
    private GameObject LobbyTable;

    [SerializeField]
    private GameObject TableRow;

    public void SpawnRow(string lobbyName, string lobbyHost, string lobbyPlayers)
    {
        GameObject clone = Instantiate(
            TableRow,
            new Vector3(LobbyTable.transform.position.x, LobbyTable.transform.position.y, 0),
            Quaternion.identity,
            LobbyTable.transform
        );
    }

    private float GetHeight(Transform gObject)
    {
        return ((RectTransform)gObject.transform).rect.height;
    }
}
