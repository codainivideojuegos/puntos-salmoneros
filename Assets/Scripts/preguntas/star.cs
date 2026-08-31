using UnityEngine;

public class star : MonoBehaviour
{
    public Test2 mange;

    void OnTriggerEnter2D(Collider2D other)
    {
        mange.NuevaPregunta();
        Destroy(gameObject);
    }
}
