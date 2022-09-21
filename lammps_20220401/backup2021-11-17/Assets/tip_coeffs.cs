using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tip_coeffs : MonoBehaviour
{
    // Start is called before the first frame update

    public static float arg0;
    public static float arg1;
    public static float arg2;
    public static float arg3;
    public static float arg4;
    public static float arg5;

    void Start()
    {
        arg0 = 0f;
        arg1 = 0f;
        arg2 = 0f;
        arg3 = 0f;
        arg4 = 0.1852f;
        arg5 = 3.15f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((coeff_choice.region == 2) && (coeff_choice.column == 1) && (coeff_choice.time_delay > 50))
        {
            if (coeff_choice.index2 == 0)
            {
                arg0 += Input.GetAxis("joy_left_x") / 100;
                GetComponent<Text>().text = "pair 1-1 0: " + arg0;
            }
            else if (coeff_choice.index2 == 1)
            {
                arg1 += Input.GetAxis("joy_left_x") / 100;
                GameObject.Find("Text_tip_c_11_1").GetComponent<Text>().text = "pair 1-1 1: " + arg1;
            }
            else if (coeff_choice.index2 == 2)
            {
                arg2 += Input.GetAxis("joy_left_x") / 100;
                GameObject.Find("Text_tip_c_12_0").GetComponent<Text>().text = "pair 1-2 0: " + arg2;
            }
            else if (coeff_choice.index2 == 3)
            {
                arg3 += Input.GetAxis("joy_left_x") / 100;
                GameObject.Find("Text_tip_c_12_1").GetComponent<Text>().text = "pair 1-2 1: " + arg3;
            }
            else if (coeff_choice.index2 == 4)
            {
                arg4 += Input.GetAxis("joy_left_x") / 100;
                GameObject.Find("Text_tip_c_22_0").GetComponent<Text>().text = "pair 2-2 0: " + arg4;
            }
            else if (coeff_choice.index2 == 5)
            {
                arg5 += Input.GetAxis("joy_left_x") / 100;
                GameObject.Find("Text_tip_c_22_1").GetComponent<Text>().text = "pair 2-2 1: " + arg5;
            }
        }
    }
}
