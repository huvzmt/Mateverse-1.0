using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using UnityEngine.EventSystems;

public class read_density : MonoBehaviour
{
    // Start is called before the first frame update

    public static string[] map_row_string;
    public static List<List<string>> map_Collections = new List<List<string>>();
    public static bool dens_updated;

    void Start()
    {
        dens_updated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((runpylammps.lammps_finished == true) && (dens_updated == false))
        {
            map_row_string = File.ReadAllLines(@"D:\project_data\lammps\water_single\hbond_fromServer\test.dens");
            int filelen = map_row_string.Length;
            int i = 0;
            float temp = 0f;
            while (i < 10)
            {
                temp += float.Parse(map_row_string[filelen - 11 + i].Split(' ')[1]);
                i += 1;
            }
            temp /= 10;
            GameObject.Find("read_density").GetComponent<Text>().text = "Result of current step: " + temp;
            dens_updated = true;
        }
    }
}
