using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hb_long : MonoBehaviour
{
    // Start is called before the first frame update

    public static double arg;
    void Start()
    {
        arg = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if ((coeff_choice.region == 2) && (coeff_choice.column == 0) && (coeff_choice.index1 == 1) && (coeff_choice.time_delay > 50))
        {
            arg += Input.GetAxis("joy_left_x") / 100;
            GetComponent<Text>().text = "long cutoff: " + arg;
        }*/

        /*if ((choose_ui.col_1 == 1) && (choose_ui.time_delay > 50))
        {
            arg += Input.GetAxis("joy2_left_x") / 100;
            GetComponent<Text>().text = "long cutoff: " + arg;
        }*/
    }
}
