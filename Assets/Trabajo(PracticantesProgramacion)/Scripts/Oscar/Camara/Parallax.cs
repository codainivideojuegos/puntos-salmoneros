using UnityEngine;

namespace Mundo
{
    [DefaultExecutionOrder(1000)]
    public class Parallax : MonoBehaviour
    {
        [System.Serializable]
        public class CapaParallax
        {
            public Renderer renderer;
            [Range(0f, 1f)] public float velocidad;
        }

        [SerializeField] private CapaParallax[] capas;

        private Transform jugador;
        private Transform camara;
        private float posicionAnteriorX;

        private void Awake()
        {
            jugador = GameObject.Find("Salmon").transform;
            camara = Camera.main.transform;

            posicionAnteriorX = camara.position.x;
        }

        private void LateUpdate()
        {
            if (jugador == null)
            {
                transform.position = new Vector3(camara.position.x - 3.74f, transform.position.y, transform.position.z);
                return;
            }                

            if (Mathf.Approximately(camara.position.x, posicionAnteriorX))
                return;

            transform.position = new Vector3(camara.position.x - 3.74f,transform.position.y,transform.position.z);

            foreach (CapaParallax capa in capas)
            {
                if (capa.renderer == null)
                    continue;

                capa.renderer.material.mainTextureOffset = new Vector2(jugador.position.x * capa.velocidad, 0);
            }

            posicionAnteriorX = camara.position.x;
        }
    }
}