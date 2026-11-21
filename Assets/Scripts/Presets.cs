using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class PadData
{
    public string rutaAudio;
    public float volumen = 1f;
    public bool activo = true;
}

public class Presets : MonoBehaviour
{
    // 0 = Sin preset elegido (aspecto inicial), 1 = Preset 1, 2 = Preset 2
    public static int PresetElegido { get; private set; } = 0;
    private static bool aplicarPresetPendiente = false;

    [Header("Escenas")]
    public string escenaPrincipal = "appmidinueva";
    public string escenaPresets = "presets";

    [Header("Referencias visuales en appmidinueva")]
    public Image fondoEscena;
    public Image[] botonesPads;
    public Image padExtra1;
    public Image padExtra2;

    [Header("Preset 1 - Sprites y Colores")]
    public Sprite spriteFondoPreset1;
    public Color colorFondoPreset1 = Color.white;
    public Sprite[] spritesBotonesPreset1;
    public Color colorBotonesPreset1 = Color.white;
    public Sprite[] pressedSpritesPreset1;

    [Header("Preset 2 - Sprites y Colores")]
    public Sprite spriteFondoPreset2;
    public Color colorFondoPreset2 = Color.white;
    public Sprite[] spritesBotonesPreset2;
    public Color colorBotonesPreset2 = Color.white;
    public Sprite[] pressedSpritesPreset2;

    [Header("Configuracion del Preset inicial (JSON)")]
    [SerializeField]
    private List<PadData> configuracionInicial = new List<PadData>();

    public List<PadData> PadsActuales { get; private set; }
    public int PresetActual { get; private set; }

    private Sprite[] pressedSpritesIniciales;
    private Sprite pressedSpriteExtra1Inicial;
    private Sprite pressedSpriteExtra2Inicial;
    private string CarpetaPresets => Path.Combine(Application.persistentDataPath, "Presets");

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ReiniciarAlArrancarApp()
    {
        PresetElegido = 0;
        aplicarPresetPendiente = false;
    }

    private void Start()
    {
        Directory.CreateDirectory(CarpetaPresets);
        GuardarPressedSpritesIniciales();
        CargarPreset(0);

        if (aplicarPresetPendiente &&
            (PresetElegido == 1 || PresetElegido == 2) &&
            (fondoEscena != null || (botonesPads != null && botonesPads.Length > 0)))
        {
            AplicarVisualPreset(PresetElegido);
            aplicarPresetPendiente = false;
        }
    }

    public void AbrirEscenaPresets()
    {
        SceneManager.LoadScene(escenaPresets);
    }

    public void SeleccionarPreset1()
    {
        PresetElegido = 1;
        aplicarPresetPendiente = true;
        CargarPreset(1);
        SceneManager.LoadScene(escenaPrincipal);
    }

    public void SeleccionarPreset2()
    {
        PresetElegido = 2;
        aplicarPresetPendiente = true;
        CargarPreset(2);
        SceneManager.LoadScene(escenaPrincipal);
    }

    private void GuardarPressedSpriteDePad(Image pad, ref Sprite guardado)
    {
        if (pad == null) return;

        Button btn = pad.GetComponent<Button>();
        if (btn != null)
        {
            guardado = btn.spriteState.pressedSprite;
        }
    }

    private void GuardarPressedSpritesIniciales()
    {
        if (botonesPads != null)
        {
            pressedSpritesIniciales = new Sprite[botonesPads.Length];
            for (int i = 0; i < botonesPads.Length; i++)
            {
                if (botonesPads[i] == null) continue;
                GuardarPressedSpriteDePad(botonesPads[i], ref pressedSpritesIniciales[i]);
            }
        }

        GuardarPressedSpriteDePad(padExtra1, ref pressedSpriteExtra1Inicial);
        GuardarPressedSpriteDePad(padExtra2, ref pressedSpriteExtra2Inicial);
    }

    private void AplicarEstiloAPad(
        Image pad,
        Sprite nuevoSprite,
        Color colorActivo,
        Sprite pressedPreset,
        Sprite pressedInicial)
    {
        if (pad == null) return;

        if (nuevoSprite != null)
        {
            pad.sprite = nuevoSprite;
        }

        pad.color = colorActivo;

        Button btn = pad.GetComponent<Button>();
        if (btn != null)
        {
            Sprite objetivoPressed = pressedPreset != null ? pressedPreset : pressedInicial;
            if (objetivoPressed != null)
            {
                SpriteState estado = btn.spriteState;
                estado.pressedSprite = objetivoPressed;
                btn.spriteState = estado;
            }
        }
    }

    public void AplicarVisualPreset(int numeroPreset)
    {
        bool esUno = (numeroPreset == 1);

        if (fondoEscena != null)
        {
            Sprite nuevoFondo = esUno ? spriteFondoPreset1 : spriteFondoPreset2;
            if (nuevoFondo != null)
            {
                fondoEscena.sprite = nuevoFondo;
            }
            fondoEscena.color = esUno ? colorFondoPreset1 : colorFondoPreset2;
        }

        Sprite[] spritesActivos = esUno ? spritesBotonesPreset1 : spritesBotonesPreset2;
        Color colorActivo = esUno ? colorBotonesPreset1 : colorBotonesPreset2;
        Sprite[] pressedActivos = esUno ? pressedSpritesPreset1 : pressedSpritesPreset2;

        if (botonesPads != null)
        {
            for (int i = 0; i < botonesPads.Length; i++)
            {
                if (botonesPads[i] == null) continue;

                Sprite spritePad = (spritesActivos != null && i < spritesActivos.Length) ? spritesActivos[i] : null;
                Sprite pressedPad = (pressedActivos != null && i < pressedActivos.Length) ? pressedActivos[i] : null;
                Sprite pressedInicial = (pressedSpritesIniciales != null && i < pressedSpritesIniciales.Length) ? pressedSpritesIniciales[i] : null;

                AplicarEstiloAPad(botonesPads[i], spritePad, colorActivo, pressedPad, pressedInicial);
            }
        }

        if (spritesActivos != null && spritesActivos.Length > 0)
        {
            Sprite spriteExtra1 = spritesActivos.Length > 6 && spritesActivos[6] != null
                ? spritesActivos[6]
                : spritesActivos[spritesActivos.Length - 1];

            Sprite spriteExtra2 = spritesActivos.Length > 7 && spritesActivos[7] != null
                ? spritesActivos[7]
                : spriteExtra1;

            Sprite pressedExtra1 = (pressedActivos != null && pressedActivos.Length > 0)
                ? (pressedActivos.Length > 6 && pressedActivos[6] != null ? pressedActivos[6] : pressedActivos[pressedActivos.Length - 1])
                : null;

            Sprite pressedExtra2 = (pressedActivos != null && pressedActivos.Length > 0)
                ? (pressedActivos.Length > 7 && pressedActivos[7] != null ? pressedActivos[7] : pressedExtra1)
                : null;

            AplicarEstiloAPad(padExtra1, spriteExtra1, colorActivo, pressedExtra1, pressedSpriteExtra1Inicial);
            AplicarEstiloAPad(padExtra2, spriteExtra2, colorActivo, pressedExtra2, pressedSpriteExtra2Inicial);
        }
    }

    private void CargarPreset(int numeroPreset)
    {
        PresetActual = numeroPreset;
        PadsActuales = new List<PadData>();

        if (configuracionInicial != null)
        {
            for (int i = 0; i < configuracionInicial.Count; i++)
            {
                PadData origen = configuracionInicial[i];
                if (origen == null) continue;

                PadsActuales.Add(new PadData
                {
                    rutaAudio = origen.rutaAudio,
                    volumen = origen.volumen,
                    activo = origen.activo
                });
            }
        }

        string rutaArchivo = Path.Combine(CarpetaPresets, $"preset_{numeroPreset}.json");
        if (numeroPreset > 0 && File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            PadDataListWrapper wrapper = JsonUtility.FromJson<PadDataListWrapper>(json);
            if (wrapper != null && wrapper.pads != null && wrapper.pads.Count > 0)
            {
                PadsActuales = wrapper.pads;
            }
        }
    }

    [Serializable]
    private class PadDataListWrapper
    {
        public List<PadData> pads = new List<PadData>();
    }
}