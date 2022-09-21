using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ratio_hb : MonoBehaviour
{
    // Start is called before the first frame update

    public static float hbond_order;

    void Start()
    {
        hbond_order = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if ((coeff_choice.region == 1) && (coeff_choice.column == 0) && (coeff_choice.time_delay > 50) && ((Input.GetAxis("joy_left_x") == 1)|| (Input.GetAxis("joy_left_x") == -1)))
        {
            hbond_order += Input.GetAxis("joy_left_x");
            GetComponent<Text>().text = "order of hbond: " + hbond_order;
        }
    }
}
