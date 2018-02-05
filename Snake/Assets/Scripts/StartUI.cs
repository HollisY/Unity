using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public Text lastText;
    public Text bestText;
    public Toggle blueTog;
    public Toggle yellowTog;
    public Toggle borderTog;
    public Toggle freeTog;

    private void Awake()
    {
        lastText.text = "上次： " + PlayerPrefs.GetInt("LastScore", 0) + " ";
        bestText.text = "最佳： " + PlayerPrefs.GetInt("BestScore", 0) + " ";
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("SnakeHead", "sh01") == "sh01")
        {
            blueTog.isOn = true;
        }
        else
        {
            yellowTog.isOn = true;
        }

        if (PlayerPrefs.GetInt("HasBorder", 1) == 1)
        {
            borderTog.isOn = true;
        }
        else
        {
            freeTog.isOn = true;
        }
    }

    public void BlueSnake(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("SnakeHead", "sh01");
        }
    }

    public void YellowSnake(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("SnakeHead", "sh02");
        }
    }

    public void Border(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("HasBorder", 1);
        }
    }

    public void Free(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("HasBorder", 0);
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
