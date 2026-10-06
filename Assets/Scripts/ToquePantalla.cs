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
        Vector2 posicionToque;

       
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            posicionToque =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }

      
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.isPressed)
        {
            posicionToque =
                Mouse.current.position.ReadValue();
        }

        
        else
        {
            return;
        }

        Debug.Log("Toque detectado en: " + posicionToque);
    }
}