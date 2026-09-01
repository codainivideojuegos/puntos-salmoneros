using UnityEngine;

public class RandomizarObstaculos : MonoBehaviour
{
    [Header("PreFab")]
    [SerializeField] private GameObject[] Obstaculos;
    [SerializeField] private GameObject Aminochem;
    [Header("Transform")]
    [SerializeField] private float DistanciaMinima;
    [SerializeField] private Transform PuntoFinal;
    [SerializeField] private Transform jugador;
    void Awake()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        GenerarObstaculos();
    }

    void Update()
    {
        if (Vector2.Distance(jugador.position, PuntoFinal.position) < DistanciaMinima)
        {
            Debug.Log(Vector2.Distance(jugador.position, PuntoFinal.position));
            GenerarObstaculos();
        }
    }
    private void GenerarObstaculos()
    {
        int CantObstaculos = Random.Range(0, 4);
        for (int i = 0; i <= CantObstaculos; i++)
        {
            int RandomObstaculo = Random.Range(0, Obstaculos.Length);
            Vector3 Punto = new Vector3 (PuntoFinal.position.x, Random.Range(-4.25f,4.25f), 0);
            GameObject Obstaculo = Instantiate(Obstaculos[Random.Range(0,4)], Punto, Quaternion.identity);
        }
        PuntoFinal.position = new Vector3(PuntoFinal.position.x+4,0,0);
    }
}
