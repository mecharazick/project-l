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
        public static UnityEvent<Lobby> onLobbyJoin = new UnityEvent<Lobby>();

        public static UnityEvent<List<Lobby>> onLobbyList = new UnityEvent<List<Lobby>>();

        private static Player _player;

        private static Lobby _hostedLobby;

        private static Lobby _joinedLobby;

        private static float _heartbeatTimer = 0f;

        public enum LobbyAvailability
        {
            Public = 0,
            Private = 1
        };

        public static void Initialize(string username)
        {
            _player = new Player(
                id: AuthenticationService.Instance.PlayerId,
                data: new Dictionary<string, PlayerDataObject>()
                {
                    {
                        "PlayerName",
                        new PlayerDataObject(
                            visibility: PlayerDataObject.VisibilityOptions.Public,
                            value: username
                        )
                    }
                }
            );
        }

        #region LobbyCreation
        public static void CreateLobby()
        {
            CreateLobby("default", 4);
        }

        public static void CreateLobby(string lobbyName, int maxPlayers)
        {
            CreateLobby(lobbyName, maxPlayers, null);
        }

        public static void CreateLobby(
            string lobbyName,
            int maxPlayers,
            Dictionary<string, DataObject> data
        )
        {
            CreateLobby(lobbyName, maxPlayers, LobbyAvailability.Public, data);
        }

        public static async void CreateLobby(
            string lobbyName,
            int maxPlayers,
            LobbyAvailability lobbyAvailability,
            Dictionary<string, DataObject> data
        )
        {
            CreateLobbyOptions options = new CreateLobbyOptions();

            options.IsPrivate =
                (lobbyAvailability & LobbyAvailability.Private) == LobbyAvailability.Private;

            options.Player = _player;

            options.Data = data;

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(
                lobbyName,
                maxPlayers,
                options
            );
            _joinedLobby = lobby;
            _hostedLobby = _joinedLobby;
            onLobbyJoin.Invoke(_joinedLobby);
            ListLobbies();
        }

        public static async void ListLobbies()
        {
            try
            {
                QueryResponse response = await LobbyService.Instance.QueryLobbiesAsync();
                List<Lobby> availableLobbies = response.Results;

                Debug.Log("Found " + availableLobbies.Count + " available lobbies");

                onLobbyList?.Invoke(availableLobbies);
                foreach (Lobby lobby in availableLobbies)
                {
                    Debug.Log(lobby.Name + ", Code: " + lobby.LobbyCode);
                }
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError(e);
            }
        }

        public static void HandleLobbyHeartbeat()
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

        public static async void UpdateLobby()
        {
            UpdateLobbyOptions options = new UpdateLobbyOptions();
            Lobby lobby = await LobbyService.Instance.UpdateLobbyAsync(_hostedLobby.Id, options);
        }
        #endregion

        #region LobbyJoin
        public static async void JoinLobbyByCode(string lobbyCode)
        {
            try
            {
                _joinedLobby = await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode);
                JoinLobbyByCodeOptions joinOptions = new JoinLobbyByCodeOptions();
                joinOptions.Player = _player;
                onLobbyJoin.Invoke(_joinedLobby);
            }
            catch (LobbyServiceException exception)
            {
                Debug.LogError(exception);
            }
        }
        #endregion
    };
}
