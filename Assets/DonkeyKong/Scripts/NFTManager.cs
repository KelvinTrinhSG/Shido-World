using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Thirdweb;

public class NFTManager : MonoBehaviour
{
    public const string ContractAddress = "0x30eFF7929136Fa21e89c9FB3c547B46Dcf49E0C9";
    private Contract contract;
    public Text btnTxt;
    public Text claimBtnTxt;
    public GameObject ClaimButton;
    public async void GetNFTBalance()
    {
        btnTxt.text = "Getting balance...";
        contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress);
        var results = await contract.ERC721.Balance();
        btnTxt.text = results;
    }

    public async void ClaimNFT()
    {
        try
        {
            btnTxt.text = "Claiming NFT...";
            contract = ThirdwebManager.Instance.SDK.GetContract(ContractAddress);
            var results = await contract.ERC721.Claim(1);
            btnTxt.text = "NFT Claimed!";
            ClaimButton.SetActive(false);
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
