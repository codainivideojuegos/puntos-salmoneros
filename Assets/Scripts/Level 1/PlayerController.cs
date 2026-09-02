using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxAngle;
    [SerializeField] private float maxAngleVelocity;
    [SerializeField] private bool hasJumped;
    [SerializeField] private bool isDead;

    private Rigidbody2D rb2D = null;
    private Animator atr = null;
    private readonly int HasJumpedHash = Animator.StringToHash("HasJumped");
    private readonly int IsDeadHash = Animator.StringToHash("IsDead");

    public bool IsDead => isDead;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        atr = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        HandleJump();
        UpdateRotation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isDead)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Column") && !isDead)
        {
            Die();
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            hasJumped = true;
            atr.SetTrigger(HasJumpedHash);

            Debug.Log("Ha saltado.");
        }
    }

    private void HandleJump()
    {
        if (hasJumped && !isDead)
        {
            rb2D.linearVelocity = Vector2.zero;
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            AudioManager.Instance.PlaySFX(AudioManager.SFX.Jump);

            hasJumped = false;
        }
    }

    private void UpdateRotation()
    {
        if (!isDead)
        {
            float currentAngleVelocity = Mathf.Clamp01(-rb2D.linearVelocity.y / maxAngleVelocity);
            float angle = Mathf.Lerp(0f, maxAngle, currentAngleVelocity);

            rb2D.rotation = angle;
        }
    }

    public void Die()
    {
        rb2D.linearVelocity = Vector2.zero;
        rb2D.rotation = maxAngle;
        hasJumped = false;
        isDead = true;
        atr.SetTrigger(IsDeadHash);

        GameController.Instance.GameOver();
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlaySFX(AudioManager.SFX.Hit);
        AudioManager.Instance.PlaySFX(AudioManager.SFX.GameOver);
    }
}