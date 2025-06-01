using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Sprite[] states = new Sprite[0];
    public int points = 100;
    public bool unbreakable;
    public bool isPowerBrick = false;
    public bool isChaosBrick = false;
    public bool isControlBrick = false;

    public static bool oneHitMode = false;

    private SpriteRenderer spriteRenderer;
    private int health;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);

        if (!unbreakable)
        {
            health = states.Length;
            if (states.Length > 0)
            {
                spriteRenderer.sprite = states[health - 1];
            }
        }
    }

    private void Hit()
    {
        if (unbreakable) return;

        // 🟪 Control brick: grant player ball steering
        if (isControlBrick)
        {
            gameObject.SetActive(false);
            GameManager.Instance.OnBrickHit(this);
            GameManager.Instance.EnableBallControl();
            return;
        }

        // 🔴 Power brick: one-hit mode
        if (isPowerBrick)
        {
            gameObject.SetActive(false);
            GameManager.Instance.OnBrickHit(this);
            GameManager.Instance.ActivateOneHitMode();
            return;
        }

        // 🟣 Chaos brick: spawn 20 balls
        if (isChaosBrick)
        {
            gameObject.SetActive(false);
            GameManager.Instance.OnBrickHit(this);
            GameManager.Instance.SpawnChaosBalls();
            return;
        }

        // 💥 One-hit mode enabled: destroy instantly
        if (oneHitMode)
        {
            gameObject.SetActive(false);
            GameManager.Instance.OnBrickHit(this);
            return;
        }

        // 🧱 Normal brick logic
        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = states[health - 1];
        }

        GameManager.Instance.OnBrickHit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null && ball.isChaosBall)
        {
            if (unbreakable)
            {
                // Bounce only, don't break
                return;
            }
            else
            {
                // Chaos ball destroys breakable bricks instantly
                gameObject.SetActive(false);
                GameManager.Instance.OnBrickHit(this);
                return;
            }
        }

        // Regular ball logic
        Hit();
    }
}