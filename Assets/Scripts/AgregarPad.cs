using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgregarPad : MonoBehaviour
{
    [Header("Configuración de los pads")]
    [SerializeField] private Transform contenedorPads;
    [SerializeField] private GameObject prefabPad;
    [SerializeField] private Vector2[] posicionesPadsExtra;

    [Header("Configuración de los sliders de volumen")]
    [SerializeField] private Transform contenedorSliders;
    [SerializeField] private GameObject prefabSlider;
    [SerializeField] private Vector2[] posicionesSlidersExtra;

    [Header("Botón para agregar (botón 8)")]
    [SerializeField] private Button botonAgregar;

    private const int MIN_PADS = 6;
    private const int MAX_PADS = 8;
    private const int PADS_POR_PULSACION = 2;

    private readonly List<GameObject> padsInstanciados = new List<GameObject>();
    private readonly List<GameObject> slidersInstanciados = new List<GameObject>();

    private void Start()
    {
        ActualizarBoton();
    }

    public void AgregarDosPads()
    {
        AgregarPads(PADS_POR_PULSACION);
    }

    public void AgregarPads(int cantidad)
    {
        if (contenedorPads == null || prefabPad == null || cantidad <= 0)
        {
            ActualizarBoton();
            return;
        }

        int hijosActuales = contenedorPads.childCount;
        int espaciosDisponibles = MAX_PADS - hijosActuales;
        int cantidadAAgregar = Mathf.Min(cantidad, espaciosDisponibles);

        if (cantidadAAgregar <= 0)
        {
            ActualizarBoton();
            return;
        }

        for (int i = 0; i < cantidadAAgregar; i++)
        {
            int indiceExtra = padsInstanciados.Count;

            GameObject nuevoPadObj = Instantiate(prefabPad, contenedorPads);
            nuevoPadObj.SetActive(true);
            padsInstanciados.Add(nuevoPadObj);

            RectTransform rtPad = nuevoPadObj.GetComponent<RectTransform>();
            if (rtPad != null && posicionesPadsExtra != null && indiceExtra >= 0 && indiceExtra < posicionesPadsExtra.Length)
            {
                rtPad.anchoredPosition = posicionesPadsExtra[indiceExtra];
            }

            if (prefabSlider != null && contenedorSliders != null)
            {
                GameObject nuevoSliderObj = Instantiate(prefabSlider, contenedorSliders);
                nuevoSliderObj.SetActive(true);
                slidersInstanciados.Add(nuevoSliderObj);

                RectTransform rtSlider = nuevoSliderObj.GetComponent<RectTransform>();
                if (rtSlider != null && posicionesSlidersExtra != null && indiceExtra >= 0 && indiceExtra < posicionesSlidersExtra.Length)
                {
                    rtSlider.anchoredPosition = posicionesSlidersExtra[indiceExtra];
                }
            }
        }

        if (Presets.PresetElegido == 1 || Presets.PresetElegido == 2)
        {
            Presets gestorPresets = FindAnyObjectByType<Presets>();
            if (gestorPresets != null)
            {
                gestorPresets.AplicarVisualPreset(Presets.PresetElegido);
            }
        }

        ActualizarBoton();
    }

    public void VolverASeisPads()
    {
        for (int i = padsInstanciados.Count - 1; i >= 0; i--)
        {
            if (padsInstanciados[i] != null)
            {
                Destroy(padsInstanciados[i]);
            }
        }
        padsInstanciados.Clear();

        for (int i = slidersInstanciados.Count - 1; i >= 0; i--)
        {
            if (slidersInstanciados[i] != null)
            {
                Destroy(slidersInstanciados[i]);
            }
        }
        slidersInstanciados.Clear();

        ActualizarBoton();
    }

    private void ActualizarBoton()
    {
        if (botonAgregar != null && contenedorPads != null)
        {
            botonAgregar.interactable = contenedorPads.childCount < MAX_PADS;
        }
    }
}