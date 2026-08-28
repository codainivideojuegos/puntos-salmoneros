using UnityEngine;

public class MovimientoNivel1 : MonoBehaviour
{
    public float velocidad;
    public float suavizado = 4.0f;
    public float y;
    public Rigidbody2D rb;
    public float limitesuperior = 4f;
    public float limiteinferior = -4f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    void Update()
    {
        y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        float velocidadObjetivo = y * velocidad;
        float velocidady = Mathf.Lerp(rb.linearVelocity.y, velocidadObjetivo, suavizado * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2 (0f, velocidady);
        if(rb.position.y > limitesuperior)
        {
            rb.position = new Vector2(rb.position.x, limitesuperior);
            rb.linearVelocity = Vector2.zero;
        }
        else if(rb.position.y < limiteinferior)
        {
            rb.position = new Vector2(rb.position.x, limiteinferior);
            rb.linearVelocity = Vector2.zero;
        }
    }
}
