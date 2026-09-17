using UnityEngine;

public class GenerarEstrellas : MonoBehaviour
{
    [Header("Configuración del generador de Estrellas")]
    [Header("")]
    [SerializeField] private GameObject estrella;
    [SerializeField] private Transform Estrellas;
    [SerializeField] private int cantidad;
    [SerializeField] private float separacionVertical;

    private void Start()
    {
        for (int i = 0; i < cantidad; i++)
        {
            Vector3 posicion = Estrellas.position + Vector3.down * (i * separacionVertical);

            GameObject nuevaEstrella = Instantiate(estrella, posicion, Quaternion.identity, Estrellas);
        }
    }
}
