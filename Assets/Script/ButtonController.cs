using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public static bool newGame;
    public static bool loadGame;

    [SerializeField]
    private GameObject setting;
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private GameObject reGame;
    private bool reGameOpen;
    private bool settingOpen;
    private bool exitOpen;

    public static bool gameStart;

    void Start()
    {
        gameStart = false;
        newGame = false;
        loadGame = false;
        settingOpen = false;
        exitOpen = false;
        reGameOpen = false;
    }

    void Update()
    {
        setting.SetActive(settingOpen);

        if(exit != null)
        {
            exit.SetActive(exitOpen);
        }
        if(reGame != null)
        {
            reGame.SetActive(reGameOpen);
        }
    }

    public void NewGame()
    {
        newGame = true;
        loadGame = false;
        PlayerPrefs.SetString("newGame", newGame.ToString());
        PlayerPrefs.SetString("loadGame", loadGame.ToString());
        SceneManager.LoadScene("InGameScene");
    }

    public void LoadGame()
    {
        newGame = false;
        loadGame = true;
        PlayerPrefs.SetString("newGame", newGame.ToString());
        PlayerPrefs.SetString("loadGame", loadGame.ToString());
        SceneManager.LoadScene("InGameScene");
    }

    public void Setting()
    {
        settingOpen = !settingOpen;
    }

    public void Exit()
    {
        exitOpen = !exitOpen;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ToHome()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ReGameScene()
    {
        reGameOpen = !reGameOpen;
    }

    public void ReGame()
    {
        newGame = true;
        loadGame = false;
        PlayerPrefs.SetString("newGame", newGame.ToString());
        PlayerPrefs.SetString("loadGame", loadGame.ToString());
        SceneManager.LoadScene("InGameScene");
    }

}
