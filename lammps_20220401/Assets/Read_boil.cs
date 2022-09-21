using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Read_boil : MonoBehaviour
{
    public static int visualization_frame = 1;                                  // the frame to show

    public static string[] map_row_string;
    public static int atomnum;
    public static int filelen;
    public static int map_row_max_cells;
    public static List<List<string>> map_Collections = new List<List<string>>();
    // Start is called before the first frame update
    void Start()
    {
        map_row_string = File.ReadAllLines(@"D:\project_data\lammps\273K_393K\220919\523K.1Bar.dump");
        atomnum = int.Parse(map_row_string[3]);                                 // number of atoms
        filelen = map_row_string.Length;                                        // the length of the file
        map_row_max_cells = 5;                                                  // 这个二维表中，最大列数，也就是在一行中最多有个单元格
        Application.targetFrameRate = 10;
    }


    // Update is called once per frame
    void Update()
    {
        visualization();
        visualization_frame++;
    }

    public void visualization()
    {

        GameObject container = GameObject.Find("container");                // 找到模型存放容器
        GameObject[] kids = new GameObject[container.transform.childCount];

        int j = 0;
        foreach (Transform kid in container.transform)
        {
            kids[j] = kid.gameObject;
            j += 1;
        }
        foreach (GameObject kid in kids)
        {
            DestroyImmediate(kid.gameObject);
        }                                                                   // clean up the container
        map_Collections.Clear();

        if ((visualization_frame + 1) * (atomnum + 9) > filelen)
        {
            return;
        }

        string[] box_x = map_row_string[(visualization_frame) * (atomnum + 9) + 5].Split(' ');
        float box_xlo = float.Parse(box_x[0]);
        float box_xhi = float.Parse(box_x[1]);
        string[] box_y = map_row_string[(visualization_frame) * (atomnum + 9) + 6].Split(' ');
        float box_ylo = float.Parse(box_y[0]);
        float box_yhi = float.Parse(box_y[1]);
        string[] box_z = map_row_string[(visualization_frame) * (atomnum + 9) + 7].Split(' ');
        float box_zlo = float.Parse(box_z[0]);
        float box_zhi = float.Parse(box_z[1]);


        for (int i = (visualization_frame) * (atomnum + 9) + 9; i < (visualization_frame + 1) * (atomnum + 9); i++)       //读取每一行的数据
        {
            //print("slicing a row now.");
            //print("Now at row number " + i.ToString());
            List<string> map_row = new List<string>(map_row_string[i].Split(' '));//按空格分割每个一个单元格
            //print("row sliced.");
            if (map_row_max_cells < map_row.Count)
            {//求一行中最多有个单元格
                map_row_max_cells = map_row.Count;
            }
            map_Collections.Add(map_row);//整理好，放到容器map_Collections中
            //print("reading line " + i.ToString());

        }
        //print("Dump data read.");

        visualization_frame += 1;

        //if (isupdate == false)
        for (int i = 0; i < map_Collections.Count; i++)//Z方向是长度就是容器的大小，也就是*读入数据*有多少有效的行
        {
            GameObject atom = Resources.Load("Prefabs/Au") as GameObject;
            GameObject atomInstance = Instantiate(atom);
            //print("before bug?");
            atomInstance.transform.parent = container.transform;
            //print("after bug?");

            atomInstance.name = "sphere " + i;
            //print("atom named with number " + i);
            atomInstance.transform.position = new Vector3((box_xlo + float.Parse(map_Collections[i][2]) * (box_xhi - box_xlo)) / 2 - 3f,
                                                          (box_ylo + float.Parse(map_Collections[i][3]) * (box_yhi - box_ylo)) / 2,
                                                          (box_zlo + float.Parse(map_Collections[i][4]) * (box_zhi - box_zlo)) / 2 + 30f);
            //print("atom repositioned");

            float element_type = float.Parse(map_Collections[i][1]);
            if (element_type == 2)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.white;
                atomInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (element_type == 1)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.red;
                atomInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            //atom.GetComponent<Text>().text = map_Collections[i][5];
        }
    }

}
