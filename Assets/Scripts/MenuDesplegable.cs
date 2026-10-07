using UnityEngine;
using UnityEngine.UI;

public class MenuDesplegable : MonoBehaviour
{
    public Slider sliderBloqueo;
    public  GameObject menu;
    void Start()
    {
        sliderBloqueo.onValueChanged.AddListener(CambiarMenu);

        //Desbloqueo
        menu.SetActive(false);
    }

    void CambiarMenu(float value)
    {
        if(value>=1)
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
        menu.SetActive(true);
    }

    void OcultarMenu()
    {
        menu.SetActive(false);
    }
}
