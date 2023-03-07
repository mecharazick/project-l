using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControlInputNumber : MonoBehaviour
{

    [SerializeField]
    private Button _upArrow, _downArrow;

    private void Start()
    {
        _upArrow.onClick.AddListener(upCount);
        _downArrow.onClick.AddListener(reduceCount);
    }

    private void upCount(){

    }
    
    private void reduceCount(){

    }
}
