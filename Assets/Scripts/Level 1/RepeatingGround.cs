using UnityEngine;

public class RepeatingGround : MonoBehaviour
{
    private CompositeCollider2D compositeCollider2D = null;
    private float horizontalLengthBG = 0f;

    private void Awake()
    {
        compositeCollider2D = GetComponent<CompositeCollider2D>();
    }

    private void Start()
    {
        horizontalLengthBG = compositeCollider2D.bounds.size.x;
    }

    private void Update()
    {
        if (compositeCollider2D.bounds.min.x < -horizontalLengthBG / 2f)
        {
            RepositionGround();
        }
    }

    private void RepositionGround()
    {
        transform.position += Vector3.right * horizontalLengthBG;
    }
}