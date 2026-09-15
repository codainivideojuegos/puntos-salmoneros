using Player2;
using UnityEngine;

public class Ostaculo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.RecibirDano(1);
        }
    }
}
