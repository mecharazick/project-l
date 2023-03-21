using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AlphaLobby.Interfaces
{
    public interface ILobbyEvents {
        static UnityEvent onLobbyCreate;
        static UnityEvent onLobbyJoin;
        static UnityEvent onLobbyQuit;
        static UnityEvent onLobbyList;
    }
}
