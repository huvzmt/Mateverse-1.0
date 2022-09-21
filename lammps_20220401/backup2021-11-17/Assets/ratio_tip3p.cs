using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ratio_tip3p : MonoBehaviour
{
    // Start is called before the first frame update

    public static float cutoff;

    void Start()
    {
        cutoff = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((coeff_choice.region == 1) && (coeff_choice.column == 1) && (coeff_choice.time_delay > 50))
        {
            cutoff += Input.GetAxis("joy_left_x") / 100;
            GetComponent<Text>().text = "lj/coul cutoff: " + cutoff;
        }
    }
}
