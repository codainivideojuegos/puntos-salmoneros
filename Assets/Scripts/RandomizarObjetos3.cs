using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizarObjetos : MonoBehaviour
{
    [Header("PreFab")]
    [SerializeField] private GameObject[] Obstaculos;
    [SerializeField] private GameObject Aminochem;
    [SerializeField] private int MaxObstaculos;
    [Header("Transform")]
    [SerializeField] private Transform PuntoFinal;
    [Header("Espaciado")]
    [SerializeField] private float distanciaMinima = 1.2f;
    [SerializeField] private int intentosMaximos = 10;
    void Awake()
    {
        StartCoroutine(GenerarObstaculos(30f));
    }
    IEnumerator GenerarObstaculos(float tiempo)
    {
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
                    posY = Random.Range(-2.125f, 2.125f);
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
