using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Thirdweb;

public class MainMenu : MonoBehaviour
{
    public async void UpdateCanPlayGameStatus()
    {
        var result = await ThirdwebManager.Instance.SDK.wallet.GetAddress();
        SceneManager.LoadSceneAsync(1);
    }
}
