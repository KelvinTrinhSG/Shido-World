using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Thirdweb;


public class GameManager : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public int score;
    public GameObject ClaimNFTPanel;
    public GameObject ClaimButton;
    public const string ContractAddress = "0x813E5E41EDBC4CDFB4D539FE62dF9d4977eaFd3a";
    private Contract contract;
    public Text claimBtnTxt;
    public Text btnTxt;
    public GameObject ToMainGameButton;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameOver.SetActive(false);
        Pause();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOver.SetActive(false);
        playButton.SetActive(false);
        ClaimNFTPanel.SetActive(false);
        ClaimButton.SetActive(false);
        ToMainGameButton.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        ClaimNFTPanel.SetActive(false);
        ClaimButton.SetActive(false);
        ToMainGameButton.SetActive(true);
        if (score >= 1)//100
        {
            claimBtnTxt.text = "Claim Golden Key";
            ClaimNFTPanel.SetActive(true);
            ClaimButton.SetActive(true);
        }
        Pause();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void BackToMainGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public async void GetNFTBalance()
    {
        if (score >= 1)//100
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
            if (score >= 1)//100
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
