using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Assets;

public class Read_1559 : MonoBehaviour
{
    public static string[] map_row_string;
    public static int atomnum;
    public static int filelen;
    public static int map_row_max_cells;
    public static float scale = 1.0f;

    public void visualization1()
    {
        map_row_string = File.ReadAllLines(Application.dataPath + "/hbond_fromServer/ice/1559_big(2).data");
        List<List<string>> map_Collections = new List<List<string>>();
        string[] row1 = map_row_string[1].Split(' ');
        atomnum = int.Parse(row1[0]);                                 // number of atoms
        filelen = map_row_string.Length;                                        // the length of the file
        map_row_max_cells = 7;
        GameObject container = GameObject.Find("container");
        GameObject[] kids = new GameObject[container.transform.childCount];

        //int j = 0;
        //foreach (Transform kid in container.transform)
        //{
        //    kids[j] = kid.gameObject;
        //    j += 1;
        //}

        //foreach (GameObject kid in kids)
        //{
        //    DestroyImmediate(kid.gameObject);
        //}                                                                   // clean up the container

        //map_Collections.Clear();

        for (int i = 18; i < 18 + atomnum; i++)       //读取每一行的数据
        {

            List<string> map_row = new List<string>(map_row_string[i].Split(' '));//按空格分割每个一个单元格
            if (map_row_max_cells < map_row.Count)
            {//求一行中最多有个单元格
                map_row_max_cells = map_row.Count;
            }
            map_Collections.Add(map_row);//整理好，放到容器map_Collections中


        }


        for (int i = 0; i < map_Collections.Count; i++)//Z方向是长度就是容器的大小，也就是*读入数据*有多少有效的行
        {
            GameObject atom = Resources.Load("Prefabs/Au") as GameObject;
            GameObject atomInstance = Instantiate(atom,GameObject.Find("bond_container").transform);

            //atomInstance.transform.parent = container.transform;
            //atomInstance.transform.SetParent(container.transform);

            int element_type = int.Parse(map_Collections[i][2]);

            atomInstance.name = "sphere " + i + " " + map_Collections[i][2];
            //print("atom named with number " + i);
            atomInstance.transform.position = new Vector3(float.Parse(map_Collections[i][4])/1.5f-22f, float.Parse(map_Collections[i][5])/1.5f +2f , float.Parse(map_Collections[i][6])/1.5f);
            //print("atom repositioned");
            
            if (element_type == 2)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.white;
                atomInstance.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (element_type == 1)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.red;
                atomInstance.transform.localScale = new Vector3(0.64f, 0.64f, 0.64f);
            }

            //atomInstance.transform.SetParent(container.transform);
            //增加可被抓取的属性与操作
            //   atomInstance.AddComponent<SphereCollider>();       //添加碰撞器
            //   atomInstance.AddComponent<Rigidbody>();         //添加刚体属性
            atomInstance.AddComponent<move_element>();
            atomInstance.GetComponent<move_element>().isGrabbable = true;   //可抓取
            atomInstance.GetComponent<move_element>().highlightOnTouch = true;
            atomInstance.GetComponent<move_element>().touchHighlightColor = Color.yellow;    //触碰时高亮绿色
            atomInstance.GetComponent<move_element>().allowedTouchControllers = VRTK.VRTK_InteractableObject.AllowedController.Both; //所有手柄可触碰
            atomInstance.GetComponent<move_element>().allowedGrabControllers = VRTK.VRTK_InteractableObject.AllowedController.Both;  //所有手柄可抓取
            atomInstance.GetComponent<move_element>().allowedUseControllers = VRTK.VRTK_InteractableObject.AllowedController.Both;
            atomInstance.GetComponent<move_element>().isDroppable = true;  //放下后不坠落
            atomInstance.GetComponent<move_element>().isSwappable = true;
            atomInstance.GetComponent<move_element>().stayGrabbedOnTeleport = true;
            atomInstance.GetComponent<move_element>().holdButtonToGrab = true; //点击后抓取，点击后放下
            atomInstance.GetComponent<move_element>().holdButtonToUse = true;
            atomInstance.GetComponent<move_element>().throwMultiplier = 1;
            atomInstance.GetComponent<move_element>().isUsable = true;
            atomInstance.GetComponent<move_element>().pointerActivatesUseAction = true;
            atomInstance.GetComponent<move_element>().grabOverrideButton = VRTK.VRTK_ControllerEvents.ButtonAlias.Trigger_Press;
            atomInstance.GetComponent<SteamVR_LaserPointer>();
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        visualization1();
    }

    // Update is called once per frame
    void Update()
    {
        //visualization1();
    }
}

