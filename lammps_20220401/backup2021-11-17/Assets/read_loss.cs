using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class read_loss : MonoBehaviour
{
    public string txt_data;
    private float target_value;
    private string loss;
    // Start is called before the first frame update
    void Start()
    {
        target_value = float.Parse(GameObject.Find("Target_rho").GetComponent<Text>().text);
    }

    // Update is called once per frame
    void Update()
    {
        txt_data = GameObject.Find("read_density").GetComponent<Text>().text;
        if ((runpylammps.lammps_finished == true) && (read_density.dens_updated == true))
        {

            var match = Regex.Match(txt_data, @"([-+]?[0-9]*\.?[0-9]+)");
            if (match.Success)
            {
                loss = Math.Abs(Convert.ToSingle(match.Groups[1].Value) - target_value).ToString();
            }
            else
            {
                loss = "N/A";
            }
            GameObject.Find("read_loss").GetComponent<Text>().text = "The loss is: " + loss;
        }
    }
}
