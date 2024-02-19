using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Thirdweb;
using UnityEngine.SceneManagement;

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

    public GameObject ClaimNFTPanel;
    public GameObject ClaimButton;
    public const string ContractAddress = "0x4c953C8F4FFBA000f6be507c3bA46935B16D2C79";
    private Contract contract;
    public Text claimBtnTxt;
    public Text btnTxt;
    public GameObject ToMainGameButton;

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
        ClaimNFTPanel.SetActive(false);
        ClaimButton.SetActive(false);
        ToMainGameButton.SetActive(false);
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
        ToMainGameButton.SetActive(true);
        if (score >= 100)//10000
        {
            claimBtnTxt.text = "Claim Golden Scroll";
            ClaimNFTPanel.SetActive(true);
            ClaimButton.SetActive(true);
        }
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

    public void BackToMainGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public async void GetNFTBalance()
    {
        if (score >= 100)//10000
        {
            btnTxt.text = "Getting balance...";
            contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress);
            var results = await contract.ERC721.Balance();
            btnTxt.text = results;
        }
    }

    public async void ClaimNFT()
    {
        try
        {
            if (score >= 100)//10000
            {
                btnTxt.text = "Claiming NFT...";
                contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress);
                var results = await contract.ERC721.Claim(1);
                btnTxt.text = "NFT Claimed!";
                ClaimButton.SetActive(false);
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Error claiming NFT");
        }
    }
}
