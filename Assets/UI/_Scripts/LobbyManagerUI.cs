using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using AlphaLobby.Managers;
using TMPro;

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

        private void Start()
        {
            _joinLobbyButton.onClick.AddListener(JoinLobby);
            _confirmLobbyCreateButton.onClick.AddListener(CreateLobby);
            LobbyManager.onLobbyList.AddListener(PopulateTable);
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
            Dictionary<string, DataObject> data = new Dictionary<string, DataObject>();
            data.Add("HostName", new DataObject(DataObject.VisibilityOptions.Public, AlphaLobby.Username));
            _lobbyManager.CreateLobby(_inputedLobbyName, _inputedMaxPlayers, data);
        }

        private void PopulateTable()
        {
            foreach (Transform row in _lobbyTable.transform)
            {
                Destroy(row.gameObject);
            }
            foreach (Lobby lobby in _lobbyManager.availableLobbies)
            {
                SpawnTableRow(lobby);
            }
        }

        public void SpawnTableRow(Lobby lobby)
        {
            Debug.Log("Spawning Table Row");
            GameObject clone = Instantiate(
                _tableRow,
                _lobbyTable.transform.position,
                _lobbyTable.transform.rotation,
                _lobbyTable.transform
            );
            int connectedPlayers = lobby.MaxPlayers - lobby.AvailableSlots;
            DataObject data;
            lobby.Data.TryGetValue("HostName", out data);
            var rowText = clone.GetComponentsInChildren<TextMeshProUGUI>();
            rowText[0].text = lobby.Name;
            rowText[1].text = data.Value;
            rowText[2].text = (connectedPlayers).ToString() + "/" + lobby.MaxPlayers;
        }
    }
}
