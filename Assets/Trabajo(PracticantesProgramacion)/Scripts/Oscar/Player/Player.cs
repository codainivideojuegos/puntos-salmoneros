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

        private void Awake()
        {
            Inicializar();
        }

        private void FixedUpdate()
        {
            Moviento();
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

        private IEnumerator EsperarAntesDeReiniciar()
        {
            AudioManager.Instance.StopMusic();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            estela.enabled = false;

            yield return new WaitForSeconds(1.2f);

            // Reiniciar la musica
            AudioManager.Instance.changeMusic(0);

            transform.position = posicionaInicial;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY;
            estela.enabled = true;          
            particulas.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            camara.PreviousStateIsValid = false;
        }
    }
}

