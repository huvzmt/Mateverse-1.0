using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class save : MonoBehaviour
{
    private struct ImageData
    {
        public long timestamp;
        public byte[] data;
    }
    public Camera cam;
    public bool capture = false;
    // Start is called before the first frame update
    void Start()
    {
        // cam = GameObject.Find("Camera(eye)").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        // cam = GameObject.Find("Camera(eye)").GetComponent<Camera>();
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (!capture)
            {
                cam.enabled = true;
                capture = true;
                StartCoroutine(CaptureAndSave());
                Debug.Log("REC!");
            }
            else
            {
                cam.enabled = false;
                capture = false;
                StopCoroutine(CaptureAndSave());
                Debug.Log("REC STOP!");
            }
        }
    }

    public IEnumerator CaptureAndSave()
    {
        while (capture)
        {
            ImageData pack;
            pack.timestamp = System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            ScreenCapture.CaptureScreenshot("D:\\" + pack.timestamp + ".png");
            yield return new WaitForSeconds(1);
        }
    }
}
