using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using UnityEngine.EventSystems;
using VRTK;
using Assets;

public class crystal_opt : MonoBehaviour
{
    public static string[] map_row_string;
    public static int atomnum;
    public static int filelen;
    public static int map_row_max_cells;
    public static int visualization_frame;
    public static List<List<string>> map_Collections = new List<List<string>>();
    public static List<List<string>> bond_Collections = new List<List<string>>();
    public static bool is_update = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //visualization();
    }

    public void run_opt() {
        Process p = new Process();
        p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
        p.StartInfo.Arguments = @"D:";                  //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;
        p.Start();                                      //启动程序

        p.StandardInput.WriteLine(@"D:");
        p.StandardInput.WriteLine("cd " + Application.dataPath + "/hbond_fromserver/ice");

        p.StandardInput.WriteLine("lmp.exe -in lammps.in_opt");
        Thread.Sleep(15 * 1000);

        visualization_frame = 0;

        map_row_string = File.ReadAllLines(Application.dataPath + "/hbond_fromserver/ice/out.xyz");
        atomnum = int.Parse(map_row_string[3]);                                 // number of atoms
        filelen = map_row_string.Length;                                        // the length of the file
        map_row_max_cells = 5;                                                  // 这个二维表中，最大列数，也就是在一行中最多有个单元格
        is_update = true;
    }

    public void visualization()
    {

        GameObject container = GameObject.Find("bond_container");                // 找到模型存放容器
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

            atomInstance.name = "sphere " + i + " " + map_Collections[i][1];
            //print("atom named with number " + i);
            atomInstance.transform.position = new Vector3(box_xlo + float.Parse(map_Collections[i][2]) * (box_xhi - box_xlo) + 2f,
                                                          box_ylo + float.Parse(map_Collections[i][3]) * (box_yhi - box_ylo) - 8f,
                                                          box_zlo + float.Parse(map_Collections[i][4]) * (box_zhi - box_zlo));
            //print("atom repositioned");

            int element_type = int.Parse(map_Collections[i][1]);
            if (element_type == 1)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.red;
                atomInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            else if (element_type == 2)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.white;
                atomInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            //atom.GetComponent<Text>().text = map_Collections[i][5];

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


    public void visualization_bulk() {
        GameObject container = GameObject.Find("bond_container");                // 找到模型存放容器
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
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 2; y++) {
                for (int z = 0; z < 2; z++) {
                    for (int i = 0; i < map_Collections.Count; i++)//Z方向是长度就是容器的大小，也就是*读入数据*有多少有效的行
                    {
                        GameObject atom = Resources.Load("Prefabs/Au") as GameObject;
                        GameObject atomInstance = Instantiate(atom);
                        //print("before bug?");
                        atomInstance.transform.parent = container.transform;
                        //print("after bug?");

                        atomInstance.name = "sphere " + i + " " + map_Collections[i][1];
                        //print("atom named with number " + i);
                        atomInstance.transform.position = new Vector3((box_xlo + (float.Parse(map_Collections[i][2])+x) * (box_xhi - box_xlo))/1.2f - 20f,
                                                                      (box_ylo + (float.Parse(map_Collections[i][3])+y) * (box_yhi - box_ylo))/1.2f - 6f,
                                                                      (box_zlo + (float.Parse(map_Collections[i][4])+z) * (box_zhi - box_zlo))/1.2f);
                        //print("atom repositioned");

                        int element_type = int.Parse(map_Collections[i][1]);
                        if (element_type == 1)
                        {
                            atomInstance.GetComponent<Renderer>().material.color = Color.red;
                            atomInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        }
                        else if (element_type == 2)
                        {
                            atomInstance.GetComponent<Renderer>().material.color = Color.white;
                            atomInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        }
                        //atom.GetComponent<Text>().text = map_Collections[i][5];

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
            }
        }




    }


}
