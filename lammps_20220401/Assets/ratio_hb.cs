using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ratio_hb : MonoBehaviour
{
    // Start is called before the first frame update

    public static double hbond_order;

    void Start()
    {
        hbond_order = 8;
    }

    // Update is called once per frame
    void Update()
    {
        hbond_order = tip_coeffs.start_values[0, 2];

        /*if ((choose_ui.region_1 == 0) && (choose_ui.time_delay > 50))
        {
            hbond_order += Input.GetAxis("joy1_left_x");
            GetComponent<Text>().text = "order of hbond: " + hbond_order;
        }*/
    }
}
