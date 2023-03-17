using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using AlphaLobby.Managers;

namespace AlphaLobby
{
    public class AlphaLobby : MonoBehaviour
    {
        #region Lobby State

        [SerializeField]
        [Tooltip("Armazena o nome de tela deste usuário")]
        static private string _username;
        static public string Username
        {
            get { return _username; }
        }

        #endregion
        [SerializeField]
        static private LobbyManager _lobbyManager;

        static public LobbyManager LobbyManager
        {
            get { return _lobbyManager; }
        }
        
        static private AuthenticationManager _authenticationManager;

        static public AuthenticationManager AuthenticationManager
        {
            get { return _authenticationManager; }
        }

        static public void UpdateUsername(string s)
        {
            _username = s;
        }
    }
}
