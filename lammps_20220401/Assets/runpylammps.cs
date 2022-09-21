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

public class runpylammps : MonoBehaviour
{
    private static string CmdPath = @"C:\Windows\System32\cmd.exe";

    public static string pycmd = "python change_ff.py";

    public bool runpy = false;
    public bool runlmp = false;                                                 // flags for running python scripts
    public static bool isupdate = false;

    public static int visualization_frame = 1;                                  // the frame to show

    public static string[] map_row_string;
    public static int atomnum;
    public static int filelen;
    public static int map_row_max_cells;
    public static List<List<string>> map_Collections = new List<List<string>>();

    public static bool lammps_finished = false;

    // Start is called before the first frame update
    void Start()
    {

    }

   
    public void init_data()
    {
        GameObject.Find("Density").GetComponent<Text>().text = "-";
        GameObject.Find("Diffusion").GetComponent<Text>().text = "-";
        GameObject.Find("Dielectric").GetComponent<Text>().text = "-";
        GameObject.Find("Viscosity").GetComponent<Text>().text = "-";
    }
    public void read_data()
    {
        string[] content = File.ReadAllLines(@"D:\project_data\lammps\water_single\hbond_fromServer\220826.txt");
        GameObject.Find("Density").GetComponent<Text>().text = content[0];
        GameObject.Find("Diffusion").GetComponent<Text>().text = content[1];
        GameObject.Find("Dielectric").GetComponent<Text>().text = content[2];
        GameObject.Find("Viscosity").GetComponent<Text>().text = content[3];

    }



    public void NewPyThread2(int controller)
    {
        init_data();

        string in_file = "in-" + controller.ToString() + ".bulk";
       // string result_file = "test_" + controller.ToString() + ".lammpstrj";
        string result_file = "test.lammpstrj";

        Process p = new Process();
        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = @"D:";                  //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;
        p.Start();                                      //启动程序

        p.StandardInput.WriteLine(@"D:");
        p.StandardInput.WriteLine("cd " + Application.dataPath + "/hbond_fromserver");

        p.StandardInput.WriteLine("python ./change_ff_hybridoverlay.py ./" + in_file + " "
            + "lj/cut/coul/long+" + ((float)tip_coeffs.start_values[1,0]) + "="
            + "1-1~" + ((float)tip_coeffs.start_values[1, 1]) + "-" + ((float)tip_coeffs.start_values[1, 2]) + "_"
            + "1-2~" + ((float)tip_coeffs.start_values[1, 3]) + "-" + ((float)tip_coeffs.start_values[1, 4]) + "_"
            + "2-2~" + ((float)tip_coeffs.start_values[1, 5]) + "-" + ((float)tip_coeffs.start_values[1, 6])
            + " "
            + "hbond/dreiding/lj+" + ((float)ratio_hb.hbond_order) + "+" + ((float)hb_short.arg) + "+" + ((float)hb_long.arg) + "+" + ((float)hb_angle.arg) + "="
            + "2-2~" + ((float)hb_epsilon.arg) + "-" + ((float)hb_sigma.arg)
            );
        p.StandardInput.WriteLine("python ./change_ff_charge.py ./" + in_file + " " + "1_" + ((float)charge1.arg) + "  " + "2_" + (-2 * ((float)charge1.arg)));

        print("python ./change_ff_hybridoverlay.py ./" + in_file + " "
            + "lj/cut/coul/long+" + ((float)tip_coeffs.start_values[1, 0]) + "="
            + "1-1~" + ((float)tip_coeffs.start_values[1, 1]) + "-" + ((float)tip_coeffs.start_values[1, 2]) + "_"
            + "1-2~" + ((float)tip_coeffs.start_values[1, 3]) + "-" + ((float)tip_coeffs.start_values[1, 4]) + "_"
            + "2-2~" + ((float)tip_coeffs.start_values[1, 5]) + "-" + ((float)tip_coeffs.start_values[1, 6])
            + " "
            + "hbond/dreiding/lj+" + ((float)ratio_hb.hbond_order) + "+" + ((float)hb_short.arg) + "+" + ((float)hb_long.arg) + "+" + ((float)hb_angle.arg) + "="
            + "2-2~" + ((float)hb_epsilon.arg) + "-" + ((float)hb_sigma.arg)
            );
        print("python ./change_ff_charge.py ./" + in_file + " " + "1_" + ((float)charge1.arg) + "  " + "2_" + (-2 * ((float)charge1.arg)));
        print("Force field modified.");

        p.StandardInput.WriteLine("mpiexec -n 8 lmp.exe -in " + in_file);
        print("LAMMPS runned.");
        Thread.Sleep(20 * 1000);

        lammps_finished = true;
        read_density.dens_updated = false;
        read_diffuseConst.D_updated = false;

        isupdate = false;
        visualization_frame = 0;

        map_row_string = File.ReadAllLines(Application.dataPath + "/hbond_fromserver/" + result_file);
        atomnum = int.Parse(map_row_string[3]);                                 // number of atoms
        filelen = map_row_string.Length;                                        // the length of the file
        map_row_max_cells = 5;                                                  // 这个二维表中，最大列数，也就是在一行中最多有个单元格

        read_data();
    }


    public bool errorReading()
    {
        string[] log = File.ReadAllLines(@"D:\project_data\lammps\water_single\hbond_fromServer\log.lammps");
        string row_minus2 = log[log.Length - 2];
        string row_minus1 = log[log.Length - 1];

        if (row_minus1.StartsWith("ERROR"))
        {
            GameObject.Find("ERROR").GetComponent<Text>().text = row_minus1;
        }
        else if (row_minus2.StartsWith("ERROR"))
        {
            GameObject.Find("ERROR").GetComponent<Text>().text = row_minus2;
        }
        else
        {
            return false;
        }
        return true;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void visualization()
    {

        GameObject container = GameObject.Find("container");                // 找到模型存放容器
        GameObject[] kids = new GameObject[container.transform.childCount];

        int j = 0;
        foreach(Transform kid in container.transform)
        {
            kids[j] = kid.gameObject;
            j += 1;
        }
        foreach (GameObject kid in kids)
        {
            DestroyImmediate(kid.gameObject);
        }                                                                   // clean up the container
        map_Collections.Clear();

        if ( (visualization_frame + 1)*(atomnum+9) > filelen )
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


        for ( int i = (visualization_frame)*(atomnum+9) + 9; i < (visualization_frame+1) * (atomnum + 9); i++ )       //读取每一行的数据
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
            atomInstance.transform.position = new Vector3((box_xlo + float.Parse(map_Collections[i][2]) * (box_xhi - box_xlo))/2-3f,
                                                          (box_ylo + float.Parse(map_Collections[i][3]) * (box_yhi - box_ylo))/2,
                                                          (box_zlo + float.Parse(map_Collections[i][4]) * (box_zhi - box_zlo))/2+30f);
            //print("atom repositioned");

            float element_type = float.Parse(map_Collections[i][1]);
            if (element_type == 1)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.white;
                atomInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            } else if (element_type == 2)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.red;
                atomInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            }
            //atom.GetComponent<Text>().text = map_Collections[i][5];
        }
    }


    public void UpdateDataFile()
    {
        string cmd_py = "";

        GameObject container = GameObject.Find("bond_container");                    // 找到模型存放容器
        GameObject[] kids = new GameObject[container.transform.childCount];
        float elem_x, elem_y, elem_z;
        string elem_type;

        int elem_id = 0;
        foreach (Transform kid in container.transform)
        {
            kids[elem_id] = kid.gameObject;
            elem_id += 1;
        }
        elem_id = 0;

        foreach (GameObject kid in kids)
        {
            string atom_name = kid.name;
            string[] atom_strs = atom_name.Split(' ');

            ++elem_id;
            elem_x = kid.gameObject.transform.position.x - 2f;
            elem_y = kid.gameObject.transform.position.y + 8f;
            elem_z = kid.gameObject.transform.position.z;
            elem_type = atom_strs[2];
            cmd_py += elem_id + " " + elem_type + " " + elem_x + " " + elem_y + " " + elem_z + "\n";
            print(kid.gameObject.transform.name + " " + elem_type + " " + elem_x + " " + elem_y + " " + elem_z);
            DestroyImmediate(kid.gameObject);
        }

        FileStream fs = new FileStream(Application.dataPath + "/hbond_fromServer/ice/data_new.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        sw.Write(cmd_py);
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();

        // 2.作为参数，调用python改写data中对应的分子坐标
        Process p = new Process();
        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = @"D:";                  //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;

        p.Start();//启动程序
        p.StandardInput.WriteLine(@"D:");
        p.StandardInput.WriteLine("cd " + Application.dataPath + "/hbond_fromserver/ice");
        p.StandardInput.WriteLine("python edit_data.py");

        Thread.Sleep(3000);

        crystal_opt.is_update = false;
    }

    int add_new_atom_id = 376;
    public void CreateNewAtom(int element_type)
    {
        GameObject container = GameObject.Find("container");                    // 找到模型存放容器
        GameObject atom = Resources.Load("Prefabs/Au") as GameObject;
        GameObject atomInstance = Instantiate(atom);

        atomInstance.name = "sphere " + add_new_atom_id + " " + element_type.ToString();
        //  atomInstance.transform.parent = container.transform; 
        atomInstance.transform.SetParent(container.transform, false);

        ++add_new_atom_id;
        atomInstance.transform.position = VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position;
        print("atom positioned");
        
        if (element_type == 1)
        {
       //     atomInstance.name = "sphere " + add_new_atom_id + " " + "1";
            atomInstance.GetComponent<Renderer>().material.color = Color.blue;
            atomInstance.transform.localScale = new Vector3(8.0f, 8.0f, 8.0f);
        }
        else if (element_type == 2)
        {
      //      atomInstance.name = "sphere " + add_new_atom_id + " " + "2";
            atomInstance.GetComponent<Renderer>().material.color = Color.red;
            atomInstance.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        }
        print("atom name: " + atomInstance.name);
        print("color: " + atomInstance.GetComponent<Renderer>().material.color);

        //增加可被抓取的属性与操作
        atomInstance.AddComponent<move_element>();
        atomInstance.GetComponent<move_element>().isGrabbable = true;   //可抓取
        atomInstance.GetComponent<move_element>().highlightOnTouch = true;
        atomInstance.GetComponent<move_element>().touchHighlightColor = Color.yellow;    //触碰时高亮绿色
        atomInstance.GetComponent<move_element>().allowedTouchControllers = VRTK_InteractableObject.AllowedController.Both; //所有手柄可触碰
        atomInstance.GetComponent<move_element>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.Both;  //所有手柄可抓取
        atomInstance.GetComponent<move_element>().isDroppable = true;  //放下后不坠落
        atomInstance.GetComponent<move_element>().isSwappable = true;
        atomInstance.GetComponent<move_element>().stayGrabbedOnTeleport = true;
        atomInstance.GetComponent<move_element>().holdButtonToGrab = true; //点击后抓取，点击后放下
        atomInstance.GetComponent<move_element>().holdButtonToUse = true;
        atomInstance.GetComponent<move_element>().throwMultiplier = 1;
        atomInstance.GetComponent<move_element>().isUsable = true;
        atomInstance.GetComponent<move_element>().pointerActivatesUseAction = true;
        atomInstance.GetComponent<move_element>().grabOverrideButton = VRTK_ControllerEvents.ButtonAlias.Trigger_Press;
        //  atomInstance.tag = "Can Cach";

        //print(VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position.x);
        //print(VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position.y);
        //print(VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position.z);
        //print("test");
    }
}
