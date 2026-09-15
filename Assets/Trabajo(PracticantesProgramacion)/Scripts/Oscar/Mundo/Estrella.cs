using Player2;
using UnityEngine;

namespace Estrella
{
    public class Estrella : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                AudioManager.Instance.changeSFX("Estrella");
                PreguntasManager.Instance.MostrarPanelPreguntas();
                collision.gameObject.GetComponent<Player>().Murio_O_Gano(Player.Estado.Gano);
            }
        }
    }
}
