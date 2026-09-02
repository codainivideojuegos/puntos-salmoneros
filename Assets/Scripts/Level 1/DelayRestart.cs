using UnityEngine;

public class DelayRestart : MonoBehaviour
{
    [SerializeField] private GameObject restartGameGO;
    [SerializeField] private float delay;

    private void OnEnable()
    {
        Invoke(nameof(EnableRestart), delay);
    }

    private void EnableRestart()
    {
        restartGameGO.SetActive(true);
    }
}