using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//teste
using System;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

public class ForceSystem : MonoBehaviour
{
    [SerializeField] private Text powerText;
    private float powerMax;
    private float power;
    private float powerUseAmount;
    public System.Drawing.Graphics graphics;

    // Use this for initialization
    void Start()
    {
        powerMax = 40;
        power = powerMax;
        powerUseAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (power < powerMax)
            power+=0.5f;


        if (power > 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-power, 0));
                power -= powerUseAmount;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(power, 0));
                power -= powerUseAmount;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, power));
                power -= powerUseAmount;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -power));
                power -= powerUseAmount;
            }
        }

        powerText.text = power.ToString();

        if (Input.GetKeyDown(KeyCode.Return))
        {

            //ScreenCapture.CaptureScreenshot("testeshoot.png");
        }
    }

    Bitmap memoryImage;
    Size size;
    private void MyScreenShoot()
    {
        size = new Size(200,200);
        memoryImage = new Bitmap(800, 800);
        System.Drawing.Graphics memoryGraphics = System.Drawing.Graphics.FromImage(memoryImage);
        memoryGraphics.CopyFromScreen(500,500, 0, 0, size);
    }

}
