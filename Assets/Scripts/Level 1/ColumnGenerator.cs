using UnityEngine;

public class ColumnGenerator : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    [SerializeField] private int columnPoolSize;
    [SerializeField] private float spawnRate;
    [SerializeField] private float columnMin;
    [SerializeField] private float columnMax;

    private GameObject[] columns = null;
    private Vector2 objectPoolPosition = Vector2.zero;
    private int currentColumn = 0;
    private float timeSinceLastSpawned = 0f;

    private void Start()
    {
        objectPoolPosition = new Vector2(-13.5f, 0f);
        columns = new GameObject[columnPoolSize];

        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }

        timeSinceLastSpawned = spawnRate;
    }

    private void Update()
    {
        if (GameController.Instance.isGameStarted)
        {
            timeSinceLastSpawned += Time.deltaTime;

            if (!GameController.Instance.isGameOver && timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0f;

                SpawnColumn();
            }
        }
    }

    private void SpawnColumn()
    {
        if (currentColumn >= columnPoolSize)
        {
            return;
        }

        float spawnYPosition = Random.Range(columnMin, columnMax);

        columns[currentColumn].transform.position = new Vector2(transform.position.x, spawnYPosition);
        currentColumn++;

        // if (currentColumn >= columnPoolSize)
        // {
        //     currentColumn = 0;
        // }
    }
}