using UnityEngine;
using static Player2.Player;

namespace Player2
{
    public class PlayeraMovimiento
    {
        private Rigidbody2D rb;
        private Transform transform;

        private float velocidadHorizontal;

        private float gravedad;
        private float fuerzaSubida;

        private float rotacionMaxima;
        private float velocidadRotacion;

        public PlayeraMovimiento(Rigidbody2D rb, float velocidadHorizontal, float gravedad, float fuerzaSubida, float rotacionMaxima, float velocidadRotacion)
        {
            this.rb = rb;
            this.velocidadHorizontal = velocidadHorizontal;
            this.gravedad = gravedad;
            this.fuerzaSubida = fuerzaSubida;
            this.rotacionMaxima = rotacionMaxima;
            this.velocidadRotacion = velocidadRotacion;
        }
        public void MoverShip()
        {
            float velocidadY = rb.linearVelocity.y;

            if (Input.GetKey(KeyCode.Space))
                velocidadY += fuerzaSubida * Time.fixedDeltaTime;
            else
                velocidadY -= gravedad * Time.fixedDeltaTime;

            velocidadY = Mathf.Clamp(velocidadY, -velocidadHorizontal, velocidadHorizontal);

            rb.linearVelocity = new Vector2(velocidadHorizontal, velocidadY);
        }

        public void MoverWave(float velocidadWave)
        {
            float direccion = Input.GetKey(KeyCode.Space) ? 1f : -1f;

            rb.linearVelocity = new Vector2(velocidadHorizontal, direccion * velocidadWave);
        }

        public void Rotar(Player.TipoVuelo tipo)
        {
            float porcentaje = Mathf.Clamp(rb.linearVelocity.y / velocidadHorizontal, -1f, 1f);

            float rotacionObjetivo = porcentaje * rotacionMaxima;

            Quaternion objetivo = Quaternion.Euler(0f, 0f, rotacionObjetivo);

            if (tipo == TipoVuelo.Ship)
                rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, objetivo, velocidadRotacion * Time.fixedDeltaTime);
            else
                rb.transform.rotation = objetivo;
        }
    }
}

