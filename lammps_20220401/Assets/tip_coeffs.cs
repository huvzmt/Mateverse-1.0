using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tip_coeffs : MonoBehaviour
{
    // Start is called before the first frame update
    public static double arg;
    public static double arg0;
    public static double arg1;
    public static double arg2;
    public static double arg3;
    public static double arg4;
    public static double arg5;

    public static double charge_H;
    public static double charge_O;
    public static double hbond;
    public static double epsilon;
    public static double sigma;

    public static double cutoff;
    public static double pair1;
    public static double pair2;
    public static double pair3;
    public static double pair4;
    public static double pair5;
    public static double pair6;

    public static string[,] texts = new string[,] { { "Text_charge1", "Text_charge2", "Text_hbond_order", "Text_hb_epsilon", "Text_hb_sigma", "", "" }, 
                                                    { "Text_tip3p_cutoff", "Text_tip_c_11_0", "Text_tip_c_11_1", "Text_tip_c_12_0", "Text_tip_c_12_1", 
                                                      "Text_tip_c_22_0", "Text_tip_c_22_1" } };
    
    public static string[,] contents = new string[,] { { "charge of H: ", "charge of O: ", "order of hbond: ", "epsilon: ", "sigma: ", "", "" },
                                                    { "TIP3P cutoff: ", "pair 1-1 0: ", "pair 1-1 1: ", "pair 1-2 0: ", "pair 1-2 1: ",
                                                      "pair 2-2 0: ", "pair 2-2 1: " } };

    public static double[,] start_values = new double[,] { { 0.5564f, -1.1128f, 8f, 0.2f, 3.1f, 0f, 0f }, { 10f, 0f, 0f, 0f, 0f, 0.1118f, 3.118f } };

    void Start()
    {
        arg = 0f;
        arg0 = 0f;
        arg1 = 0f;
        arg2 = 0f;
        arg3 = 0f;
        arg4 = 0.1852f;
        arg5 = 3.15f;

        cutoff = 10f;
        pair1 = 0f;
        pair2 = 0f;
        pair3 = 0f;
        pair4 = 0f;
        pair5 = 0.1852f;
        pair6 = 3.15f;
}

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < 7; j++) {
                if (choose_ui.mark[i, j] != 0) {
                    if (choose_ui.mark[i, j] == 1) {
                        arg0 = start_values[i, j];
                        arg0 += Input.GetAxis("joy_left_x") / 1000;
                        GameObject.Find(texts[i, j]).GetComponent<Text>().text = contents[i, j] + ((float)arg0);
                 //       print(arg0);
                        start_values[i, j] = arg0;
                    }
                    else if (choose_ui.mark[i, j] == 2)
                    {
                        arg1 = start_values[i, j];
                        arg1 += Input.GetAxis("joy1_left_x") / 1000;
                        GameObject.Find(texts[i, j]).GetComponent<Text>().text = contents[i, j] + ((float)arg1);
                        start_values[i, j] = arg1;
                        print("arg1 = " + arg1);
                        print("start_values[i, j] = "+ start_values[i, j]);
                    }
                    else if (choose_ui.mark[i, j] == 3)
                    {
                        arg2 = start_values[i, j];
                        arg2 += Input.GetAxis("joy2_left_x") / 1000;
                        GameObject.Find(texts[i, j]).GetComponent<Text>().text = contents[i, j] + ((float)arg2);
                        start_values[i, j] = arg2;
              //          print(arg2);
                    }
                    else if (choose_ui.mark[i, j] == 4)
                    {
                        arg3 = start_values[i, j];
                        arg3 += Input.GetAxis("joy3_left_x") / 1000;
                        GameObject.Find(texts[i, j]).GetComponent<Text>().text = contents[i, j] + ((float)arg3);
                        start_values[i, j] = arg3;
              //          print(arg3);
                    }   
                }
            }
        }


        /*if ((choose_ui.col_2 == 0) && (choose_ui.time_delay > 50))
        {
            arg0 += Input.GetAxis("joy3_left_x") / 1000;
            GetComponent<Text>().text = "pair 1-1 0: " + arg0;
        }
        else if ((choose_ui.col_2 == 1) && (choose_ui.time_delay > 50)) {
            arg1 += Input.GetAxis("joy3_left_x") / 1000;
            GameObject.Find("Text_tip_c_11_1").GetComponent<Text>().text = "pair 1-1 1: " + arg1;
        }
        else if ((choose_ui.col_2 == 2) && (choose_ui.time_delay > 50))
        {
            arg2 += Input.GetAxis("joy3_left_x") / 1000;
            GameObject.Find("Text_tip_c_12_0").GetComponent<Text>().text = "pair 1-2 0: " + arg2;
        }
        else if ((choose_ui.col_2 == 3) && (choose_ui.time_delay > 50))
        {
            arg3 += Input.GetAxis("joy3_left_x") / 1000;
            GameObject.Find("Text_tip_c_12_1").GetComponent<Text>().text = "pair 1-2 1: " + arg3;
        }
        else if ((choose_ui.col_2 == 4) && (choose_ui.time_delay > 50))
        {
            arg4 += Input.GetAxis("joy3_left_x") / 1000;
            GameObject.Find("Text_tip_c_22_0").GetComponent<Text>().text = "pair 2-2 0: " + arg4;
        }
        else if ((choose_ui.col_2 == 5) && (choose_ui.time_delay > 50))
        {
            arg5 += Input.GetAxis("joy3_left_x") / 1000;
            GameObject.Find("Text_tip_c_22_1").GetComponent<Text>().text = "pair 2-2 1: " + arg5;
        }*/

        /*if ((coeff_choice.region == 2) && (coeff_choice.column == 1) && (coeff_choice.time_delay > 50))
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
        }*/
    }
}
