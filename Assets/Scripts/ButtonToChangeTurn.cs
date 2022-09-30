using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToChangeTurn : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private ActivePlayerManager manager;
    private void Start()
    {
        _button.onClick.AddListener(ButtonPressed);
    }

    public void ButtonPressed()
    {
        manager.GameStateTurnChange();

    }
}
