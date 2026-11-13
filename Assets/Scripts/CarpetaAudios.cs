using System.IO;
using UnityEngine;

public class CarpetaAudios : MonoBehaviour
{
    public static string RutaAudios{get;private set;}
    void Start()
    {
        RutaAudios = Path.Combine(
            Application.persistentDataPath,
            "Audios"
        );

        Directory.CreateDirectory(RutaAudios);
    }
}
