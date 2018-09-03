using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSizeUIElement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int height = Screen.height;
        float scale = height / 525f;
        float newYPos = ((height / 2f) - (40f / 525f * height)) * -1f;
        Text txt = GetComponent<Text>();
        txt.rectTransform.localPosition = new Vector3(0, newYPos, 0);
        txt.transform.localScale = new Vector3(scale, scale, scale);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
