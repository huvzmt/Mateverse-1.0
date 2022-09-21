using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;


public class read_last : MonoBehaviour
{
    public string txt_data;
    private string tmp;
    public static bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        txt_data = GameObject.Find("read_density").GetComponent<Text>().text;
        var match = Regex.Match(txt_data, @"([-+]?[0-9]*\.?[0-9]+)");
        if (match.Success)
        {
            tmp = Convert.ToSingle(match.Groups[1].Value).ToString();
        }
        else
        {
            tmp = "None";
        }
    }

    // Update is called once per frame
    void Update()
    {
        txt_data = GameObject.Find("read_density").GetComponent<Text>().text;
        if ((runpylammps.lammps_finished == true) && (read_density.dens_updated == false))
        {

            var match = Regex.Match(txt_data, @"([-+]?[0-9]*\.?[0-9]+)");
            if (match.Success)
            {
                tmp = Convert.ToSingle(match.Groups[1].Value).ToString();
            }
            else
            {
                tmp = "N/A";
            }
            GameObject.Find("read_density_last").GetComponent<Text>().text = "The last density is: " + tmp;
            finished = true;
        }  
        if (read_density.dens_updated == true)
        {
            finished = false;
        }
    }
}
