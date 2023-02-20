using UnityEngine;

namespace AlphaLobby
{
    public class UsernameInput : InputBehaviour
    {
        public override void OnValueChanged(string s)
        {
            GameObject.Find("AlphaLobby").GetComponent<AlphaLobby>().UpdateUsername(s);
        }

        public override void OnEndEdit(string s) { }

        public override void OnSelect(string s) { }

        public override void OnDeselect(string s) { }
    }
}
