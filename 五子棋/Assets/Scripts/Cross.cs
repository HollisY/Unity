using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour {

    public int GridX;
    public int GridY;
    public MainLoop mainloop;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            mainloop.OnClick(this);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
