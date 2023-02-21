using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

namespace AlphaLobby.Managers
{
    public class AuthenticationManager : MonoBehaviour
    {
        // #region AuthenticationManager Singleton Constructor Definition
        // private static AuthenticationManager _authenticationManagerSingleton;
        // public static AuthenticationManager Instance
        // {
        //     get
        //     {
        //         if (_authenticationManagerSingleton == null)
        //             _authenticationManagerSingleton = new AuthenticationManager();
        //         return _authenticationManagerSingleton;
        //     }
        //     private set { _authenticationManagerSingleton = Instance; }
        // }

        // private AuthenticationManager() { }
        // #endregion

        public async void AuthenticateGuest(string username)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}
