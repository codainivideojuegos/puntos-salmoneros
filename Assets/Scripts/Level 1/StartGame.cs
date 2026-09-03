using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI shadowStartText;
    [SerializeField] private TextMeshProUGUI startText;

    private readonly WaitForSeconds waitForSeconds0_5 = new(0.5f);

    private void Start()
    {
        StartCoroutine(BlinkText());

        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameController.Instance.isGameStarted)
        {
            StartGameMethod();
        }
    }

    public void StartGameMethod()
    {
        GameController.Instance.isGameStarted = true;
        GameController.Instance.player.ResetPlayer();

        gameObject.SetActive(false);
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            shadowStartText.enabled = true;
            startText.enabled = true;

            yield return waitForSeconds0_5;

            shadowStartText.enabled = false;
            startText.enabled = false;

            yield return waitForSeconds0_5;
        }
    }
}