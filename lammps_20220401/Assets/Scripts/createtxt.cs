using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using UnityEngine;

public class createtxt : MonoBehaviour
{

    public GameObject joint0;

    //Start is called before the first frame update
    void Start()
    {
        File.Delete("D:\\data.Ag-Au1");
        CreateFile("D:/data.Ag-Au1", "#test \r\n \r\n498 atoms \r\n1 atom types \r\n \r\n-150.0 150.0  xlo xhi \r\n- 150.0 150.0  ylo yhi \r\n-150.0 150.0  zlo zhi \r\n \r\nMasses \r\n \r\n1  196.966 \r\n \r\nAtoms \r\n");
        //CreateFile("D:/MyTxt.txt", "X Y Z");
        // CreateFile("/home/zhuxi/MyTxt.txt", "X Y Z");
        Debug.Log("Script2");
    }

    //void OutPut()
    //{
    //    GameObject container = GameObject.Find("container");//找到模型存放容器
    //    print(container.name);
    //    print(container.transform.childCount);
    //    for (int i = 0; i < container.transform.childcount; i++)//读取每个原子的数据
    //    {
    //        createfile("d:/data.ag-au1", i + 1 + " " + 1 + " " + 0 + " ");
    //    }

    //   // foreach (transform child in container.transform)
    //   //{
    //   //     print(child.name);
    //   // }

    //    //container.GetComponentInChildren(typeof(Transform),true);
    //    //foreach(Transform )
    //}

    public void CreateFile(string _filePath, string _data)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(_filePath);
        //如果文件不存在
        if (!fi.Exists)
        {
            //创建文件
            sw = fi.CreateText();
        }
        else
        {
            //打开文件
            sw = fi.AppendText();
        }
        sw.WriteLine(_data);//以行的形式写入
                            //   sw.Write(_data);//首位衔接的方式写入
        sw.Close();//关闭流
        sw.Dispose();//销毁流
    }

    // Update is called once per frame
    bool isupdate = false;
    void Update()
    {
        if(!isupdate)
        {
            GameObject container = GameObject.Find("container");//找到模型存放容器
            print(container.name);
            print(container.transform.childCount);
            //for (int i = 0; i < container.transform.childCount; i++)//读取每个原子的数据
            //{
            //    CreateFile("D:/data.Ag-Au1", i+1 + " " + 1 + " " + 0 + " " );
            //}
            int i = 1;
            foreach (Transform child in container.transform)
            {
                CreateFile("D:/data.Ag-Au1", i + " " + 1 + " " + 0 + " " + child.transform.localPosition.x + " " + child.transform.localPosition.y + " " + child.transform.localPosition.z);
                i++;
            }
            isupdate = true;
        }



        //container.GetComponentInChildren(typeof(Transform),true);
        //foreach(Transform )

        // CreateFile("D:/project data/lammps/test/data.Ag-Au1", "joint0" + joint0.transform.localEulerAngles.y);
        //CreateFile("D:/MyTxt.txt", "atom1" + atom1.transform.localPosition + "\r\n" + "atom2" + joint1.transform.localPosition + "\r\n" + "atom3" + joint2.transform.localPosition + "\r\n" + "atom4" + joint3.transform.localPosition + "\r\n" + "atom5" + joint4.transform.localPosition + "\r\n");
        //CreateFile("/home/zhuxi/MyTxt.txt", "atom1" + atom1.transform.localPosition + "\r\n" + "atom2" + joint1.transform.localPosition + "\r\n" + "atom3" + joint2.transform.localPosition + "\r\n" + "atom4" + joint3.transform.localPosition + "\r\n" + "atom5" + joint4.transform.localPosition + "\r\n");
    }
}
