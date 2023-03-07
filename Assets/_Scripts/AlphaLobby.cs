using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

namespace AlphaLobby
{
    public class AlphaLobby : MonoBehaviour
    {
        #region Lobby State

        [SerializeField]
        [Tooltip("Armazena o nome de tela deste usuário")]
        private string _username;
        public string Username
        {
            get { return _username; }
        }

        #endregion

        public void UpdateUsername(string s)
        {
            _username = s;
        }

        protected async void Start()
        {
            await UnityServices.InitializeAsync();
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (AuthenticationException e)
            {
                Debug.LogError(e);
            }
            finally
            {
                Debug.Log("Did SignIn Succesfullly? " + AuthenticationService.Instance.IsSignedIn);
            }
        }
    }
}
