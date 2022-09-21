using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charge1 : MonoBehaviour
{
    // Start is called before the first frame update

    public static double arg;

    void Start()
    {
        arg = 0.5564f;
    }

    // Update is called once per frame
    void Update()
    {
        arg = tip_coeffs.start_values[0, 0];
    //    print("charge1");

        /*if ((choose_ui.region_0 == 0) && (choose_ui.time_delay > 50))
        {
            arg += Input.GetAxis("joy_left_x") / 100;
            GetComponent<Text>().text = "charge of H: " + arg;
            GameObject.Find("Text_charge1").GetComponent<Text>().text = "charge of H: " + (arg);
        }*/
    }
}
