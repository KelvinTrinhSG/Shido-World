using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Thirdweb;

public class IntroductionManager : MonoBehaviour
{
    public GameObject firstScene;
    public GameObject quizScene;
    public GameObject flappyBirdScene;
    public GameObject bossFightingScene;

    public GameObject firstSceneNextButton;
    public GameObject quizSceneNextButton;
    public GameObject quizSceneBackButton;
    public GameObject flappyBirdSceneNextButton;
    public GameObject flappyBirdSceneBackButton;
    public GameObject bossFightingSceneNextButton;
    public GameObject bossFightingSceneBackButton;
    public const string ContractAddress = "0xE48DED2552efCD461CeE086Ba3434b7eE95D87ab";
    private Contract contract;
    public const string ContractAddressBossFighting = "0x4c953C8F4FFBA000f6be507c3bA46935B16D2C79";
    private Contract contractBossFighting;

    public void MoveToQuizScene()
    {
        firstScene.SetActive(false);
        quizScene.SetActive(true);
        flappyBirdScene.SetActive(false);
        bossFightingScene.SetActive(false);
    }
    public void MoveToFlappyBirdScene()
    {
        firstScene.SetActive(false);
        quizScene.SetActive(false);
        flappyBirdScene.SetActive(true);
        bossFightingScene.SetActive(false);
    }
    public void MoveToBossFightingScene()
    {
        firstScene.SetActive(false);
        quizScene.SetActive(false);
        flappyBirdScene.SetActive(false);
        bossFightingScene.SetActive(true);

    }
    public void MoveBackToFirstScene()
    {
        firstScene.SetActive(true);
        quizScene.SetActive(false);
        flappyBirdScene.SetActive(false);
        bossFightingScene.SetActive(false);
    }
    public void MoveBackToQuizScene()
    {
        firstScene.SetActive(false);
        quizScene.SetActive(true);
        flappyBirdScene.SetActive(false);
        bossFightingScene.SetActive(false);
    }
    public void MoveBackToflappyBirdScene()
    {
        firstScene.SetActive(false);
        quizScene.SetActive(false);
        flappyBirdScene.SetActive(true);
        bossFightingScene.SetActive(false);
    }
    public void QuizGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // public void FlappyBirdGame()
    // {
    //     SceneManager.LoadSceneAsync(2);
    // }
    public async void FlappyBirdGame()
    {
        contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress);
        var results = await contract.ERC721.Balance();
        if (int.Parse(results) >= 1)
        {
            SceneManager.LoadSceneAsync(2);
        }
    }
    // public void BossFightingGame()
    // {
    //     SceneManager.LoadSceneAsync(3);
    // }
    public async void BossFightingGame()
    {
        contractBossFighting = ThirdwebManager.Instance.SDK.GetContract(ContractAddressBossFighting);
        var results = await contractBossFighting.ERC721.Balance();
        if (int.Parse(results) >= 1)
        {
            SceneManager.LoadSceneAsync(3);
        }
    }

}
