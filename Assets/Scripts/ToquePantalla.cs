using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ToquePantalla : MonoBehaviour
{
    [SerializeField] private Transform objeto;


    private void Update()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed) {return;}

        Vector2 posicionToque = Touchscreen.current.primaryTouch.position.ReadValue();

    }
}
