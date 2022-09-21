using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ReadDens : MonoBehaviour
{
    Text text;
    string content;
    // Start is called before the first frame update
    void Start()
    {
        //text = GameObject.Find("Canvas (1)/Text (3)").GetComponent<Text>();
        //content = File.ReadAllText(@"D:\project_data\lammps\water\TIP4P-2005\T273K\wat.DENS");
        //text.text = content;
    }

    //void ReadTxtSecond()
    //{
    //    string path = @"D:\project_data\lammps\water\TIP4P-2005\T273K\wat.DENS";
    //    //逐行读取返回的为数组数据
    //    string[] strs = File.ReadAllLines(path);
        


    //}

    // Update is called once per frame
    void Update()
    {
        text = GameObject.Find("Canvas (1)/Text (3)").GetComponent<Text>();
        content = File.ReadAllText(@"D:\project_data\lammps\water\TIP4P-2005\T273K\wat.DENS");
        text.text = content;
    }
}
