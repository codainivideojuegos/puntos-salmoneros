using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI restartText;

    private readonly WaitForSeconds waitForSeconds0_5 = new(0.5f);

    private void Start()
    {
        StartCoroutine(BlinkText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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