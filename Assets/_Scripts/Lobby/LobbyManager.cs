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
        public enum LobbyAvailability
        {
            Public = 0,
            Private = 1
        };

        private void Start(){
            Debug.Log(GameObject.Find("AlphaLobby").GetComponent<AlphaLobby>());   
        }

        #region LobbyCreation
        private void CreateLobby(string lobbyName, int maxPlayers)
        {
            CreateLobby(lobbyName, maxPlayers, null);
        }

        private void CreateLobby(
            string lobbyName,
            int maxPlayers,
            Dictionary<string, DataObject> data
        )
        {
            CreateLobby(lobbyName, maxPlayers, LobbyAvailability.Public, data);
        }

        private async void CreateLobby(
            string lobbyName,
            int maxPlayers,
            LobbyAvailability lobbyAvailability,
            Dictionary<string, DataObject> data
        )
        {
            await LobbyService.Instance.CreateLobbyAsync(
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
        }
        #endregion
        
        #region LobbyUpdate
        
        #endregion
    };
}
