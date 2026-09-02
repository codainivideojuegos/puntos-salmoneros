using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RepeatingBG : MonoBehaviour
{
    private BoxCollider2D bcol2D = null;
    private float horizontalLengthBG = 0f;

    private void Awake()
    {
        bcol2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        horizontalLengthBG = bcol2D.bounds.size.x;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < -horizontalLengthBG)
        {
            RepositionBG();
        }
    }

    private void RepositionBG()
    {
        transform.Translate(2f * horizontalLengthBG * Vector2.right);
    }
}