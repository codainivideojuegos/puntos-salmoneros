using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI restartText;

    private readonly WaitForSeconds waitForSeconds0_5 = new(0.5f);

    private void Start()
    {
        StartCoroutine(BlinkText());

        EventSystem.current.SetSelectedGameObject(restartButton.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            restartText.enabled = true;

            yield return waitForSeconds0_5;

            restartText.enabled = false;

            yield return waitForSeconds0_5;
        }
    }
}