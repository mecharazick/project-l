using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services;
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

        static public async void Authenticate()
        {
            try
            {
                if (AlphaLobby.Username == string.Empty)
                {
                    throw new System.Exception("User must provide a name");
                }
                Debug.Log("Authenticating user: " + AlphaLobby.Username);

                InitializationOptions initializationOptions = new InitializationOptions();

                initializationOptions.SetProfile(AlphaLobby.Username);

                await UnityServices.InitializeAsync(initializationOptions);

                AuthenticationService.Instance.SignedIn += () =>
                {
                    Debug.Log("Signed In!" + AuthenticationService.Instance.PlayerId);
                };

                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (AuthenticationException exception)
            {
                Debug.LogError(exception);
            }
        }
    }
}
