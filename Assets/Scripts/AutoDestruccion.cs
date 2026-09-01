using UnityEngine;

public class AutoDestruccion : MonoBehaviour
{
    private Transform PuntoFinal;
    void Start()
    {
        PuntoFinal = GameObject.FindGameObjectWithTag("PuntoFinal").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, PuntoFinal.position) > 25)
        {
            Destroy(gameObject);
        }
    }
}
