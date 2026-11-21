using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GestorAsignacionSonidos : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private IdentificadoSonidos identificadorSonidos;
    [SerializeField] private Transform contenedorPads; // El objeto 'pads'
    [SerializeField] private List<TMP_Text> textosFilasBiblioteca = new List<TMP_Text>(); // textopad01..06
    [SerializeField] private TMP_Text textoPadSeleccionado;

    private int padSeleccionadoIndex = 0;

    private void OnEnable()
    {
        if (identificadorSonidos != null)
            identificadorSonidos.OnSonidoAgregado += AsignarClipAPadSeleccionado;
    }

    private void OnDisable()
    {
        if (identificadorSonidos != null)
            identificadorSonidos.OnSonidoAgregado -= AsignarClipAPadSeleccionado;
    }

    
    public void SeleccionarPad(int indice)
    {
        padSeleccionadoIndex = Mathf.Max(0, indice);
        if (textoPadSeleccionado != null)
            textoPadSeleccionado.text = $"Destino: Pad {padSeleccionadoIndex + 1:00}";
    }

    
    private void AsignarClipAPadSeleccionado(AudioClip clip)
    {
        if (clip == null || contenedorPads == null) return;
        if (padSeleccionadoIndex < 0 || padSeleccionadoIndex >= contenedorPads.childCount) return;

        Transform padObj = contenedorPads.GetChild(padSeleccionadoIndex);
        AudioSource fuente = padObj.GetComponent<AudioSource>();
        if (fuente == null)
            fuente = padObj.gameObject.AddComponent<AudioSource>();

        fuente.clip = clip;

        if (padSeleccionadoIndex < textosFilasBiblioteca.Count && textosFilasBiblioteca[padSeleccionadoIndex] != null)
        {
            textosFilasBiblioteca[padSeleccionadoIndex].text = $"Pad {padSeleccionadoIndex + 1:00} - {clip.name}";
        }
    }
}