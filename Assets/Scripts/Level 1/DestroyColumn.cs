using UnityEngine;

public class DestroyColumn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Column"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
}