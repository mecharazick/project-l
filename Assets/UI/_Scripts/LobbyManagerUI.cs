using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Lobbies.Models;
using AlphaLobby.Managers;
using UnityEngine.Events;
using TMPro;

namespace AlphaLobby.UI
{
    public class LobbyManagerUI : MonoBehaviour
    {
        public delegate void RowAction<T>(T parameter);

        [SerializeField]
        [Tooltip("Referencia o componente que receberá a lista de lobbies")]
        private GameObject _lobbyTable;

        [SerializeField]
        private GameObject _playerTableHeaders;

        [SerializeField]
        [Tooltip("Referencia o componente que receberá a lista de jogadores")]
        private GameObject _playerTable;

        [SerializeField]
        private GameObject _lobbyTableRow;

        [SerializeField]
        private GameObject _playerTableRow;

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
            LobbyManager.onLobbyList.AddListener(PopulateLobbyTable);
            LobbyManager.onLobbyJoin.AddListener(SetPlayerTableHeaders);
            LobbyManager.onLobbyJoin.AddListener(PopulatePlayerTable);
        }

        public void JoinLobby()
        {
            LobbyManager.JoinLobbyByCode(_lobbyCode);
        }

        private void JoinLobby(string lobbyCode)
        {
            LobbyManager.JoinLobbyByCode(lobbyCode);
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
            data.Add(
                "HostName",
                new DataObject(DataObject.VisibilityOptions.Public, AlphaLobby.Username)
            );
            LobbyManager.CreateLobby(_inputedLobbyName, _inputedMaxPlayers, data);
        }

        #region Lobby Table
        private void PopulateLobbyTable(List<Lobby> availableLobbies)
        {
            foreach (Transform row in _lobbyTable.transform)
            {
                DespawnLobbyTableRow(row);
            }
            foreach (Lobby lobby in availableLobbies)
            {
                SpawnLobbyTableRow(lobby);
            }
        }

        public void SpawnLobbyTableRow(Lobby lobby)
        {
            Debug.Log("Spawning Table Row");
            GameObject clone = Instantiate(
                _lobbyTable,
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
            clone
                .GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    JoinLobby(lobby.LobbyCode);
                });
        }

        public void DespawnLobbyTableRow(Transform row)
        {
            Button rowButton;
            row.TryGetComponent<Button>(out rowButton);
            if (rowButton != null)
            {
                rowButton.onClick.RemoveAllListeners();
            }
            Destroy(row.gameObject);
        }
        #endregion
        #region Player Table
        private void SetPlayerTableHeaders(Lobby lobby)
        {
            _playerTableHeaders.transform.Find("LobbyName").GetComponent<TextMeshProUGUI>().text =
                lobby.Name;
            _playerTableHeaders.transform
                .Find("ConnectedPlayerQuantity")
                .GetComponent<TextMeshProUGUI>()
                .text =
                (lobby.MaxPlayers - lobby.AvailableSlots).ToString() + "/" + lobby.MaxPlayers;
        }

        private void PopulatePlayerTable(Lobby lobby)
        {
            foreach (Transform row in _playerTable.transform)
            {
                DespawnLobbyTableRow(row);
            }
            foreach (Player player in lobby.Players)
            {
                SpawnPlayerTableRow(player);
            }
        }

        public void SpawnPlayerTableRow(Player player)
        {
            GameObject clone = Instantiate(
                _playerTableRow,
                _playerTable.transform.position,
                _playerTable.transform.rotation,
                _playerTable.transform
            );
            var rowText = clone.GetComponentsInChildren<TextMeshProUGUI>();
            PlayerDataObject pdo;
            player.Data.TryGetValue("PlayerName", out pdo);
            rowText[0].text = pdo?.Value;
            rowText[1].text = "";
        }

        public void DespawnPlayerTableRow(Transform row)
        {
            Button rowButton;
            row.TryGetComponent<Button>(out rowButton);
            if (rowButton != null)
            {
                rowButton.onClick.RemoveAllListeners();
            }
            Destroy(row.gameObject);
        }
        #endregion

        #region Experimental functions
        // private void PopulateTable(
        //     Dictionary<string, string[]> info,
        //     GameObject table,
        //     GameObject rowPrefab
        // )
        // {
        //     foreach (Transform row in _playerTable.transform)
        //     {
        //         DespawnLobbyTableRow(row);
        //     }
        //     foreach (string key in info.Keys)
        //     {
        //         InstantiateRow(table, rowPrefab, info[key]);
        //     }
        // }

        // public void InstantiateRow(GameObject table, GameObject rowPrefab, string[] rowContent)
        // {
        //     InstantiateRow(table, rowPrefab, rowContent, null);
        // }

        // public void InstantiateRow(
        //     GameObject table,
        //     GameObject rowPrefab,
        //     string[] rowContent,
        //     UnityAction action = null
        // )
        // {
        //     Button rowActionArea;

        //     GameObject row = Instantiate(
        //         rowPrefab,
        //         table.transform.position,
        //         table.transform.rotation,
        //         table.transform
        //     );
        //     row.TryGetComponent<Button>(out rowActionArea);
        //     if (action != null && rowActionArea != null)
        //     {
        //         rowActionArea.onClick.AddListener(action);
        //     }
        //     int i = 0;
        //     foreach (TextMeshProUGUI textEl in row.GetComponentsInChildren<TextMeshProUGUI>())
        //     {
        //         textEl.text = rowContent[i];
        //         i++;
        //     }
        // }
        #endregion
    }
}
