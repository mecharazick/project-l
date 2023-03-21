using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using AlphaLobby.Interfaces;

namespace AlphaLobby.Managers
{
    public class LobbyManager : MonoBehaviour, ILobbyEvents
    {
        // #region LobbyManager Singleton Constructor Definition
        // private static LobbyManager _lobbyManagerSingleton;
        // public static LobbyManager Instance
        // {
        //     get
        //     {
        //         if (_lobbyManagerSingleton == null)
        //             _lobbyManagerSingleton = new LobbyManager();
        //         return _lobbyManagerSingleton;
        //     }
        //     private set { _lobbyManagerSingleton = Instance; }
        // }

        // private LobbyManager() { }
        // #endregion
        public static UnityEvent onLobbyJoin = new UnityEvent();
        public static UnityEvent onLobbyList = new UnityEvent();

        [SerializeField]
        private Lobby _hostedLobby;

        [SerializeField]
        private Lobby _joinedLobby;

        private float _heartbeatTimer = 0f;

        public List<Lobby> availableLobbies;

        public enum LobbyAvailability
        {
            Public = 0,
            Private = 1
        };

        private void Update()
        {
            HandleLobbyHeartbeat();
        }

        #region LobbyCreation
        public void CreateLobby()
        {
            CreateLobby("default", 4);
        }

        public void CreateLobby(string lobbyName, int maxPlayers)
        {
            CreateLobby(lobbyName, maxPlayers, null);
        }

        public void CreateLobby(
            string lobbyName,
            int maxPlayers,
            Dictionary<string, DataObject> data
        )
        {
            CreateLobby(lobbyName, maxPlayers, LobbyAvailability.Public, data);
        }

        public async void CreateLobby(
            string lobbyName,
            int maxPlayers,
            LobbyAvailability lobbyAvailability,
            Dictionary<string, DataObject> data
        )
        {
            CreateLobbyOptions options = new CreateLobbyOptions();
            options.IsPrivate =
                (lobbyAvailability & LobbyAvailability.Private) == LobbyAvailability.Private;
            options.Player = new Player(
                id: AuthenticationService.Instance.PlayerId,
                data: new Dictionary<string, PlayerDataObject>()
                {
                    {
                        "PlayerName",
                        new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Member,
                            value: AlphaLobby.Username
                        )
                    }
                }
            );
            options.Data = data;
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(
                lobbyName,
                maxPlayers,
                options
            );
            _joinedLobby = lobby;
            _hostedLobby = _joinedLobby;
            ListLobbies();
        }

        public async void ListLobbies()
        {
            List<Lobby> lobbies;
            try
            {
                lobbies = (await LobbyService.Instance.QueryLobbiesAsync()).Results;
                availableLobbies = lobbies;
                Debug.Log("Found " + lobbies.Count + " available lobbies");
                onLobbyList.Invoke();
                foreach (Lobby lobby in lobbies)
                {
                    Debug.Log(lobby.Name + ", Code: " + lobby.LobbyCode);
                }
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError(e);
            }
        }

        private void HandleLobbyHeartbeat()
        {
            if (_hostedLobby == null)
                return;
            if (_heartbeatTimer >= 0f)
            {
                _heartbeatTimer -= Time.deltaTime;
            }
            else
            {
                LobbyService.Instance.SendHeartbeatPingAsync(_hostedLobby.Id);
                Debug.Log("Sending Heartbeat to " + _hostedLobby.Name);
                float heartbeatTimerMax = 29.9f;
                _heartbeatTimer = heartbeatTimerMax;
            }
        }
        #endregion

        #region LobbyUpdate

        public async void UpdateLobby()
        {
            UpdateLobbyOptions options = new UpdateLobbyOptions();
            Lobby lobby = await LobbyService.Instance.UpdateLobbyAsync(_hostedLobby.Id, options);
        }
        #endregion

        #region LobbyJoin
        public async void JoinLobbyByCode(string lobbyCode)
        {
            try
            {
                _joinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode);
                onLobbyJoin.Invoke();
                Debug.Log("Joined Lobby " + _joinedLobby.Name);
            }
            catch (LobbyServiceException exception)
            {
                Debug.LogError(exception);
            }
        }
        #endregion
    };
}
