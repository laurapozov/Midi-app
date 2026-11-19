using UnityEngine;
using UnityEngine.UI;

public class MenuDesplegable : MonoBehaviour
{
    public Slider sliderBloqueo;
    public GameObject menu;
    public GameObject botonMenu;

    void Start()
    {
        if (sliderBloqueo != null)
        {
            sliderBloqueo.onValueChanged.AddListener(CambiarMenu);
        }
        else
        {
            Debug.LogWarning("MenuDesplegable: falta asignar 'sliderBloqueo'.", this);
        }

        OcultarMenu();
    }

    void CambiarMenu(float value)
    {
        if (value >= 1f)
        {
            MostrarMenu();
        }
        else
        {
            OcultarMenu();
        }
    }

    void MostrarMenu()
    {
        if (botonMenu != null)
        {
            botonMenu.SetActive(true);
        }

        // Si quieres que 'menu' aparezca solo al pulsar botonMenu, déjalo en false.
        // Si quieres que aparezca de inmediato al deslizar arriba, cámbialo a true.
        if (menu != null)
        {
            menu.SetActive(false);
        }
    }

    void OcultarMenu()
    {
        if (botonMenu != null)
        {
            botonMenu.SetActive(false);
        }

        if (menu != null)
        {
            menu.SetActive(false);
        }
    }

    public void AlternarIconosMenu()
    {
        if (menu != null)
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}