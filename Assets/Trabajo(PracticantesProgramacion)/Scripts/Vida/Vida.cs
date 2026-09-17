using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    public GameObject[] corazones;
    public GameObject Panelmuerte;
    public MovimientoTouchScreen ScriptdeNviel1;
    public Parallax Scripparal;
    public GameObject coso;
    public int MaxVida = 3;
    public int vida;

    void Awake()
    {
        vida = MaxVida;
        Panelmuerte.SetActive(false);
    }
    public void Dolor(int cantidad)
    {
        vida -= cantidad;
        
        if (vida < 0) vida = 0; 

        Actualizar();

        if (vida <= 0)
        {
            ScriptdeNviel1.anim.SetBool("IsDead",true);
            ScriptdeNviel1.Estamuerto();
            Scripparal.vel=0f;
            Panelmuerte.SetActive(true);
            coso.SetActive(false);
        }
    }

    void Actualizar()
    {
        int cont = 1;
        foreach (GameObject corazon in corazones)
        {
            corazon.SetActive(vida >= cont);
            cont++;
        }
    }
    public void Reinicio()
    {
        int Escenaactual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(Escenaactual);
    }
}