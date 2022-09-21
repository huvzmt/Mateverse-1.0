using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Read_cellparam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Volume").GetComponent<Text>().text = "-";
        GameObject.Find("Energy").GetComponent<Text>().text = "-";
        GameObject.Find("cellalpha").GetComponent<Text>().text = "-";
        GameObject.Find("cellbeta").GetComponent<Text>().text = "-";
        GameObject.Find("cellgamma").GetComponent<Text>().text = "-";
        GameObject.Find("cella").GetComponent<Text>().text = "-";
        GameObject.Find("cellb").GetComponent<Text>().text = "-";
        GameObject.Find("cellc").GetComponent<Text>().text = "-";
    }

    // Update is called once per frame
    void Update()
    {
        if (crystal_opt.is_update)
        {
            string[] content = File.ReadAllLines(Application.dataPath + "/hbond_fromServer/ice/cell.param");
            GameObject.Find("Volume").GetComponent<Text>().text = content[1];
            GameObject.Find("Energy").GetComponent<Text>().text = content[0];
            GameObject.Find("cellalpha").GetComponent<Text>().text = content[3];
            GameObject.Find("cellbeta").GetComponent<Text>().text = content[4];
            GameObject.Find("cellgamma").GetComponent<Text>().text = content[5];
            GameObject.Find("cella").GetComponent<Text>().text = content[6];
            GameObject.Find("cellb").GetComponent<Text>().text = content[7];
            GameObject.Find("cellc").GetComponent<Text>().text = content[8];
        }
        else {
            GameObject.Find("Volume").GetComponent<Text>().text = "-";
            GameObject.Find("Energy").GetComponent<Text>().text = "-";
            GameObject.Find("cellalpha").GetComponent<Text>().text = "-";
            GameObject.Find("cellbeta").GetComponent<Text>().text = "-";
            GameObject.Find("cellgamma").GetComponent<Text>().text = "-";
            GameObject.Find("cella").GetComponent<Text>().text = "-";
            GameObject.Find("cellb").GetComponent<Text>().text = "-";
            GameObject.Find("cellc").GetComponent<Text>().text = "-";
        }

    }
}
