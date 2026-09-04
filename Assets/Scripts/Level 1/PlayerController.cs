using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxAngle;
    [SerializeField] private float maxAngleVelocity;
    [SerializeField] private bool hasJumped;
    [SerializeField] private bool isStopped;
    [SerializeField] private bool isDead;

    private Rigidbody2D rb2D = null;
    private Animator atr = null;
    private readonly int HasJumpedHash = Animator.StringToHash("HasJumped");
    private readonly int IsDeadHash = Animator.StringToHash("IsDead");

    public bool IsDead => isDead;
    public bool IsStopped => isStopped;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        atr = GetComponent<Animator>();
    }

    private void Start()
    {
        Stop();
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
        if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Border"))
            && !isDead && !isStopped && GameController.Instance.isGameStarted)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Column") && !isDead && !isStopped && GameController.Instance.isGameStarted)
        {
            Die();
        }
    }

    private void HandleJumpInput()
    {
        if (Input.GetMouseButtonDown(0) && !isDead && !isStopped && GameController.Instance.isGameStarted)
        {
            hasJumped = true;
            atr.SetTrigger(HasJumpedHash);

            Debug.Log("Ha saltado.");
        }
    }

    private void HandleJump()
    {
        if (hasJumped && !isDead && !isStopped && GameController.Instance.isGameStarted)
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

    public void ResetPlayer()
    {
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        rb2D.linearVelocity = Vector2.zero;
        rb2D.rotation = 0f;
        hasJumped = false;
        isStopped = false;
        isDead = false;
    }

    public void Stop()
    {
        rb2D.linearVelocity = Vector2.zero;
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        isStopped = true;

        if (GameController.Instance.isGameStarted)
        {
            StartCoroutine(MoveToCenter());
        }
    }

    private IEnumerator MoveToCenter()
    {
        float speed = 3f;
        var target = new Vector2(-2f, 0f);

        while (Vector2.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

            yield return null;
        }

        transform.position = target;

        yield return new WaitForSeconds(1f);

        GameController.Instance.question.ActivateQuestion();
        GameController.Instance.shadowScoreText.enabled = false;
        GameController.Instance.scoreText.enabled = false;
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