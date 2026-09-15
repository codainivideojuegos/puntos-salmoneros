using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Vida")]
    [SerializeField] private GameObject prefabVida;
    [SerializeField] private Transform corazones;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Inicializar();
    }

    private void Inicializar()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(prefabVida, corazones);
        }
    }

    public void ActualizarVida(int vida)
    {
        if (corazones.childCount > vida)
        {
            Destroy(corazones.GetChild(corazones.childCount - 1).gameObject);
        }
    }

    public IEnumerator HaciendoFade(string DedondeViene = "")
    {
        if (DedondeViene == "Player")
            yield return new WaitForSeconds(2f);
            
        yield return Fade();

        SceneManager.LoadScene("Menu");
    }

    private IEnumerator Fade()
    {
        yield return null;
    }
}
