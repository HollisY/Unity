using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    private static MainUI _instance;
    public static MainUI Instance
    {
        get
        {
            return _instance;
        }
    }

    public int score = 0;
    public int length = 0;
    public Text stageText;
    public Text scoreText;
    public Text lenText;
    public Image bgImage;
    public bool isPause = false;
    public bool hasBorder = true;
    private Color tempColor;
    public Image pauseImage;
    public Sprite[] pauseSprites = new Sprite[2];

    private void Awake()
    {
        _instance = this;
        
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("HasBorder", 1) == 0)
        {
            hasBorder = false;
            foreach (Transform t in bgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }

    private void Update()
    {
        switch (score / 100)
        {
            case 3:
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                bgImage.color = tempColor;
                stageText.text = "阶段二";
                break;
            case 5:
                ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                bgImage.color = tempColor;
                stageText.text = "阶段三";
                break;
            case 7:
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                bgImage.color = tempColor;
                stageText.text = "阶段四";
                break;
            case 9:
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImage.color = tempColor;
                stageText.text = "阶段五";
                break;
            case 11:
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                bgImage.color = tempColor;
                stageText.text = "无尽挑战";
                break;
        }
    }

    public void UpdateUI(int s = 5, int l = 1)
    {
        score += s;
        length += l;
        scoreText.text = "得  分:\n" + score;
        lenText.text = "长  度:\n" + length;
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseImage.sprite = pauseSprites[1];
        }
        else
        {
            Time.timeScale = 1;
            pauseImage.sprite = pauseSprites[0];
        }
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
