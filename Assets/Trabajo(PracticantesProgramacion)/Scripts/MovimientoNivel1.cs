using UnityEngine;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class MovimientoNivel1 : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad;
    [SerializeField] private float y;
    [SerializeField] private float suavizado = 4.0f;
    [SerializeField] private float limitesuperior = 4.25f;
    [SerializeField] private float limiteinferior = -4.25f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] public bool Daño = false;
    [SerializeField] private float TiempoVolver;
    [SerializeField] private bool Moverse;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = 0f;
    }
    void Update()
    {
        y = Input.GetAxisRaw("Vertical");
        anim.SetBool("Daño", Daño);
    }
    private void FixedUpdate()
    {
        if (Moverse)
        {
            float velocidadObjetivo = y * velocidad;
            float velocidady = Mathf.Lerp(rb.linearVelocity.y, velocidadObjetivo, suavizado * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector2 (0, velocidady);
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
    public void VolverAlCentro()
    {
        Moverse = false;
        StartCoroutine(RetornarAlCentro());
    }
    public void Liberar()
    {
        Moverse = true;
    }
    private IEnumerator RetornarAlCentro()
    {
        rb.linearVelocity = Vector2.zero;
        Vector2 inicio = rb.position;
        Vector2 destino = new Vector2(inicio.x, 0);
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < TiempoVolver)
        {
            Vector2 nuevaPos = Vector2.Lerp(inicio, destino, tiempoTranscurrido / TiempoVolver);
            rb.MovePosition(nuevaPos);
            tiempoTranscurrido += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(destino);
    }
}
