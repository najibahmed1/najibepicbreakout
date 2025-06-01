using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;

    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private SpriteRenderer spriteRenderer;

    // Flashing logic
    private static Paddle instance;
    private static Coroutine flashRoutine;

    public static bool controlFrozen = false; // ✅ Freeze toggle during ball control

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;
    }

    private void Start()
    {
        ResetPaddle();
    }

    public void ResetPaddle()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);

        StopFlashing();
        spriteRenderer.color = Color.white;
    }

    private void Update()
    {
        if (controlFrozen) {
            direction = Vector2.zero;
            return;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            direction = Vector2.left;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            direction = Vector2.right;
        } else {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero) {
            rb.AddForce(direction * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;

        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

        ball.velocity = ballDirection * ball.velocity.magnitude;
    }

    public static void StartFlashing()
    {
        if (instance != null && flashRoutine == null)
        {
            flashRoutine = instance.StartCoroutine(instance.FlashEffect());
        }
    }

    public static void StopFlashing()
    {
        if (instance != null && flashRoutine != null)
        {
            instance.StopCoroutine(flashRoutine);
            flashRoutine = null;
            instance.spriteRenderer.color = Color.white;
        }
    }

    private IEnumerator FlashEffect()
    {
        while (true)
        {
            spriteRenderer.color = Color.black;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
