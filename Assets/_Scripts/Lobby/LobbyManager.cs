using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using AlphaLobby;

namespace AlphaLobby.Managers
{
    public class LobbyManager : MonoBehaviour
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

        [SerializeField]
        private Lobby _hostedLobby;

        [SerializeField]
        private Lobby _joinedLobby;

        private float heartbeatTimer = 0f;

        public enum LobbyAvailability
        {
            Public = 0,
            Private = 1
        };

        private void Start()
        {
            Debug.Log(GameObject.Find("AlphaLobby").GetComponent<AlphaLobby>());
        }

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
            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(
                lobbyName,
                maxPlayers,
                new CreateLobbyOptions
                {
                    IsPrivate =
                        (lobbyAvailability & LobbyAvailability.Private)
                        == LobbyAvailability.Private,
                    Player = new Player(AuthenticationService.Instance.PlayerId),
                    Data = data
                }
            );
            _joinedLobby = lobby;
            _hostedLobby = _joinedLobby;
        }

        private async void HandleLobbyHeartbeat()
        {
            if(heartbeatTimer < 0){
                if (_hostedLobby != null)
                {
                    await LobbyService.Instance.GetLobbyAsync(_hostedLobby.Id);
                }
                float heartbeatTimerMax = 1.1f;
                heartbeatTimer = heartbeatTimerMax;
            } else {
                heartbeatTimer -= Time.deltaTime;
            }
        }
        #endregion

        #region LobbyUpdate
        #endregion
    };
}
