using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
namespace Player2
{
    [RequireComponent(typeof(Rigidbody2D))]
    //[RequireComponent(typeof(BoxCollider2D))]
    public class Player : MonoBehaviour
    {
        public enum TipoVuelo { Ship, Wave }
        public enum Estado { Moviendo, Muerto, Gano }

        [Header("Modo")]
        [SerializeField] private TipoVuelo tipo = TipoVuelo.Ship;

        [Header("Movimiento horizontal")]
        [SerializeField] private float velocidadHorizontal = 8f;

        [Header("Ship")]
        [SerializeField] private float gravedad = 18f;
        [SerializeField] private float fuerzaSubida = 30f;

        [Header("Wave")]
        [SerializeField] private float velocidadWave = 10f;

        [Header("Rotación")]
        [SerializeField] private float rotacionMaxima = 30f;
        [SerializeField] private float velocidadRotacion = 8f;

        private Rigidbody2D rb;
        private CinemachineCamera camara;
        private PlayeraMovimiento playerMovimiento;
        private Vector3 posicionaInicial;
        private TrailRenderer estela;
        private ParticleSystem particulas;
        private int vida = 3;
        [HideInInspector] public Estado estado = Estado.Moviendo;

        private void Awake()
        {
            Inicializar();
        }

        private void FixedUpdate()
        {
            switch (estado)
            {
                case Estado.Moviendo:
                    Moviento();
                    break;
                case Estado.Muerto:
                    rb.linearVelocity = Vector2.zero;
                    break;
                case Estado.Gano:
                    rb.linearVelocity = Vector2.zero;
                    break;
            }
        }

        private void Inicializar()
        {
            rb = GetComponent<Rigidbody2D>();
            playerMovimiento = new PlayeraMovimiento(rb, velocidadHorizontal, gravedad, fuerzaSubida, rotacionMaxima, velocidadRotacion);
            posicionaInicial = transform.position;
            camara = FindAnyObjectByType<CinemachineCamera>();
            estela = GetComponentInChildren<TrailRenderer>();
            particulas = GetComponentInChildren<ParticleSystem>();
        }

        private void Moviento()
        {
            LimitesY();

            if (tipo == TipoVuelo.Ship)
                playerMovimiento.MoverShip();

            else
                playerMovimiento.MoverWave(velocidadWave);

            playerMovimiento.Rotar(tipo);
        }

        public void RecibirDano(int dano)
        {
            vida -= dano;

            UIManager.Instance.ActualizarVida(vida);

            Murio_O_Gano(estado = Estado.Muerto);
        }

        private void LimitesY()
        {
            float limiteSuperior = 4.58f;
            float limiteInferior = -4.58f;

            if (rb.position.y > limiteSuperior)
            {
                rb.position = new Vector2(rb.position.x, limiteSuperior);
                rb.linearVelocity = Vector2.zero;
            }
            else if (rb.position.y < limiteInferior)
            {
                rb.position = new Vector2(rb.position.x, limiteInferior);
                rb.linearVelocity = Vector2.zero;
            }
        }

        public void Murio_O_Gano(Estado estado)
        {
            if (estado == Estado.Gano)
            {
                Destroy(gameObject);
            }
            else if (estado == Estado.Muerto)
            {
                AudioManager.Instance.changeSFX("Muerte");

                if (vida <= 0)
                {
                    UIManager.Instance.StartCoroutine(UIManager.Instance.HaciendoFade("Player"));
                    Destroy(gameObject);
                    return;
                }
                else if (vida > 0)
                {
                    StartCoroutine(EsperarAntesDeReiniciar());
                }
            }            
        }

        private IEnumerator EsperarAntesDeReiniciar()
        {
            AudioManager.Instance.StopMusic();
            particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            estela.enabled = false;

            yield return new WaitForSeconds(1.2f);

            AudioManager.Instance.changeMusic(0);
            transform.position = posicionaInicial;
            estela.enabled = true;
            particulas.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            camara.PreviousStateIsValid = false;
            estado = Estado.Moviendo;
        }
    }
}

