using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class IdentificadoSonidos : MonoBehaviour
{
    [Header("UI de Biblioteca")]
    public Text nombreAudio;
    public Text textoRutaCarpeta;
    public string nombreCarpeta = "Biblioteca Sonidos";

    [Header("Resultado")]
    public AudioClip audioSeleccionado;
    public List<AudioClip> sonidosAgregados = new List<AudioClip>();
    public event Action<AudioClip> OnSonidoAgregado;

    public string RutaCarpetaBiblioteca { get; private set; }

    private void Awake()
    {
        PrepararCarpetaBiblioteca();
    }

    public string PrepararCarpetaBiblioteca()
    {
        RutaCarpetaBiblioteca = Path.Combine(Application.persistentDataPath, nombreCarpeta);

        if (!Directory.Exists(RutaCarpetaBiblioteca))
        {
            Directory.CreateDirectory(RutaCarpetaBiblioteca);
        }

        if (textoRutaCarpeta != null)
        {
            textoRutaCarpeta.text = $"Carpeta: {RutaCarpetaBiblioteca}";
        }

        return RutaCarpetaBiblioteca;
    }

    public void seleccionarAudio()
    {
        if (NativeFilePicker.IsFilePickerBusy())
            return;

        string rutaInicial = PrepararCarpetaBiblioteca();

        NativeFilePicker.PickFile((path) =>
        {
            if (string.IsNullOrEmpty(path)) return;

            if (Path.GetExtension(path).ToLower() != ".wav")
            {
                if (nombreAudio != null)
                    nombreAudio.text = "Solo archivos .WAV";
                return;
            }

            StartCoroutine(CargarAudio(path));
        }, rutaInicial, "audio/*");
    }

    private IEnumerator CargarAudio(string path)
    {
        string ruta = path.Contains("://") ? path : new Uri(path).AbsoluteUri;

        using (UnityWebRequest solicitud = UnityWebRequestMultimedia.GetAudioClip(ruta, AudioType.WAV))
        {
            yield return solicitud.SendWebRequest();

            if (solicitud.result == UnityWebRequest.Result.Success)
            {
                AudioClip clipCargado = DownloadHandlerAudioClip.GetContent(solicitud);
                if (clipCargado != null)
                {
                    clipCargado.name = Path.GetFileNameWithoutExtension(path);
                    audioSeleccionado = clipCargado;
                    sonidosAgregados.Add(clipCargado);

                    if (nombreAudio != null)
                        nombreAudio.text = clipCargado.name;

                    OnSonidoAgregado?.Invoke(clipCargado);
                }
            }
            else if (nombreAudio != null)
            {
                nombreAudio.text = "Error al cargar audio";
            }
        }
    }
}