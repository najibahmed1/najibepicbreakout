using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private AudioSource audioSource;

    public float speed = 10f;
    public bool isChaosBall = false;

    public static bool controlEnabled = false;

    public AudioClip popSound; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (!isChaosBall)
        {
            ResetBall();
        }
    }

    public void ResetBall()
    {
        isChaosBall = false;
        controlEnabled = false;

        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        CancelInvoke();
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = new Vector2(Random.Range(-1f, 1f), -1f);
        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (controlEnabled)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            rb.velocity += new Vector2(moveX, moveY) * 0.5f;
        }

        rb.velocity = rb.velocity.normalized * speed;

        if (isChaosBall && rb.velocity.y <= 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Abs(rb.velocity.y) + 0.1f);
        }
    }

    public void Launch()
    {
        isChaosBall = true;
        rb.velocity = Vector2.zero;

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ✅ Play bounce pop sound
        if (audioSource != null && popSound != null)
        {
            audioSource.PlayOneShot(popSound);
        }

        if (isChaosBall && rb.velocity.y < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Abs(rb.velocity.y) + 0.5f);
        }
    }
}