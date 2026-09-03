using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float spawnRate;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private float timer;

    private void Update()
    {
        if (!GameController.Instance.isGameOver)
        {
            timer += Time.deltaTime;

            if (timer >= spawnRate)
            {
                timer = 0f;

                SpawnBubble();
            }
        }
    }

    private void SpawnBubble()
    {
        var position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Instantiate(bubblePrefab, position, Quaternion.identity);
    }
}