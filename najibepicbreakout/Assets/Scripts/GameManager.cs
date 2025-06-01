using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Ball ball;
    private Paddle paddle;
    private Brick[] bricks;

    public GameObject ballPrefab;

    // Ensure these match EXACTLY with your Build Settings order
    private string[] levelScenes = { "Level1", "Level2", "Level3", "Level4", "Level5" };
    private int currentLevelIndex = 0;

    public int lives = 3;
    private Coroutine powerTimer;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSceneReferences();

        Debug.Log($"[GameManager] Loaded: {scene.name}, currentLevelIndex: {currentLevelIndex}");

        // Reset lives only on Level1–4
        if (currentLevelIndex < levelScenes.Length - 1)
        {
            lives = 3;
        }
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }

    public void OnBallMiss()
    {
        Ball.controlEnabled = false;
        Paddle.controlFrozen = false;

        lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            Debug.Log("[GameManager] Ran out of lives. Restarting game.");
            LoadLevel(0); // Always reset to Level1
        }
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    public void OnBrickHit(Brick brick)
    {
        if (Cleared())
        {
            Ball.controlEnabled = false;
            Paddle.controlFrozen = false;

            int nextLevel = currentLevelIndex + 1;

            if (nextLevel >= levelScenes.Length)
            {
                Debug.Log("[GameManager] No more levels. Restarting to Level1.");
                LoadLevel(0); // After Level5 (win screen), go back to Level1
            }
            else
            {
                LoadLevel(nextLevel);
            }
        }
    }

    private bool Cleared()
    {
        foreach (Brick brick in bricks)
        {
            if (brick.gameObject.activeInHierarchy && !brick.unbreakable)
                return false;
        }
        return true;
    }

    public void LoadLevel(int index)
    {
        if (index >= 0 && index < levelScenes.Length)
        {
            currentLevelIndex = index;
            Debug.Log($"[GameManager] Loading level: {levelScenes[index]} (index {index})");
            SceneManager.LoadScene(levelScenes[index]);
        }
        else
        {
            Debug.LogWarning("[GameManager] Invalid level index! Resetting to Level1.");
            currentLevelIndex = 0;
            SceneManager.LoadScene(levelScenes[0]);
        }
    }

    // 🔴 Power-Up: One-Hit Mode
    public void ActivateOneHitMode()
    {
        if (powerTimer != null)
            StopCoroutine(powerTimer);

        powerTimer = StartCoroutine(OneHitModeRoutine());
    }

    private IEnumerator OneHitModeRoutine()
    {
        Brick.oneHitMode = true;
        Paddle.StartFlashing();

        yield return new WaitForSeconds(15f);

        Brick.oneHitMode = false;
        Paddle.StopFlashing();
        powerTimer = null;
    }

    // 🟣 Power-Up: Chaos Balls
    public void SpawnChaosBalls()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject ballGO = Instantiate(ballPrefab, ball.transform.position, Quaternion.identity);
            Ball newBall = ballGO.GetComponent<Ball>();
            newBall.isChaosBall = true;
            newBall.Launch();

            Destroy(ballGO, 5f);
        }
    }

    // 🩷 Power-Up: Ball Control Mode
    public void EnableBallControl()
    {
        Ball.controlEnabled = true;
        Paddle.controlFrozen = true;
    }
}