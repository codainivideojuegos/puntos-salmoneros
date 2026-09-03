using UnityEngine;

public class MovimientoNivel3 : MonoBehaviour
{
    [SerializeField] private float velocidad, y;
    [SerializeField] private float suavizado = 4.0f;
    [SerializeField] private float limitesuperior = 4.25f;
    [SerializeField] private float limiteinferior = -4.25f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator anim;
    [SerializeField] public bool Daño = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = 0f;
    }
    void Update()
    {
        y = Input.GetAxisRaw("Vertical");
        cam.position = new Vector3(cam.position.x, .0f, cam.position.z);
        anim.SetBool("Daño", Daño);
    }
    private void FixedUpdate()
    {
        float velocidadObjetivo = y * velocidad;
        float velocidady = Mathf.Lerp(rb.linearVelocity.y, velocidadObjetivo, suavizado * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(0, velocidady);
        if (rb.position.y > limitesuperior)
        {
            rb.position = new Vector2(rb.position.x, limitesuperior);
            rb.linearVelocity = Vector2.zero;
        }
        else if (rb.position.y < limiteinferior)
        {
            rb.position = new Vector2(rb.position.x, limiteinferior);
            rb.linearVelocity = Vector2.zero;
        }
    }
}
