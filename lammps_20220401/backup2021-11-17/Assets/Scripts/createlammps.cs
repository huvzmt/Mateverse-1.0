using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using UnityEngine;

public class createlammps : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        //TextAsset textAsset = (TextAsset)Resources.Load("tmp1");//载入数据
        //string[] map_row_string = textAsset.text.Trim().Split('\n');//清除这个Map.csv前前后后的换行，空格之类的，并按换行符分割每一行
        string[] map_row_string = File.ReadAllLines(@"D:\project data\lammps\test-new\tmp.dump");
        int map_row_max_cells = 5;//这个二维表中，最大列数，也就是在一行中最多有个单元格
        List<List<string>> map_Collections = new List<List<string>>();//设置一个C#容器map_Collections

        GameObject container = GameObject.Find("container");//找到模型存放容器
        //GameObject container = new GameObject("container");//设置一个容器存放模型
        //print(map_row_string.Length);
        
        for (int i = 9; i < 507; i++)//读取每一行的数据
        {
            List<string> map_row = new List<string>(map_row_string[i].Split(' '));//按逗号分割每个一个单元格
            if (map_row_max_cells < map_row.Count)
            {//求一行中最多有个单元格
                map_row_max_cells = map_row.Count;
            }
            map_Collections.Add(map_row);//整理好，放到容器map_Collections中
        }

        //print(map_Collections.Count);

        ///*生成一个刚好放好Cube的Plane*/
        //GameObject map_plane = GameObject.CreatePrimitive(PrimitiveType.Plane);//生成一个Plane
        //map_plane.transform.position = new Vector3(0, 0, 0);//放到(0,0,0)这个位置
        ////求其原始大小
        //float map_plane_original_x_size = map_plane.GetComponent<MeshFilter>().mesh.bounds.size.x;
        //float map_plane_original_z_size = map_plane.GetComponent<MeshFilter>().mesh.bounds.size.z;
        ////缩放这个Map到所需大小，刚好和二维表匹配
        //float map_plane_x = map_row_max_cells / map_plane_original_x_size;
        //float map_plane_z = map_Collections.Count / map_plane_original_z_size;
        //map_plane.transform.localScale = new Vector3(map_plane_x, 1, map_plane_z);

        /*在Plane上放Cube*/
        for (int i = 0; i < map_Collections.Count; i++)//Z方向是长度就是容器的大小，也就是*读入数据*有多少有效的行
        {
            GameObject atom = Resources.Load("Prefabs/Au") as GameObject;
            GameObject atomInstance = Instantiate(atom);
            atomInstance.transform.parent = container.transform;

            //GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            atomInstance.name = "sphere" + i;
            //float x = float.Parse(map_Collections[i][2]);
            //float y = float.Parse(map_Collections[i][3]);
            //float z = float.Parse(map_Collections[i][4]);
            atomInstance.transform.position = new Vector3(float.Parse(map_Collections[i][2]), float.Parse(map_Collections[i][3]), float.Parse(map_Collections[i][4]));
        }

        //Debug.Log("Script1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
