using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hb_sigma : MonoBehaviour
{
    // Start is called before the first frame update

    public static double arg;
    void Start()
    {
        arg = 3.1f;
    }

    // Update is called once per frame
    void Update()
    {
        arg = tip_coeffs.start_values[0, 4];

        /*if ((choose_ui.col_1 == 4) && (choose_ui.time_delay > 50))
        {
            arg += Input.GetAxis("joy2_left_x") / 100;
            GetComponent<Text>().text = "sigma: " + arg;
        }*/
    }
}
