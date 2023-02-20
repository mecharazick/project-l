using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlphaLobby.Managers;

namespace AlphaLobby
{
    public class AuthenticateBtn : MonoBehaviour
    {
        public void OnClick()
        {
            GameObject
                .Find("Manager")
                .GetComponent<AuthenticationManager>()
                .AuthenticateGuest(
                    GameObject.Find("AlphaLobby").GetComponent<AlphaLobby>().Username
                );
        }
    }
}
