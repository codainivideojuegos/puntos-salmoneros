using UnityEngine;

public class Aminochem : MonoBehaviour
{
    public Test2 mange;
    private Animator anim;
    private bool tocado;
    public bool pregunta;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mange = GameObject.FindGameObjectWithTag("Manager").GetComponent<Test2>();
    }
    void Update()
    {
        if (!tocado)
        {
            transform.Translate(Vector3.left * 3f * Time.deltaTime);
        }else if (pregunta)
        {
            mange.NuevaPregunta();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tocado = true;
            anim.SetBool("Tocado",tocado);
        }
    }
}
