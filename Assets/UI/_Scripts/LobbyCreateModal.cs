using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlphaLobby.Managers;

namespace AlphaLobby.UI
{
    public class LobbyCreateModal : MonoBehaviour
    {

        [SerializeField]
        private LobbyManager _lobbyManager;
        private string _lobbyName;
        private int _maxPlayers;
        public void ChangeLobbyName(string s)
        {
            _lobbyName = s;
        }

        public void ChangeMaxPlayers(string s)
        {
            _maxPlayers = int.Parse(s);
        }

        public void ChangeMaxPlayers(int i)
        {
            _maxPlayers = i;
        }

        private void OnAwake()
        {
            ClearFiels();
        }

        public void ConfirmLobbyCreation()
        {
            Debug.Log("Creating Lobby " + _lobbyName + ", with capacity to: " + _maxPlayers + " players.");
            _lobbyManager.CreateLobby(_lobbyName, _maxPlayers);
        }

        private void ClearFiels()
        {
            _lobbyName = string.Empty;
            _maxPlayers = 0;
        }

    }
}