using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public PlayerController player;
    public GameObject gameOverGO;
    public TextMeshProUGUI scoreText;
    public AudioClip bgmClip;
    public int score;
    public float scrollSpeed;
    public bool isGameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);

            Debug.LogWarning($"Duplicado eliminado del objeto: '{gameObject.name}'", gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.BGM.Gameplay);
    }

    public void GameOver()
    {
        gameOverGO.SetActive(true);
        isGameOver = true;
    }

    public void AddScore()
    {
        if (!player.IsDead)
        {
            score++;
            scoreText.text = "Puntuación: " + score.ToString("000000");

            AudioManager.Instance.PlaySFX(AudioManager.SFX.Score);
        }
    }
}