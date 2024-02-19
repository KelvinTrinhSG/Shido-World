using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManagerDino : MonoBehaviour
{
    public static GameManagerDino Instance { get; private set; }
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI scoreText;

    public Button retryButton;
    private PlayerDino player;
    private SpawnerDino spawner;
    private float score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerDino>();
        spawner = FindObjectOfType<SpawnerDino>();
        NewGame();
    }
    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        // UpdateHiScore();
    }
    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        // UpdateHiScore();
    }
    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    // private void UpdateHiScore()
    // {
    //     float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
    //     if (score > hiscore)
    //     {
    //         hiscore = score;
    //         PlayerPrefs.SetFloat("hiscore", hiscore);
    //     }
    //     hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    // }
}
