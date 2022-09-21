using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hb_epsilon : MonoBehaviour
{
    // Start is called before the first frame update

    public static double arg;
    void Start()
    {
        arg = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        arg = tip_coeffs.start_values[0, 3];

        /*if ((choose_ui.col_1 == 3) && (choose_ui.time_delay > 50))
        {
            arg += Input.GetAxis("joy2_left_x") / 1000;
            GetComponent<Text>().text = "epsilon: " + arg;
        }*/
    }
}
