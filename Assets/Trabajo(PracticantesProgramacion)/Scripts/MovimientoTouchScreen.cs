using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MovimientoTouchScreen : MonoBehaviour
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
    [SerializeField] private float TiempoVolver = 4f;
    [SerializeField] private bool Moverse;

    [Header("Touch")]
    [SerializeField] private Camera cam;
    [SerializeField] private float sensibilidadTouch = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = 0f;
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        y = ObtenerInputVertical();
        anim.SetBool("Daño", Daño);
    }

    private float ObtenerInputVertical()
    {
        // Touch real
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 posPantalla = Touchscreen.current.primaryTouch.position.ReadValue();
            return CalcularYDesdeToque(posPantalla);
        }
        return 0f;
    }

    private float CalcularYDesdeToque(Vector2 posicionPantalla)
    {
        Vector3 posicionMundo = cam.ScreenToWorldPoint(new Vector3(posicionPantalla.x, posicionPantalla.y, cam.nearClipPlane));
        float diferencia = posicionMundo.y - rb.position.y;
        return Mathf.Clamp(diferencia * sensibilidadTouch, -1f, 1f);
    }

    private void FixedUpdate()
    {
        if (Moverse)
        {
            float velocidadObjetivo = y * velocidad;
            float velocidady = Mathf.Lerp(rb.linearVelocity.y, velocidadObjetivo, suavizado * Time.fixedDeltaTime);
            float anguloObjetivo = Mathf.Clamp(velocidadObjetivo * 5f, -30f, 30f);
            float nuevoAngulo = Mathf.LerpAngle(rb.rotation, anguloObjetivo, 5f * Time.fixedDeltaTime);
            rb.MoveRotation(nuevoAngulo);
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
        Vector2 inicioPos = rb.position;
        Vector2 destinoPos = new Vector2(inicioPos.x, 0);
        float inicioAng = rb.rotation;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < TiempoVolver)
        {
            float t = tiempoTranscurrido / TiempoVolver;

            Vector2 nuevaPos = Vector2.Lerp(inicioPos, destinoPos, t);
            rb.MovePosition(nuevaPos);

            float nuevoAngulo = Mathf.LerpAngle(inicioAng, 0, t);
            rb.MoveRotation(nuevoAngulo);

            tiempoTranscurrido += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        rb.MovePosition(destinoPos);
        rb.MoveRotation(0f);
    }
}
