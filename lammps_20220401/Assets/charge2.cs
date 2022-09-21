using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charge2 : MonoBehaviour
{
    // Start is called before the first frame update

    public static double arg;

    void Start()
    {
        arg = -1.1128f;
    }

    // Update is called once per frame
    void Update()
    {
        arg = tip_coeffs.start_values[0, 1];

        /* if ((choose_ui.region_0 == 1) && (choose_ui.time_delay > 50))
         {
             arg += Input.GetAxis("joy_left_x") / 100;
             GetComponent<Text>().text = "charge of O: " + arg;
             GameObject.Find("Text_charge2").GetComponent<Text>().text = "charge of O: " + (-2 * arg);
         }*/
    }
}
