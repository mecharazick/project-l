using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyListManagerUI : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Referencia à tabela de listagem de lobbies")]
    private GameObject LobbyTable;
    
    [SerializeField]
    private GameObject TableRow; 

    private float _rowsStartHeight;

    private void Start(){
        _rowsStartHeight = GetHeight(LobbyTable.transform.GetChild(0));
    }

    private float GetHeight(Transform gObject){
        return ((RectTransform)gObject.transform).rect.height;
    }

}
