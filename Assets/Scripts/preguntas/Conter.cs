using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Conter : MonoBehaviour
{
    public int puntu;
    public TMP_Text textonumerorespuestas;
    public TMP_Text textonumerorespuestas2;
    public GameObject canvabueno;
    void Start()
    {
        canvabueno.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Check());
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(2);
        }
    }
    IEnumerator Check()
    {
        canvabueno.SetActive(true);
        textonumerorespuestas.text = "Respondiste " + puntu + " preguntas de 3!";
        if(puntu==3)
        {
            textonumerorespuestas2.text = "Felizcidades!";
        }
        else
        {
            textonumerorespuestas2.text = "suicidate";
        }
        yield return new WaitForSeconds(1.0f);
        canvabueno.SetActive(false);
    }
}
