using UnityEngine;
using AlphaLobby.Managers;

namespace AlphaLobby
{
    public class AlphaLobby : MonoBehaviour
    {
        #region Lobby State

        [SerializeField]
        [Tooltip("Armazena o nome de tela deste usuário")]
        private static string _username;
        public static string Username
        {
            get { return _username; }
        }

        #endregion
        [SerializeField]
        private static LobbyManager _lobbyManager;

        public static LobbyManager LobbyManager
        {
            get { return _lobbyManager; }
        }

        private static AuthenticationManager _authenticationManager;

        public static AuthenticationManager AuthenticationManager
        {
            get { return _authenticationManager; }
        }

        public static void UpdateUsername(string s)
        {
            _username = s;
        }

        public static void InitializeManager(){
            LobbyManager.Initialize(_username);
        }
    }
}
