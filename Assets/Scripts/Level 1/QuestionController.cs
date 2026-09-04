using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionController : MonoBehaviour
{
    public GameObject questionsGO;

    [SerializeField] private List<Questions> questionsList;
    [SerializeField] private Questions currentQuestion;
    [SerializeField] private TextMeshProUGUI shadowQuestionText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI shadowAnswerText1;
    [SerializeField] private TextMeshProUGUI answerText1;
    [SerializeField] private TextMeshProUGUI shadowAnswerText2;
    [SerializeField] private TextMeshProUGUI answerText2;
    [SerializeField] private TextMeshProUGUI shadowAnswerText3;
    [SerializeField] private TextMeshProUGUI answerText3;
    [SerializeField] private TextMeshProUGUI shadowAnswerText4;
    [SerializeField] private TextMeshProUGUI answerText4;

    private void Start()
    {
        questionsGO.SetActive(false);
    }

    public void ActivateQuestion()
    {
        Debug.Log("Activar pregunta.");

        UIManager.Instance.OpenPanel(questionsGO);

        if (questionsList.Count == 0)
        {
            return;
        }

        int index = Random.Range(0, questionsList.Count);

        currentQuestion = questionsList[index];
        shadowQuestionText.text = currentQuestion.question;
        questionText.text = currentQuestion.question;
        shadowAnswerText1.text = currentQuestion.answer1;
        answerText1.text = currentQuestion.answer1;
        shadowAnswerText2.text = currentQuestion.answer2;
        answerText2.text = currentQuestion.answer2;
        shadowAnswerText3.text = currentQuestion.answer3;
        answerText3.text = currentQuestion.answer3;
        shadowAnswerText4.text = currentQuestion.answer4;
        answerText4.text = currentQuestion.answer4;
    }

    public void Reply(int answer)
    {
        if (currentQuestion == null)
        {
            return;
        }

        if (answer == currentQuestion.correctAnswer)
        {
            Debug.Log("Respuesta correcta.");

            UIManager.Instance.ClosePanel(questionsGO);

            Invoke(nameof(RestartGameMethod), 1.5f);
        }
        else
        {
            Debug.Log("Respuesta incorrecta.");
        }
    }

    public void RestartGameMethod()
    {
        GameController.Instance.transition.StartScaleOut();

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitUntil(() => !GameController.Instance.transition.isTransitioning);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

[System.Serializable]
public class Questions
{
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public int correctAnswer;
}