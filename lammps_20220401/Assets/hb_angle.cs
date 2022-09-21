using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hb_angle : MonoBehaviour
{
    // Start is called before the first frame update

    public static double arg;
    void Start()
    {
        arg = 165f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if ((coeff_choice.region == 2) && (coeff_choice.column == 0) && (coeff_choice.index1 == 2) && (coeff_choice.time_delay > 50))
        {
            arg += Input.GetAxis("joy_left_x") / 80;
            GetComponent<Text>().text = "angle cutoff: " + arg;
        }*/

        /*if ((choose_ui.col_1 == 2) && (choose_ui.time_delay > 50))
        {
            arg += Input.GetAxis("joy2_left_x") / 80;
            GetComponent<Text>().text = "angle cutoff: " + arg;
        }*/
    }
}
