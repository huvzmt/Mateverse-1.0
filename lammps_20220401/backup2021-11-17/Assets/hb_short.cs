using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hb_short : MonoBehaviour
{
    // Start is called before the first frame update

    public static float arg;
    void Start()
    {
        arg = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((coeff_choice.region == 2) && (coeff_choice.column == 0) && (coeff_choice.index1 == 0) && (coeff_choice.time_delay > 50))
        {
            arg += Input.GetAxis("joy_left_x") / 100;
            GetComponent<Text>().text = "short cutoff: " + arg;
        }
    }
}
