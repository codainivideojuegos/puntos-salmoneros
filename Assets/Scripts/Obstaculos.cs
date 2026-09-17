using UnityEngine;

public class Obstaculos : MonoBehaviour
{
    private Transform PuntoFinal;
    private Vector3 dir = Vector3.left;
    [SerializeField]private float vel = 5f;
    void Start()
    {
        PuntoFinal = GameObject.FindGameObjectWithTag("PuntoFinal").transform;
    }
    void Update()
    {
        transform.Translate(dir * vel * Time.deltaTime);
        if (Vector2.Distance(transform.position, PuntoFinal.position) > 20)
        {
            Muerte();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MovimientoNivel3 MN = collision.GetComponent<MovimientoNivel3>();
            MN.Daño = true;
        }
    }
    public void Muerte()
    {
        Destroy(gameObject);
    }
}
