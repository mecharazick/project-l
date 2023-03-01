using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaLobby
{
    public class ManagerUI : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Define as telas exibidas durante o fluxo de criação dos Lobbies e ativa a primeira tela da lista")]
        private List<GameObject> _screens;

        private void Start()
        {
            int i = 0;

            foreach (GameObject screen in _screens)
            {
                CloseGUI(screen);
                if (i == 0)
                {
                    OpenGUI(screen);
                }
                i++;
            }
        }

        public void OpenGUI(GameObject gui)
        {
            gui.SetActive(true);
        }
        public void CloseGUI(GameObject gui)
        {
            gui.SetActive(false);
        }
    }
}
