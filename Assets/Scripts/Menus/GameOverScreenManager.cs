using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreenManager : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject LoseScreen;

    private void Awake()
    {
        if (GameManager.PlayerWin) WinScreen.SetActive(true);
        else LoseScreen.SetActive(true);
    }
}
