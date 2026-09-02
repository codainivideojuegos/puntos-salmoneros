using UnityEngine;

public class AutoDestruccion : MonoBehaviour
{
    private Transform PuntoFinal;
    private Vector3 dir = Vector3.left;
    private float vel = 5f;
    void Start()
    {
        PuntoFinal = GameObject.FindGameObjectWithTag("PuntoFinal").transform;
    }
    void Update()
    {
        transform.Translate(dir * vel * Time.deltaTime);
        if (Vector2.Distance(transform.position, PuntoFinal.position) > 20)
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
            Destroy(gameObject);
        }
    }
}
