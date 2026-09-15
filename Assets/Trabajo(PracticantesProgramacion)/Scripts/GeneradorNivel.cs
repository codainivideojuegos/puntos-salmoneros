using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneradorNivel : MonoBehaviour
{
    [Header("PreFab")]
    [SerializeField] public float tiempo;
    [SerializeField] private GameObject[] Obstaculos;
    [SerializeField] private GameObject Aminochem;
    [SerializeField] private int MaxObstaculos;
    [Header("Spawn")]
    [SerializeField] private Transform PuntoFinal;
    [Header("Espaciado")]
    [SerializeField] private float distanciaMinima = 1.2f;
    [SerializeField] private int intentosMaximos = 10;
    [SerializeField] private MovimientoNivel1 Jugador;
    void Awake()
    {
        Empezar();
    }
    public void Empezar()
    {
        StartCoroutine(GenerarObstaculos(tiempo));
    }

    IEnumerator GenerarObstaculos(float tiempo)
    {
        Jugador.Liberar();
        for (float i = 0f; i <= tiempo; i += 1f)
        {
            int CantObstaculos = Random.Range(0, MaxObstaculos);
            List<float> posicionesUsadas = new List<float>();
            for (int z = 0; z <= CantObstaculos; z++)
            {
                float posY;
                int intentos = 0;
                do
                {
                    posY = Random.Range(-4.5f, 4.5f);
                    intentos++;
                }
                while (EstaMuyCerca(posY, posicionesUsadas) && intentos < intentosMaximos);
                if (intentos >= intentosMaximos && EstaMuyCerca(posY, posicionesUsadas))
                    continue;

                posicionesUsadas.Add(posY);

                int RandomObstaculo = Random.Range(0, Obstaculos.Length);
                Vector3 Punto = new Vector3(PuntoFinal.position.x, posY, 0);
                GameObject Obstaculo = Instantiate(Obstaculos[RandomObstaculo], Punto, Quaternion.identity);
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        StartCoroutine(GenerarAminochem());
        yield return null;
    }
    IEnumerator GenerarAminochem()
    {
        Jugador.VolverAlCentro();
        yield return new WaitForSecondsRealtime(4f);
        Instantiate(Aminochem, PuntoFinal.position, Quaternion.identity);
        yield return null;
    }
    private bool EstaMuyCerca(float posY, List<float> posicionesUsadas)
    {
        foreach (float p in posicionesUsadas)
        {
            if (Mathf.Abs(posY - p) < distanciaMinima)
                return true;
        }
        return false;
    }
}
