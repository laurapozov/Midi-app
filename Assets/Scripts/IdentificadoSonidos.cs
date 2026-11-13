using System;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class IdentificadoSonidos : MonoBehaviour
{
   public TMP_Text nombreAudio;
   public AudioClip audioSeleccionado;


    public void seleccionarAudio()
    {
        if (NativeFilePicker.IsFilePickerBusy()) 
            return;

        NativeFilePicker.PickFile((path)=>
        {
            if (string.IsNullOrEmpty(path)) return;

            if (Path.GetExtension(path).ToLower() != ".wav") return;

            StartCoroutine(CargarAudio(path));
        }, "audio/*");
    }
    private IEnumerator CargarAudio(String path)
    {
        string ruta = path.Contains("://")? path : new System.Uri(path).AbsoluteUri;
        
        using (UnityWebRequest solicitud = UnityWebRequestMultimedia.GetAudioClip(ruta,AudioType.WAV))

        {
            yield return solicitud.SendWebRequest();

            if (solicitud.result == UnityWebRequest.Result.Success)
            {
                audioSeleccionado = DownloadHandlerAudioClip.GetContent (solicitud);

                audioSeleccionado.name = Path.GetFileNameWithoutExtension (path);

                nombreAudio.text = audioSeleccionado.name;
            }


                
            
        }
    }
}

