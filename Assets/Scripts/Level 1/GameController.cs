using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public QuestionController question;
    public PlayerController player;
    public Transition transition;
    public GameObject gameOverGO;
    public TextMeshProUGUI shadowScoreText;
    public TextMeshProUGUI scoreText;
    public int score;
    public float scrollSpeed;
    public bool isGameOver;
    public bool isGameStarted;

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

        transition.gameObject.SetActive(true);
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
        isGameStarted = false;

        AudioManager.Instance.StopBGM();

        Invoke(nameof(PlayBGM), 0.5f);
    }

    private void Update()
    {
        if (score >= 10 && !player.IsDead && !player.IsStopped)
        {
            player.Stop();
        }
    }

    private void PlayBGM()
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
            shadowScoreText.text = score.ToString();
            scoreText.text = score.ToString();

            AudioManager.Instance.PlaySFX(AudioManager.SFX.Score);
        }
    }
}