using TMPro;
using UnityEngine;

public class Tiempo : MonoBehaviour
{
    private float tiempoTranscurrido = 0f;
    private TMP_Text TMP_Text;
    void Start()
    {
        TMP_Text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        if(tiempoTranscurrido >= 0 && tiempoTranscurrido < 10)
        {
            TMP_Text.text = "0" + Mathf.FloorToInt(tiempoTranscurrido).ToString();
        }
        else if(tiempoTranscurrido >= 10 )
        {
            TMP_Text.text = Mathf.FloorToInt(tiempoTranscurrido).ToString();
        }
    }
}
