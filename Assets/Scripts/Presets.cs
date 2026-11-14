
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PadData
{
    public string rutaAudio;
    public float volumen = 1f;
    public bool activo = true;
}

public class Presets : MonoBehaviour
{
    [Header("Configuracion del Preset inicial")]

    [SerializeField]
    private List<PadData> configuracionInicial = new List<PadData>();
    public List<PadData> PadsActuales { get; private set; }
    public int PresetActual { get; private set; }
    private string CarpetaPresets => Path.Combine(Application.persistentDataPath, "Presets");

    private void Start()
    {
        Directory.CreateDirectory(CarpetaPresets);
        CargarPreset(0);
    }

    public void CargarPreset(int numeroPreset)
    {
        if (numeroPreset < 0 || numeroPreset > 2) return;

        if (numeroPreset == 0)
        {
            PadsActuales = CopiarLista(configuracionInicial);
        }
        else
        {
            string ruta = ObtenerRutaPreset(numeroPreset);

            if (!File.Exists(ruta))
            {
                Debug.LogWarning("El preset no tiene datos guardados.");
                return;
            }

            string json = File.ReadAllText(ruta);

            PadListWrapper datos = JsonUtility.FromJson<PadListWrapper>(json);

            if (datos == null || datos.pads == null) return;

            PadsActuales = CopiarLista(datos.pads);
        }

        PresetActual = numeroPreset;

        Debug.Log("Preset " + numeroPreset + " cargado correctamente.");
    }

    public void GuardarPreset(int numeroPreset)
    {
        if (numeroPreset != 1 && numeroPreset != 2) return;

        if (PadsActuales == null) return;

        PadListWrapper datos = new PadListWrapper();

        datos.pads = CopiarLista(PadsActuales);

        string json = JsonUtility.ToJson(datos, true);

        File.WriteAllText(
            ObtenerRutaPreset(numeroPreset),
            json
        );

        PresetActual = numeroPreset;

        Debug.Log("Preset " + numeroPreset + " guardado correctamente.");
    }

    private string ObtenerRutaPreset(int numeroPreset)
    {
        return Path.Combine(CarpetaPresets,"preset_" + numeroPreset + ".json");
    }

    private List<PadData> CopiarLista(List<PadData> original)
    {
        List<PadData> copia = new List<PadData>();

        if (original == null) return copia;

        foreach (PadData pad in original)
        {
            copia.Add(new PadData
            {
                rutaAudio = pad.rutaAudio,
                volumen = pad.volumen,
                activo = pad.activo
            });
        }

        return copia;
    }

    [Serializable]
    private class PadListWrapper
    {
        public List<PadData> pads;
    }
}
