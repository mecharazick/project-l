using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlphaLobby
{
    public abstract class InputBehaviour : MonoBehaviour
    {
        public abstract void OnValueChanged(string s);

        public abstract void OnEndEdit(string s);

        public abstract void OnSelect(string s);

        public abstract void OnDeselect(string s);
    }
}
