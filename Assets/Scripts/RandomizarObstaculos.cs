using System.Collections;
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
        StartCoroutine(GenerarObstaculos(30f));
    }
    IEnumerator GenerarObstaculos(float tiempo)
    {
        for (float i = 0f; i<= tiempo; i += 1f)
        {
            int CantObstaculos = Random.Range(0, 4);
            for (int z = 0; z <= CantObstaculos; z++)
            {
                int RandomObstaculo = Random.Range(0, Obstaculos.Length);
                Vector3 Punto = new Vector3 (PuntoFinal.position.x, Random.Range(-4.25f,4.25f), 0);
                GameObject Obstaculo = Instantiate(Obstaculos[Random.Range(0,4)], Punto, Quaternion.identity);
            }
            yield return new WaitForSecondsRealtime(1f);
        }
    yield return null;
    }
}
