using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject panelOpciones;

    public void Jugar()
    {
        SceneManager.LoadScene("Movimiento");
    }

    public void Opciones()
    {
        panelOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        panelOpciones.SetActive(false);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");

        Application.Quit();
    }
}