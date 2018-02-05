using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindow : MonoBehaviour {

    public Button ReplayButton;
    public Text Message;
    public MainLoop mainloop;

	// Use this for initialization
	void Start () {
        ReplayButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            mainloop.Restart();
        });
    }

    public void Show(ChessType wintype)
    {
        switch (wintype)
        {
            case ChessType.Black:
                Message.text = string.Format("恭喜，你战胜了电脑！");
                break;
            case ChessType.White:
                Message.text = string.Format("很遗憾，你输给了电脑！");
                break;
        }
    }
}
