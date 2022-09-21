using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coeff_choice : MonoBehaviour
{
    // Start is called before the first frame update

    public static int region = 0;
    public static int column = 0;
    public static int index1 = 0;
    public static int index2 = 0;

    public static int time_delay = 0;

    public static GameObject[] region0 = new GameObject[2];

    public static GameObject hbond_pow;
    public static GameObject tip3p_cut;

    public static GameObject[] col1 = new GameObject[5];

    public static GameObject[] col2 = new GameObject[6];

    void Start()
    {
        int j = 0;
        foreach (Transform kid in GameObject.Find("region0").transform)
        {
            region0[j] = kid.gameObject;
            j += 1;
        }
        hbond_pow = GameObject.Find("hb_ratio").transform.gameObject;
        tip3p_cut = GameObject.Find("tip_ratio").transform.gameObject;
        j = 0;
        foreach (Transform kid in GameObject.Find("col1").transform)
        {
            col1[j] = kid.gameObject;
            j += 1;
        }
        j = 0;
        foreach (Transform kid in GameObject.Find("col2").transform)
        {
            col2[j] = kid.gameObject;
            j += 1;
        }

        initColors();

        print("colors initialized.");
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetAxis("joy_right_y") == -1) && (Input.GetAxis("joy_left_y") == 1) && (time_delay > 50))
        {
            // changing the region, where there are three, denoting the charges, force fields, and the coefficients.
            // THE Y AXIS INPUT IS 1 TOWARDS THE USER, AND -1 THE OTHER WAY.
            toGray();
            region += 1;
            region %= 3;
            time_delay = 0;
            toGreen();
            //print("Method region-1");
        }
        else if ((Input.GetAxis("joy_right_y") == -1) && (Input.GetAxis("joy_left_y") == -1) && (time_delay > 50))
        {
            toGray();
            region -= 1;
            region %= 3;
            time_delay = 0;
            toGreen();
            //print("Method region+1");
        }
        else if ((region != 0) && (time_delay > 50))
        {
            // changing the column being edited.
            // region 0, 1 have 1 column, while region 2 has 2 columns.
            if ((Input.GetAxis("joy_right_x") == 1) && (Input.GetAxis("joy_left_x") == +1))
            {
                toGray();
                column += 1;
                column %= 2;
                time_delay = 0;
                toGreen();
                //print("Method column+1");
            }
            else if ((Input.GetAxis("joy_right_x") == 1) && (Input.GetAxis("joy_left_x") == -1))
            {
                toGray();
                column -= 1;
                column %= 2;
                time_delay = 0;
                toGreen();
                //print("Method column-1");
            }
            // changing the index. applies only for region 2.
            else if (Input.GetAxis("joy_left_y") == 1)
            {
                toGray();
                if (column == 0)
                {
                    index1 += 1;
                    if (index1 == 5) { index1 = 0; }
                }
                else
                {
                    index2 += 1;
                    if (index2 == 6) { index2 = 0; }
                };
                time_delay = 0;
                toGreen();
            }
            else if (Input.GetAxis("joy_left_y") == -1)
            {
                toGray();
                if (column == 0) {
                    index1 -= 1;
                    if (index1 == -1) { index1 = 4; }
                }
                else {
                    index2 -= 1;
                    if (index2 == -1) { index2 = 5; } // the operator % does not work here, why???
                };
                time_delay = 0;
                toGreen();
            }
        }

        time_delay += 1;
    }

    void toGray()
    {
        // to hide the value no longer being edited.
        if (region == 0)
        {
            foreach (GameObject temp in region0)
            {
                temp.GetComponent<Renderer>().material.color = Color.gray;
            }
        }
        else if (region == 1)
        {
            if (column == 0)
            {
                hbond_pow.GetComponent<Renderer>().material.color = Color.gray;
            }
            else
            {
                tip3p_cut.GetComponent<Renderer>().material.color = Color.gray;
            }
        }
        else
        {
            if (column == 0)
            {
                col1[index1].GetComponent<Renderer>().material.color = Color.gray;
            }
            else
            {
                col2[index2].GetComponent<Renderer>().material.color = Color.gray;
            }
        }
    }

    void toGreen()
    {
        // to show the value that is now being edited.
        if (region == 0)
        {
            foreach (GameObject temp in region0)
            {
                temp.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else if (region == 1)
        {
            if (column == 0)
            {
                hbond_pow.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                tip3p_cut.GetComponent<Renderer>().material.color = Color.green;
            }
        }
        else
        {
            if (column == 0)
            {
                print("index1 from toGreen: " + index1);
                col1[index1].GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                col2[index2].GetComponent<Renderer>().material.color = Color.green;
            }
        }
    }
    
    void initColors()
    {
        foreach (GameObject gameobject in region0)
        {
            gameobject.GetComponent<Renderer>().material.color = Color.gray;
        }
        hbond_pow.GetComponent<Renderer>().material.color = Color.gray;
        tip3p_cut.GetComponent<Renderer>().material.color = Color.gray;
        foreach (GameObject gameobject in col1)
        {
            gameobject.GetComponent<Renderer>().material.color = Color.gray;
        }
        foreach (GameObject gameobject in col2)
        {
            gameobject.GetComponent<Renderer>().material.color = Color.gray;
        }
        toGreen();

        //GameObject.Find("charge1").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("charge2").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("hbond_ratio").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("tip_ratio").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("lj_c_11").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("lj_c_12").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("lj_c_22").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("tip_c_11").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("tip_c_12").GetComponent<Renderer>().material.color = Color.gray;
        //GameObject.Find("tip_c_22").GetComponent<Renderer>().material.color = Color.gray;
    }
}
