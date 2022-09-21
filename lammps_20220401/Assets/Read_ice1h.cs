using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Read_ice1h : MonoBehaviour
{
    public static string[] map_row_string;
    public static int atomnum;
    public static int filelen;
    public static int map_row_max_cells;
    public static List<List<string>> map_Collections = new List<List<string>>();
    public static List<List<string>> bond_Collections = new List<List<string>>();
    public static GameObject container;
    public static GameObject container1;
    public static GameObject[] kids;
    // Start is called before the first frame update


    public void visualization1()
    {
        map_row_string = File.ReadAllLines(Application.dataPath + "/supercell/ice1h.data");
        string[] row1 = map_row_string[1].Split(' ');
        atomnum = int.Parse(row1[0]);                                 // number of atoms
        filelen = map_row_string.Length;                                        // the length of the file
        map_row_max_cells = 7;
        container = GameObject.Find("container");
        container1 = GameObject.Find("bond_container");
        kids = new GameObject[container.transform.childCount];

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

        bond_Collections.Clear();
        map_Collections.Clear();


        for (int i = 16; i < 16 + atomnum; i++)       //读取每一行的数据
        {

            List<string> map_row = new List<string>(map_row_string[i].Split(' '));//按空格分割每个一个单元格
            if (map_row_max_cells < map_row.Count)
            {//求一行中最多有个单元格
                map_row_max_cells = map_row.Count;
            }
            map_Collections.Add(map_row);//整理好，放到容器map_Collections中


        }

        for (int i = 19 + atomnum; i < 19 + 5 * atomnum / 3; i++)
        {
            List<string> bond_row = new List<string>(map_row_string[i].Split(' '));
            bond_Collections.Add(bond_row);

        }


        for (int i = 0; i < map_Collections.Count; i++)//Z方向是长度就是容器的大小，也就是*读入数据*有多少有效的行
        {
            GameObject atom = Resources.Load("Prefabs/Au") as GameObject;
            GameObject atomInstance = Instantiate(atom);

            atomInstance.transform.parent = container.transform;

            atomInstance.name = "sphere " + i;
            //print("atom named with number " + i);
            atomInstance.transform.position = new Vector3(float.Parse(map_Collections[i][4]) - 40, float.Parse(map_Collections[i][5]), float.Parse(map_Collections[i][6]) + 40);
            //print("atom repositioned");

            float element_type = float.Parse(map_Collections[i][2]);
            if (element_type == 1)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.white;
                atomInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (element_type == 2)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.red;
                atomInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
        }

        for (int i = 0; i < bond_Collections.Count; i++)
        {

            GameObject line = new GameObject();
            LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = Resources.Load<Material>("Au");
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;
            GameObject lineInstance = Instantiate(line);
            lineInstance.transform.parent = container1.transform;
            lineInstance.name = "bond " + i;

            LineRenderer lineRenderer1 = lineInstance.GetComponent<LineRenderer>();

            int id1 = int.Parse(bond_Collections[i][2]);
            int id2 = int.Parse(bond_Collections[i][3]);
            List<Vector3> points = new List<Vector3>();
            points.Add(new Vector3(float.Parse(map_Collections[id1 - 1][4]) - 40, float.Parse(map_Collections[id1 - 1][5]), float.Parse(map_Collections[id1 - 1][6]) + 40));
            points.Add(new Vector3(float.Parse(map_Collections[id2 - 1][4]) - 40, float.Parse(map_Collections[id2 - 1][5]), float.Parse(map_Collections[id2 - 1][6]) + 40));
            lineRenderer1.SetPositions(points.ToArray());
            lineRenderer1.startWidth = 0.2f;
            lineRenderer1.endWidth = 0.2f;

        }
    }
}
