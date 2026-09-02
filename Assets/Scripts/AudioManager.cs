using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Enumeraciones

    // Enumeración de la música de fondo.
    public enum BGM
    {
        Gameplay // Partida.
    }

    // Enumeración de los efectos de sonido.
    public enum SFX
    {
        Jump,     // Salto.
        Hit,      // Golpe.
        Score,    // Puntuación.
        GameOver  // Fin del juego.
    }

    #endregion

    #region Variables

    public static AudioManager Instance; // Instancia única del AudioManager.

    [Header("Fuentes de Audio")]
    public AudioMixer audioMixer; // Referencia al AudioMixer del audio.
    public AudioSource bgmSource; // Referencia a el AudioSource de la música de fondo.
    public AudioSource sfxSource; // Referencia a el AudioSource de los efectos de sonido.
    public AudioClip[] bgmClips;  // Arreglo de los AudioClips de las músicas de fondo.
    public AudioClip[] sfxClips;  // Arreglo de los AudioClips de los efectos de sonido.
    public float bgmVolume;       // Volumen de la música de fondo.
    public float sfxVolume;       // Volumen de los efectos de sonido.

    #endregion

    #region Métodos de Unity

    // Se llama al despertar el objeto.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"Duplicado eliminado del objeto: {gameObject.name}", gameObject);

            Destroy(gameObject);
        }
    }

    // Se llama al destruir el objeto.
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    #endregion

    #region Control de Audio

    // Reproduce la música de fondo.
    public void PlayBGM(BGM bgm)
    {
        bgmSource.clip = bgmClips[(int)bgm];
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // Reproduce los efectos de sonido.
    public void PlaySFX(SFX sfx)
    {
        sfxSource.PlayOneShot(sfxClips[(int)sfx]);
    }

    // Detiene la música de fondo.
    public void StopBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
        bgmSource.loop = false;
    }

    #endregion

    #region Configuración del Audio

    public void ApplyBGMVolume()
    {
        if (bgmVolume <= 0f)
        {
            audioMixer.SetFloat("BGMVolume", -80f);
        }
        else
        {
            float dB = 20f * Mathf.Log10(bgmVolume / 100f);

            audioMixer.SetFloat("BGMVolume", dB);
        }
    }

    public void ApplySFXVolume()
    {
        if (sfxVolume <= 0f)
        {
            audioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            float dB = 20f * Mathf.Log10(sfxVolume / 100f);

            audioMixer.SetFloat("SFXVolume", dB);
        }
    }

    #endregion
}