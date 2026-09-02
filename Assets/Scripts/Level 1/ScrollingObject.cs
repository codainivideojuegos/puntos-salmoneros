using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private void Update()
    {
        if (!GameController.Instance.isGameOver)
        {
            transform.Translate(GameController.Instance.scrollSpeed * Time.deltaTime * Vector2.right);
        }
    }
}