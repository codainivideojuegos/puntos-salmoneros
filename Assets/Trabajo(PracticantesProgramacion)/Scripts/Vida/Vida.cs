using System.Collections;
using Unity.VisualScripting;
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
    public float TempoInmunidad; 

    void Awake()
    {
        EstablecerVida();
    }
    public void Dolor(int cantidad)
    {
        vida -= cantidad;
        if (vida < 0) vida = 0; 
        Actualizar();
        ScriptdeNviel1.Daño = true;
        if (vida <= 0)
        {
            ScriptdeNviel1.anim.SetBool("IsDead",true);
            ScriptdeNviel1.Estamuerto();
            Scripparal.vel=0f;
            Panelmuerte.SetActive(true);
            coso.SetActive(false);
        }
    }
    public void NoDaño()
    {
        ScriptdeNviel1.Daño = false;
    }
    public void EstablecerVida()
    {
        vida = MaxVida;
        Panelmuerte.SetActive(false);
        Actualizar();
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