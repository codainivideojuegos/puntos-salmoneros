using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] public float vel = 5.0f;
    [SerializeField] private GameObject[] fondos; 
    [SerializeField] private float Distancia = 18.0f; 
    private void Update()
    {
        for (int i = 0; i < fondos.Length; i++)
        {
            if (fondos[i].transform.position.x > -Distancia)
            {
                Vector3 dir = Vector3.left;
                fondos[i].transform.Translate(dir * (vel/(i+1)) * Time.deltaTime);
            }
            else
            {
                fondos[i].transform.position = new Vector3(0, fondos[i].transform.position.y, 0);
            }
        }
    }
}
