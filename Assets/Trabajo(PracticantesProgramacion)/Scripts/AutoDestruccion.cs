using Unity.VisualScripting;
using UnityEngine;

public class AutoDestruccion : MonoBehaviour
{
    private Transform PuntoFinal;
    private Animator anim;
    private float vel = 5f;
    private bool tocado= false;
    public bool muerte = false;
    void Awake()
    {
        PuntoFinal = GameObject.FindGameObjectWithTag("PuntoFinal").transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector3.left * vel * Time.deltaTime);
        anim.SetBool("Tocado", tocado);
        if (Vector2.Distance(transform.position, PuntoFinal.position) > 20 || muerte)
        {
            Destroy(gameObject);
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
}
