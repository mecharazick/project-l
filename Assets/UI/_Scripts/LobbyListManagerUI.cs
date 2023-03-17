using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListManagerUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Referencia o componente que receberá a lista de lobbies")]
    private GameObject _lobbyTable;

    [SerializeField]
    private GameObject _tableRow;
    [SerializeField]
    private Text _lobbyCode;

    [SerializeField]
    [Tooltip("Botão de se juntar ao Lobby")]
    private Button _joinLobbyButton;

    public void Start(){
        _joinLobbyButton.onClick.AddListener(() => {
            
        });
    }

    public void SpawnRow(string lobbyName, string lobbyHost, string lobbyPlayers)
    {
        GameObject clone = Instantiate(
            _tableRow,
            _lobbyTable.transform.position,
            Quaternion.identity,
            _lobbyTable.transform
        );
    }
}
