using Unity.VisualScripting;
using UnityEngine;

public class AutoDestruccion : MonoBehaviour
{
    private Transform PuntoFinal;
    private Animator anim;
    private float vel = 5f;
    private bool tocado= false;
    public bool Animar = false;
    public bool muerte = false;
    public Vida Scripdevida;

    void Start()
    {
        Scripdevida = GameObject.FindGameObjectWithTag("Player").GetComponent<Vida>();
        PuntoFinal = GameObject.FindGameObjectWithTag("PuntoFinal").transform;
        if (Animar)
        {
        anim = GetComponent<Animator>();
        }
    }
    void Update()
    {
        transform.Translate(Vector3.left * vel * Time.deltaTime);
        if (Vector2.Distance(transform.position, PuntoFinal.position) > 9 || muerte)
        {
            Destroy(gameObject);
        }
    }
    public void Animaciones()
    {
        if (Animar)
        {
            anim.SetBool("Tocado", tocado);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MovimientoTouchScreen MN = collision.GetComponent<MovimientoTouchScreen>();
            MN.Daño = true;
            tocado = true;
            Scripdevida.Dolor(1);
        }
    }
}
