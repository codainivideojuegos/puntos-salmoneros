using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    private void Update()
    {
        if (!GameController.Instance.isGameOver)
        {
            transform.Translate(scrollSpeed * Time.deltaTime * Vector2.right);
        }
    }
}