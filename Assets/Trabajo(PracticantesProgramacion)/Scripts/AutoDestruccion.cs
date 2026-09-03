using Unity.VisualScripting;
using UnityEngine;

public class AutoDestruccion : MonoBehaviour
{
    private Transform PuntoFinal;
    private Animator anim;
    private Vector3 dir = Vector3.left;
    private float vel = 5f;
    private bool tocado= false;
    public bool explosion = false, muerte = false;
    void Start()
    {
        PuntoFinal = GameObject.FindGameObjectWithTag("PuntoFinal").transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(dir * vel * Time.deltaTime);
        anim.SetBool("Tocado", tocado);
        anim.SetBool("Explosion", explosion);
        if (Vector2.Distance(transform.position, PuntoFinal.position) > 20 || muerte)
        {
            Muerte();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MovimientoNivel1 MN = collision.GetComponent<MovimientoNivel1>();
            MN.Daño = true;
            tocado = true;
        }
    }
    public void Muerte()
    {
        Destroy(gameObject);
    }
}
