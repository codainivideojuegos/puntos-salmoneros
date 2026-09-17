using System.Collections.Generic;
using UnityEngine;

public class AudioManagerOscar : MonoBehaviour
{
    public static AudioManagerOscar Instance { get; private set; }

    [Header("🎵 Música del juego")]
    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioClip[] audioMusic;
    private int indexCurrentMusic = -1;

    [System.Serializable]
    public class SFX
    {
        public string nameSFX;
        public AudioSource audioSFX;
    }

    [Header("🔊 Efectos de sonido")]
    [SerializeField] private SFX[] sFXArray; 

    private Dictionary<string, AudioSource> sfxDictionary = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Inicializar();  
    }

    public void Inicializar()
    {
        // Llenar el diccionario con los SFX
        foreach (var sfx in sFXArray)
        {
            if (!sfxDictionary.ContainsKey(sfx.nameSFX))
                sfxDictionary.Add(sfx.nameSFX, sfx.audioSFX);
            else
                Debug.LogWarning("SFX duplicado: " + sfx.nameSFX);
        }
    }

    /// <summary>
    /// Cambia la música si no es la misma que está sonando.
    /// </summary>
    public void changeMusic(int index)
    {
        if (indexCurrentMusic != index && index >= 0 && index < audioMusic.Length)
        {
            gameMusic.clip = audioMusic[index];
            gameMusic.Play();
            indexCurrentMusic = index;
        }
        else if (index < 0 || index >= audioMusic.Length)
        {
            Debug.LogWarning("Índice de música fuera de rango: " + index);
        }
    }

    /// <summary>
    /// Detiene la música actual.
    /// </summary>

    public void StopMusic()
    {
        gameMusic.Stop();
        indexCurrentMusic = -1; 
    }

    /// <summary>
    /// Pausa la música actual.
    /// </summary>

    public void PauseMusic()
    {
        gameMusic.Pause();
    }

    /// <summary>
    /// Reanuda la música actual.
    /// </summary>

    public void ReanudarMusic()
    {
        gameMusic.UnPause();
    }

    /// <summary>
    /// Reproduce un efecto de sonido por nombre.
    /// </summary>
    public void changeSFX(string name)
    {
        if (sfxDictionary.TryGetValue(name, out AudioSource audio))
            audio.Play();
        else
            Debug.LogWarning("SFX no encontrado: " + name);
    }

    /// <summary>
    /// + Detiene un efecto de sonido por nombre.
    /// </summary>
    public void StopSFX(string name)
    {
        if (sfxDictionary.TryGetValue(name, out AudioSource audio))
            audio.Stop();
        else
            Debug.LogWarning("SFX no encontrado: " + name);
    }

    /// <summary>
    /// Detiene todos los efectos de sonido.
    /// </summary>
    public void StopAllSFX()
    {
        foreach (var sfx in sfxDictionary.Values)
        {
            sfx.Stop();
        }
    }
}
