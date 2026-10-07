using UnityEngine;
using UnityEngine.UI;

public class BloqueoYdesbloqueo : MonoBehaviour
{
   public Slider sliderbloqueo;
   public GameObject pads; 
   private CanvasGroup canvasPads;
    void Start()
    {
        canvasPads = pads.GetComponent<CanvasGroup>();
        sliderbloqueo.value=0;
        sliderbloqueo.onValueChanged.AddListener(CambiarEstado);
        Desbloquear();
    }

    void CambiarEstado(float value)
    {
      if(value>=1)
        {
            Bloquear();
        }  
        else
         {
           Desbloquear(); 
         }
    }

    void Bloquear()
    {
        canvasPads.interactable=false;
        canvasPads.blocksRaycasts=false;
    }

    void Desbloquear()
    {
        canvasPads.interactable=true;
        canvasPads.blocksRaycasts=true;
    }
}
