using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;  //操作文件夹时需引用该命名空间
using System.Text;
using UnityEngine.EventSystems;

public class TxtWriteAndRead : MonoBehaviour
{
    public Slider targetSliderObject;

    void Start()
    {

        targetSliderObject.onValueChanged.AddListener((float value) => OnSliderValueChange(targetSliderObject)); 



    }
    public void OnSliderValueChange(Slider EventSender)
    {
        if (EventSender.name == "Slider (1)") {
            AddTxtTextByFileInfo(targetSliderObject.value.ToString(),"S1");
        }
        if (EventSender.name == "Slider (2)")
        {
            AddTxtTextByFileInfo(targetSliderObject.value.ToString(),"S2");
        }
        //AddTxtTextByFileInfo(targetSliderObject.value.ToString());
    }
   

    public void AddTxtTextByFileInfo(string txtText,string prefix)
    {
        string path = Application.dataPath + "/"+ prefix +".txt";
        StreamWriter sw;
        FileInfo fi = new FileInfo(path);

        //if (!File.Exists(path))
        //{
        //    sw = fi.CreateText();
        //}
        //else
        //{
        //    sw = fi.AppendText();   //在原文件后面追加内容      
        //}

        sw = fi.CreateText();

        sw.WriteLine(txtText);
        sw.Close();
        sw.Dispose();
    }

    private void Update()
    {
        OnSliderValueChange(targetSliderObject);
    }

}
