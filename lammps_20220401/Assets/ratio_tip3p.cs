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

        /*if ((choose_ui.region_1 == 1) && (choose_ui.time_delay > 50))
        {
            cutoff += Input.GetAxis("joy1_left_x");
            GetComponent<Text>().text = "TIP3P cutoff: " + cutoff;
        }*/
    }
}
