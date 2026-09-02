using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.Instance.AddScore();
        }
    }
}