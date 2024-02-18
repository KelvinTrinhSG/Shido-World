using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Thirdweb;

public class NFTManager : MonoBehaviour
{
    public const string ContractAddress = "0xA9f52287BBbb8b459A824F188BA35662cf13D5b3";
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
