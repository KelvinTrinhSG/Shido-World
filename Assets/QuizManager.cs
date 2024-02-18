using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Thirdweb;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject QuizPanel;
    public GameObject GOPanel;
    public GameObject ClaimNFTPanel;
    public Text QuestionTxt;
    public Text ScoreTxt;
    int TotalQuestions = 0;
    public int score;
    public const string ContractAddress = "0x14512A2eB546C811DC260Af09897434823e489Ee";
    private Contract contract;
    public Text btnTxt;
    public Text claimBtnTxt;
    public GameObject ClaimButton;

    private void Start()
    {
        TotalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        generateQuestion();
    }

    void GameOver()
    {
        QuizPanel.SetActive(false);
        GOPanel.SetActive(true);
        ClaimNFTPanel.SetActive(false);
        ScoreTxt.text = score + "/" + TotalQuestions;
        if (score >= 10)
        {
            claimBtnTxt.text = "Claim Silver Key";
            ClaimNFTPanel.SetActive(true);
        }
    }

    public void correct()
    {
        score += 1;
        generateQuestion();
    }

    public void wrong()
    {
        generateQuestion();
    }
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswersScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswersScript>().isCorrect = true;
            }
        }
    }
    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Questions;
            SetAnswers();
            QnA.RemoveAt(currentQuestion);
        }
        else
        {
            Debug.Log("Out of Question");
            GameOver();
        }
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public async void GetNFTBalance()
    {
        if (score >= 10)
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
            if (score >= 10)
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

    public void BackToMainGame()
    {
        SceneManager.LoadSceneAsync(0);
    }


}
