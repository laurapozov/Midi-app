using UnityEngine;
using UnityEngine.UI;

public class MenuDesplegable : MonoBehaviour
{
    public Slider sliderBloqueo;
    public GameObject menu;
    public GameObject botonMenu;

    void Start()
    {
        sliderBloqueo.onValueChanged.AddListener(CambiarMenu);

        botonMenu.SetActive(false);
        menu.SetActive(false);
    }

    void CambiarMenu(float value)
    {
        if (value >= 1)
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
        botonMenu.SetActive(true);
        menu.SetActive(false);
    }

    void OcultarMenu()
    {
        botonMenu.SetActive(false);
        menu.SetActive(false);
    }

    public void AlternarIconosMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}