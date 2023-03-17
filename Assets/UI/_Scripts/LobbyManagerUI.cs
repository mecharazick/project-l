using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using AlphaLobby;
using AlphaLobby.Managers;

namespace AlphaLobby.UI
{
    public class LobbyManagerUI : MonoBehaviour
    {
        [SerializeField]
        private LobbyManager _lobbyManager;

        [SerializeField]
        [Tooltip("Referencia o componente que receberá a lista de lobbies")]
        private GameObject _lobbyTable;

        [SerializeField]
        private GameObject _tableRow;

        [SerializeField]
        [Tooltip("Botão de se juntar ao Lobby")]
        private Button _joinLobbyButton;

        private string _lobbyCode;
        
        public void HandleLobbyCodeChange(string s)
        {
            _lobbyCode = s;
        }


        #region Lobby Creation
        private string _inputedLobbyName;
        private int _inputedMaxPlayers;

        [SerializeField]
        [Tooltip("Botão de confirmar criação de Lobby")]
        private Button _confirmLobbyCreateButton;

        public void HandleChangeLobbyName(string s)
        {
            _inputedLobbyName = s;
        }

        public void HandleChangeMaxPlayers(string s)
        {
            _inputedMaxPlayers = int.Parse(s);
        }

        public void HandleChangeMaxPlayers(int i)
        {
            _inputedMaxPlayers = i;
        }

        #endregion

        public void Start()
        {
            _joinLobbyButton.onClick.AddListener(JoinLobby);
            _confirmLobbyCreateButton.onClick.AddListener(CreateLobby);
            _lobbyManager.onLobbyListFinishFetchEvent.AddListener(PopulateTable);
        }

        private void JoinLobby()
        {
            _lobbyManager.JoinLobbyByCode(_lobbyCode);
        }

        private void CreateLobby()
        {
            Debug.Log(
                "Creating Lobby "
                    + _inputedLobbyName
                    + ", with capacity to: "
                    + _inputedMaxPlayers
                    + " players."
            );
            _lobbyManager.CreateLobby(_inputedLobbyName, _inputedMaxPlayers);
        }

        private void PopulateTable()
        {
            // foreach(Lobby lobby in LobbyManager.availableLobbies){
            //     SpawnTableRow(lobby);
            // }
        }

        public void SpawnTableRow(string lobbyName, string lobbyHost, string lobbyPlayers)
        {
            GameObject clone = Instantiate(
                _tableRow,
                _lobbyTable.transform.position,
                Quaternion.identity,
                _lobbyTable.transform
            );
        }
    }
}
