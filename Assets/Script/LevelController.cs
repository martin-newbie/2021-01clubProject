using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public int maxXp;

    public bool levelUpChk;

    public static int currentLevel;
    public int nextLevel;

    public Slider XpSlider;

    public Text previousLevel;
    public Text NextLevel;

    private bool loadGame;
    private bool newGame;

    private int temp;

    public static int score;

    void Start()
    {
        string newgame = PlayerPrefs.GetString("newGame", "true");
        string loadgame = PlayerPrefs.GetString("loadGame", "false");

        newGame = System.Convert.ToBoolean(newgame);
        loadGame = System.Convert.ToBoolean(loadgame);

        if (newGame)
        {
            currentLevel = 1;
            levelUpChk = false;
            score = 0;
            maxXp = 100;
        }
        else if(loadGame)
        {
            currentLevel = (int)PlayerPrefs.GetFloat("Level");
            score = (int)PlayerPrefs.GetFloat("Score");
            maxXp = (int)PlayerPrefs.GetFloat("MaxXp");
            string value = PlayerPrefs.GetString("LevelUpChk", "false");
            levelUpChk = System.Convert.ToBoolean(value);
        }
    }

    void Update()
    {
        nextLevel = currentLevel + 1;
        XpController();
        LevelUp();
        TextPrint();
        Gameover();
    }

    private void XpController()
    {
        XpSlider.value = (float)score / maxXp;
        PlayerPrefs.SetFloat("Score", score);
    }

    private void LevelUp()
    {
        if (score >= maxXp && levelUpChk == false && currentLevel <= 9)
        {
            Debug.Log("Level up");
            if(score > maxXp)
            {
                temp = score - maxXp;
                score = 0 + temp;
            }
            else
            {
                score = 0;
            }
            currentLevel += 1;
            levelUpChk = true;
        }

        if (levelUpChk == true)
        {
            if (currentLevel < 5)
            {
                maxXp += 50;
            }
            else if (currentLevel < 6)
            {
                maxXp += 100;
            }
            else if (currentLevel < 9)
            {
                maxXp += 250;
            }
            else
            {
                maxXp = 0;
            }
            levelUpChk = false;
        }
        PlayerPrefs.SetFloat("Level", currentLevel);
        PlayerPrefs.SetFloat("MaxXp", maxXp);
        PlayerPrefs.SetString("LevelUpChk", levelUpChk.ToString());
    }

    private void TextPrint()
    {
        previousLevel.text = currentLevel.ToString();
        NextLevel.text = nextLevel.ToString();
    }

    private void Gameover()
    {
        if (GameOver.gameoverSceneChk)
        {
            PlayerPrefs.SetFloat("Level", 0);
            PlayerPrefs.SetFloat("MaxXp", 100);
            PlayerPrefs.SetFloat("Score", 0);
        }
    }
}
