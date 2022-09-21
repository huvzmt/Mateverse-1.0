//namespace VRTK
//{
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class changecolor : MonoBehaviour
{
    static bool istogon=false;
    //Transform changer;
    //public GameObject originobj; 
    Color origin ;
    int i;

    public void Toggle(bool state)
    {
        //Debug.Log("The toggle state is " + (state ? "on" : "off"));
        //changer = transform.Find("container");
        ////Color origin = changer.GetChild(0).GetComponent<MeshRenderer>().material.color;
        //print(changer.name);
        istogon = state;
        print(istogon);

    }


    void Start()
    {
        origin = this.GetComponent<MeshRenderer>().material.color;
       // print(origin);
       // print(this.name);
       // print(originobj.name);
        //base.Start();
        //changer = transform.Find("container");
        ////Color origin = changer.GetChild(0).GetComponent<MeshRenderer>().material.color;
        //print(changer.name);
        //print(origin);
    }

    void Update()
    {        
        //print(this.GetComponent<MeshRenderer>().material.color);
        //if(istogon==true)
        //{
        //    if (this.GetComponent<Text>().text == "0")
        //    {
        //        this.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.1f, 1f);
        //    }
        //    else if (this.GetComponent<Text>().text == "1")
        //    {
        //        this.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.6f, 0f, 1f);
        //    }
        //    else if (this.GetComponent<Text>().text == "2")
        //    {
        //        this.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0.6f, 1f);
        //    }
        //    else if (this.GetComponent<Text>().text == "3")
        //    {
        //        this.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.1f, 0f, 1f);
        //    }
        //    else if (this.GetComponent<Text>().text == "4")
        //    {
        //        this.GetComponent<MeshRenderer>().material.color = new Color(0f, 0.6f, 0f, 1f);
        //    }

        //    print(this.GetComponent<Text>().text);
            //print(origin);
            //print(a);

            //this.GetComponent<MeshRenderer>().material.color = new Color(i * Time.deltaTime, 0f, 0f, 1f);
            //i++;
            //if (i > 60)
            //{
            //    i = 1;
            //}
            //print(this.GetComponent<MeshRenderer>().material.color);

        //}
        //if(istogon==false)
        //{
        //    this.GetComponent<MeshRenderer>().material.color = origin;
        //    //print(a);
        //}
    }

}
//}
