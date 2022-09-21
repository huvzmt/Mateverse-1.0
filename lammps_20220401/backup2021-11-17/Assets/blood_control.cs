using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class blood_control : MonoBehaviour
{
    public Slider HPStrip;
    public Image fill;
    public float HP;
    public float target_value = float.Parse(GameObject.Find("Target_D").GetComponent<Text>().text);
    // Start is called before the first frame update
    void Start()
    {
        HPStrip.value = 5f; 
        HPStrip.maxValue = 10f;
        fill.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        string txt_data = GameObject.Find("read_density").GetComponent<Text>().text;

        var match = Regex.Match(txt_data, @"([-+]?[0-9]*\.?[0-9]+)");
        if (match.Success)
        {
            HP = Convert.ToSingle(match.Groups[1].Value);
        }
        HPStrip.value = HP - target_value;
        if (HP <= 3)
        {
            fill.color = Color.red;
        }
    }
}
