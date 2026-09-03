using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        if (!GameController.Instance.isGameOver)
        {
            transform.Translate(speed * Time.deltaTime * new Vector2(-1f, 1f).normalized);
        }
        else
        {
            DestroyBubble();
        }
    }

    public void DestroyBubble()
    {
        Destroy(gameObject);
    }
}