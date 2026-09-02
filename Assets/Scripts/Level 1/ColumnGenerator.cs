using UnityEngine;

public class ColumnGenerator : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    [SerializeField] private int columnPoolSize;
    [SerializeField] private float columnMin;
    [SerializeField] private float columnMax;
    [SerializeField] private float spawnRate;

    private GameObject[] columns = null;
    private Vector2 objectPoolPosition = Vector2.zero;
    private int currentColumn = 0;
    private float timeSinceLastSpawned = 0f;

    private void Start()
    {
        objectPoolPosition = new Vector2(-11.5f, 0f);
        columns = new GameObject[columnPoolSize];

        for (int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }

        timeSinceLastSpawned = spawnRate;
    }

    private void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (!GameController.Instance.isGameOver && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            SpawnColumn();
        }
    }

    private void SpawnColumn()
    {
        float spawnYPosition = Random.Range(columnMin, columnMax);

        columns[currentColumn].transform.position = new Vector2(transform.position.x, spawnYPosition);
        currentColumn++;

        if (currentColumn >= columnPoolSize)
        {
            currentColumn = 0;
        }
    }
}