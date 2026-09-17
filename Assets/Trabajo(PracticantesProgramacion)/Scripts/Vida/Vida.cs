using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    public GameObject cora1;
    public GameObject cora2;
    public GameObject cora3;
    public GameObject Panelmuerte;
    public MovimientoNivel1 ScriptdeNviel1;
    public Parallax Scripparal;
    public GameObject coso;
    public int vida = 3;

    void Start()
    {

        Panelmuerte.SetActive(false);
    }
    void Update()
    {
        Actualizar();
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
        cora1.SetActive(vida >= 1);
        cora2.SetActive(vida >= 2);
        cora3.SetActive(vida >= 3);
    }
    public void Reinicio()
    {
        int Escenaactual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(Escenaactual);
    }
}