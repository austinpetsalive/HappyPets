using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using ZXing;
using ZXing.QrCode;

public class QRReader : MonoBehaviour {
    private Camera thisCamera;
    private Texture2D texture;
    private RectInt screenRect;
    private Rect screenRectF;
    public Text displayText;
    IBarcodeReader barcodeReader;
    RenderTexture rt;
    bool frameReady;
    string previousVideoURL;
    public VideoPlayer player;
    public Texture playerTex;
    // Use this for initialization
    void Start () {
        screenRect = new RectInt(0, 0, Screen.width, Screen.height);
        screenRectF = new Rect(0, 0, Screen.width, Screen.height);
        thisCamera = GetComponent<Camera>();
        barcodeReader = new BarcodeReader();
        texture = new Texture2D(screenRect.width, screenRect.height, TextureFormat.RGB24, false);
        rt = new RenderTexture(screenRect.width, screenRect.height, 24);
        frameReady = false;
        previousVideoURL = "";
    }

    // Update is called once per frame
    void Update () {
        if (!frameReady)
        {
            thisCamera.targetTexture = rt;
            thisCamera.Render();
            RenderTexture.active = rt;

            texture.ReadPixels(screenRectF, 0, 0);
            texture.Apply();

            RenderTexture.active = null;
            thisCamera.targetTexture = null;

            frameReady = true;
        }
    }

    void FixedUpdate()
    {
        if (frameReady)
        {
            try
            {
                var result = barcodeReader.Decode(texture.GetPixels32(), texture.width, texture.height);
                if (result != null)
                {
                    displayText.text = result.Text;
                    if (result.Text != previousVideoURL) {
                        //Update
                        Debug.Log("Update");
                        player.Stop();
                        player.enabled = false;
                        previousVideoURL = result.Text;
                        playerTex = new Texture2D(playerTex.width, playerTex.height);
                        player.url = result.Text;
                        player.enabled = true;
                        player.frame = 0;
                        player.Play();
                    }
                }
                else
                {
                    displayText.text = "No Measurement";
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }

            frameReady = false;
        }
    }
}
