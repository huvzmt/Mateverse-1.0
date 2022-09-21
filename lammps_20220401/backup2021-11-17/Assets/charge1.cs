using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charge1 : MonoBehaviour
{
    // Start is called before the first frame update

    public static float arg;

    void Start()
    {
        arg = 0.5564f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((coeff_choice.region == 0) && (coeff_choice.time_delay > 50))
        {
            arg += Input.GetAxis("joy_left_x") / 100;
            GetComponent<Text>().text = "charge of H: " + arg;
            GameObject.Find("Text_charge2").GetComponent<Text>().text = "charge of O: " + (-2*arg);
        }
    }
}
