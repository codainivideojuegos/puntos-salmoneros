using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float vel = 5.0f;
    [SerializeField] private GameObject[] fondos; 
    private void Update()
    {
        for (int i = 0; i < fondos.Length; i++)
        {
            if (fondos[i].transform.position.x > -18)
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
