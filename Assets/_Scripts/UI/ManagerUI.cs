using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaLobby
{
    public class ManagerUI : MonoBehaviour
    {
        public void OpenGUI()
        {
            this.gameObject.SetActive(true);
        }
        public void CloseGUI()
        {
            this.gameObject.SetActive(false);
        }
    }
}
