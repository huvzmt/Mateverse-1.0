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
    private FileSystemWatcher watcher1 = new FileSystemWatcher();
    private FileSystemWatcher watcher2 = new FileSystemWatcher();
    public Slider targetSliderObject1;
    public Slider targetSliderObject2;
    public static string pycmd = "python change_ff.py";
    //public static string pltcmd = "python vacf_read.py";
    public static string pltcmd = "python dens_plt.py";
    public static string lmpcmd = "lmp.exe -sf omp -in in.atomic";

    public static string slpcmd = "ping 127.0.0.1 -n 0.5 >nul";                 // cmd commands

    public bool runpy = false;
    public bool runlmp = false;                                                 // flags for running python scripts

    public static float arg1 = 0.18520F;
    public static float arg2 = 3.1589F;                                         // the default values

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
        watcher1.BeginInit();
        watcher1.IncludeSubdirectories = true;
        watcher1.Path = Application.dataPath;
        watcher1.Filter = "*.txt";
        watcher1.Changed += new FileSystemEventHandler(OnChanged);
        watcher1.EndInit();
        watcher1.EnableRaisingEvents = true;

        watcher2.BeginInit();
        watcher2.IncludeSubdirectories = true;

        //watcher2.Path = @"D:\project_data\lammps\water\TIP4P-2005\T273K";
        watcher2.Path = @"D:\project_data\lammps\water_single"; 

        watcher2.Filter = "forcefield.TIP4P-2005";
        watcher2.Changed += new FileSystemEventHandler(OnChanged);
        watcher2.EndInit();
        watcher2.EnableRaisingEvents = true;

    }
    public void reset_parameters()
    {
        arg1 = 0.18520F;
        arg2 = 3.1589F;
    }

    public void NewPyThread()
    {
        //read_CMD.run_checker = true;

        Process p = new Process();
        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = @"D:";                  //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;

        //reader = p.StandardOutput;

        p.Start();//启动程序
        //cmd_running = true;

        p.StandardInput.WriteLine(@"D:");
        p.StandardInput.WriteLine(@"cd D:\project_data\lammps\water_single\hbond_fromServer");

        p.StandardInput.WriteLine("python ./change_ff_hybridoverlay.py ./in.bulk "
            + "lj/cut/coul/long+" + ratio_tip3p.cutoff + "="
            + "1-1~" + tip_coeffs.arg0 + "-" + tip_coeffs.arg1 + "_"
            + "1-2~" + tip_coeffs.arg2 + "-" + tip_coeffs.arg3 + "_"
            + "2-2~" + tip_coeffs.arg4 + "-" + tip_coeffs.arg5
            + " "
            + "hbond/dreiding/lj+" + ratio_hb.hbond_order + "+" + hb_short.arg + "+" + hb_long.arg + "+" + hb_angle.arg + "="
            + "2-2~" + hb_epsilon.arg + "-" + hb_sigma.arg
            );
        p.StandardInput.WriteLine("python ./change_ff_charge.py ./in.bulk " + "1_" + charge1.arg + "  " + "2_" + (-2 * charge1.arg));

        print("Force field modified.");

        p.StandardInput.WriteLine("mpiexec -n 8 lmp.exe -in in.bulk");
        //p.StandardInput.WriteLine("lmp.exe -in in.bulk");
        print("LAMMPS runned.");

        Thread.Sleep(10*1000);
        // Since this process won't exit itslef,
        // I had to wait for a proper period of time and kill it myself.
        // This time should be altered according to the time steps read from the log.
        // The true time can actually be approximated.
        // This can be further optimized.

        //System.Collections.ArrayList proc_list = new System.Collections.ArrayList();
        //foreach (System.Diagnostics.Process this_proc in System.Diagnostics.Process.GetProcesses())
        //{
        //    //print(this_proc.ProcessName);
        //    if (this_proc.ProcessName == "cmd")
        //    {
        //        if (!this_proc.CloseMainWindow())
        //        {
        //            this_proc.Kill();
        //            print("CMD killed");
        //        }
        //    }
        //}

        lammps_finished = true;
        read_density.dens_updated = false;
        read_diffuseConst.D_updated = false;

        //p.WaitForExit();        //等待程序执行完退出进程
        //p.Close();

        //cmd_running = false;

        //cmd_output = p.StandardOutput.ReadToEnd();
        //GameObject.Find("ERROR").GetComponent<Text>().text = cmd_output;

        //if (!errorReading())
        //{
        //    //RunPlt();
        //    print("Figure updated.");
        //}

        isupdate = false;
        visualization_frame = 0;

        map_row_string = File.ReadAllLines(@"D:\project_data\lammps\water_single\hbond_fromServer\test.lammpstrj");
        atomnum = int.Parse(map_row_string[3]);                                 // number of atoms
        filelen = map_row_string.Length;                                        // the length of the file
        map_row_max_cells = 5;                                                  // 这个二维表中，最大列数，也就是在一行中最多有个单元格
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
    private void OnChanged(object source, FileSystemEventArgs e)
    {
        if (e.FullPath == Application.dataPath + "/S1.txt" || e.FullPath == Application.dataPath + "/S2.txt")
        {
            watcher1.EnableRaisingEvents = false;
            //RunPy(pycmd);
            print("li chang gai");
            runpy = true;
            watcher1.EnableRaisingEvents = true;
        }

        if (e.FullPath == @"D:\project_data\lammps\water_single\water.DENS")
        {
            watcher2.EnableRaisingEvents = false;
            runlmp = true;
            //RunCmd(lmpcmd);
            watcher2.EnableRaisingEvents = true;
        }

    }
    public static void RunPy(string cmd)
    {
        Process p = new Process();
        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = "D:";                   //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;             //不显示程序窗口
        p.Start();//启动程序

        //向cmd窗口写入命令
        p.StandardInput.WriteLine(@"D:");

        //p.StandardInput.WriteLine(@"cd D:\project_data\lammps\water\TIP4P-2005\T273K");
        p.StandardInput.WriteLine(@"cd D:\project_data\lammps\water_single");

        p.StandardInput.WriteLine(cmd);
        p.StandardInput.WriteLine("exit");

        p.WaitForExit();//等待程序执行完退出进程
        p.Close();
        return;
    }
    public static void RunCmd(string cmd)
    {
        Process q = new Process();
        q.StartInfo.FileName = CmdPath;
        q.StartInfo.Arguments = "D:";                   //指定程式命令行
        q.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        q.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        q.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        q.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        q.StartInfo.CreateNoWindow = false;             //不显示程序窗口
        q.Start();//启动程序


        //向cmd窗口写入命令
        q.StandardInput.WriteLine(@"D:");
        //q.StandardInput.WriteLine(@"cd D:\project_data\lammps\water\TIP4P-2005\T273K");
        q.StandardInput.WriteLine(@"cd D:\project_data\lammps\water_single");
        q.StandardInput.WriteLine(cmd);
        q.StandardInput.WriteLine("exit");

        q.WaitForExit();//等待程序执行完退出进程
        q.Close();
        return;
    }
    public static void RunPlt()
    {
        Process p = new Process();
        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = "D:";                   //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;             //不显示程序窗口
        p.Start();//启动程序


        //向cmd窗口写入命令
        p.StandardInput.WriteLine(@"D:");
        p.StandardInput.WriteLine(@"cd D:\project_data\lammps\water_single");
        p.StandardInput.WriteLine(pltcmd);
        p.StandardInput.WriteLine("exit");

        p.WaitForExit();//等待程序执行完退出进程
        p.Close();

        //GameObject.Find("Image").GetComponent<SpriteRenderer>().sprite = Resources.Load("D:\\project_data\\lammps\\water_single\\vdos.png", typeof(Sprite)) as Sprite;

        /*WWW www = new WWW("file://D:\\project_data\\lammps\\water_single\\vdos.png");

        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Texture2D t2d = www.texture;
        }*/

        FileStream filestream = new FileStream("D:\\project_data\\lammps\\water_single\\dens.png", FileMode.Open, FileAccess.Read);
        filestream.Seek(0, SeekOrigin.Begin);

        byte[] bytes = new byte[filestream.Length];

        filestream.Read(bytes, 0, (int)filestream.Length);

        filestream.Close();
        filestream.Dispose();
        filestream = null;

        Texture2D t2d = new Texture2D(800, 600);
        t2d.LoadImage(bytes);

        Sprite sprite = Sprite.Create(t2d,
            new Rect(0, 0, t2d.width, t2d.height),
            new Vector2(0.5f, 0.5f));

        GameObject.Find("vdos_plot").GetComponent<Image>().sprite = sprite;

        return;
    }
    public void OnSliderValueChange(Slider EventSender)
    {
        if (EventSender.name == "Slider (1)")
        {
            AddTxtTextByFileInfo(targetSliderObject1.value.ToString(), "S1");
        }
        if (EventSender.name == "Slider (2)")
        {
            AddTxtTextByFileInfo(targetSliderObject2.value.ToString(), "S2");
        }
        //AddTxtTextByFileInfo(targetSliderObject.value.ToString());
    }
    public void AddTxtTextByFileInfo(string txtText, string prefix)
    {
        string path = Application.dataPath + "/" + prefix + ".txt";
        StreamWriter sw;
        FileInfo fi = new FileInfo(path);

        sw = fi.CreateText();

        sw.WriteLine(txtText);
        sw.Close();
        sw.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        if ((runpy == true)&&(runlmp==true))
        {
            print("Modifying ff.");
            Thread t1 = new Thread(() => NewPyThread());
            t1.Start();
            //GameObject.Find("CMD_out").BroadcastMessage("run");

            runpy = false;
            runlmp = false;
            OnSliderValueChange(targetSliderObject1);
            OnSliderValueChange(targetSliderObject2);
        };
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
            atomInstance.transform.position = new Vector3(25 * float.Parse(map_Collections[i][2]), 25 * float.Parse(map_Collections[i][3]), 25 * float.Parse(map_Collections[i][4]));
            //print("atom repositioned");

            float element_type = float.Parse(map_Collections[i][1]);
            if (element_type == 1)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.blue;
                atomInstance.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            } else if (element_type == 2)
            {
                atomInstance.GetComponent<Renderer>().material.color = Color.red;
            }
            //atom.GetComponent<Text>().text = map_Collections[i][5];
        }
    }


    public void MoveMolecule()
    {
        string[] atoms_string = File.ReadAllLines(@"D:\project_data\lammps\water_single\hbond_fromServer\data.1014");
        string tmp_str = atoms_string[2];
        string[] tmp_strs = tmp_str.Split(' ');
        int auto_num = int.Parse(tmp_strs[0]);
       // filelen = atoms_string.Length;                                          // the length of the file

        GameObject container = GameObject.Find("container");                    // 找到模型存放容器
        //GameObject[] kids = new GameObject[container.transform.childCount];
        print(auto_num);

        int start_line = 0;
        while (true)
        {
            tmp_str = atoms_string[start_line];
            tmp_strs = tmp_str.Split(' ');
         //   print(tmp_strs.GetLength(0));
            if (tmp_strs.GetLength(0) == 10)
            {
                break;
            }
            print(tmp_strs.GetLength(0));
            ++start_line;
        }

        if (container.transform.childCount == 0)                                //空盒，表示要读分子，并修改
        {
            print("The box is empty");
          //  List<string> map_row;
            List<List<string>> atom_Collections = new List<List<string>>();

            for (int i = start_line; i < start_line + auto_num; ++i) {
                string[] atom_info = atoms_string[i].Split(' ');
                List<string> atom_info_list = new List<string>(atom_info);
                atom_Collections.Add(atom_info_list);//整理好，atom_Collections
            }
            print("read atoms finished!");

            GameObject atom = Resources.Load("Prefabs/O") as GameObject;
            for (int i = 0; i < atom_Collections.Count; ++i)//Z方向是长度就是容器的大小，也就是*读入数据*有多少有效的行
            {
                GameObject atomInstance = Instantiate(atom);
                //print("before bug?");
                atomInstance.transform.parent = container.transform;
                //print("after bug?");

                int elem_id = i + 1;
                atomInstance.name = "sphere " + elem_id;
                print("atom name: " + atomInstance.name);
                print("atom x: " + atom_Collections[i][4] + "atom y: " + atom_Collections[i][5] + "atom z: " + atom_Collections[i][6]);

                atomInstance.transform.position = new Vector3(25 * float.Parse(atom_Collections[i][4]),
                    25 * float.Parse(atom_Collections[i][5]), 25 * float.Parse(atom_Collections[i][6]));
                print("atom repositioned");

                float element_type = float.Parse(atom_Collections[i][2]);
                if (element_type == 1)
                {
                    atomInstance.GetComponent<Renderer>().material.color = Color.blue;
                    atomInstance.transform.localScale = new Vector3(8.0f, 8.0f, 8.0f);
                }
                else if (element_type == 2)
                {
                    atomInstance.GetComponent<Renderer>().material.color = Color.red;
                    atomInstance.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
                }

                //增加可被抓取的属性与操作
             //   atomInstance.AddComponent<SphereCollider>();       //添加碰撞器
             //   atomInstance.AddComponent<Rigidbody>();         //添加刚体属性
                atomInstance.AddComponent<move_element>();
                atomInstance.GetComponent<move_element>().isGrabbable = true;   //可抓取
                atomInstance.GetComponent<move_element>().highlightOnTouch = true;
                atomInstance.GetComponent<move_element>().touchHighlightColor = Color.yellow;    //触碰时高亮绿色
                atomInstance.GetComponent<move_element>().allowedTouchControllers = VRTK_InteractableObject.AllowedController.Both; //所有手柄可触碰
                atomInstance.GetComponent<move_element>().allowedGrabControllers = VRTK_InteractableObject.AllowedController.Both;  //所有手柄可抓取
                atomInstance.GetComponent<move_element>().allowedUseControllers = VRTK_InteractableObject.AllowedController.Both;
                atomInstance.GetComponent<move_element>().isDroppable = true;  //放下后不坠落
                atomInstance.GetComponent<move_element>().isSwappable = true;
                atomInstance.GetComponent<move_element>().stayGrabbedOnTeleport = true;
                atomInstance.GetComponent<move_element>().holdButtonToGrab = true; //点击后抓取，点击后放下
                atomInstance.GetComponent<move_element>().holdButtonToUse = true;
                atomInstance.GetComponent<move_element>().throwMultiplier = 1;
                atomInstance.GetComponent<move_element>().isUsable = true;
                atomInstance.GetComponent<move_element>().pointerActivatesUseAction = true;
                atomInstance.GetComponent<move_element>().grabOverrideButton = VRTK_ControllerEvents.ButtonAlias.Trigger_Press;
                atomInstance.GetComponent<SteamVR_LaserPointer>();
                atomInstance.tag = "Can Cach";
            }
        }
        else {                                                  //非空盒，表示已读完分子并修改完成，保存修改并删除盒子内容
            print("The box is not empty");
            UpdateDataFile();
        }
    }


    private void UpdateDataFile() 
    {
        string cmd_py = "";

        GameObject container = GameObject.Find("container");                    // 找到模型存放容器
        GameObject[] kids = new GameObject[container.transform.childCount];
        float elem_x, elem_y, elem_z;

        int elem_id = 0;
        foreach (Transform kid in container.transform)
        {
            kids[elem_id] = kid.gameObject;
            elem_id += 1;
        }
        elem_id = 0;
        foreach (GameObject kid in kids)
        {
            ++elem_id;
            elem_x = kid.gameObject.transform.position.x / 25;
            elem_y = kid.gameObject.transform.position.y / 25;
            elem_z = kid.gameObject.transform.position.z / 25;
            cmd_py += elem_id + " " + elem_x + " " + elem_y + " " + elem_z + "\n";
            print(kid.gameObject.transform.name + " " + elem_x + " " + elem_y + " " + elem_z);
            DestroyImmediate(kid.gameObject);
        }

        FileStream fs = new FileStream("D:/project_data/lammps/water_single/hbond_fromServer/data_new.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        sw.Write(cmd_py);
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();

        // 2.作为参数，调用python改写data中对应的分子坐标
        /*Process p = new Process();
        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = @"D:";                  //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;

        p.Start();//启动程序
        p.StandardInput.WriteLine(@"D:");
        p.StandardInput.WriteLine(@"cd D:\project_data\lammps\water_single\hbond_fromServer");
        p.StandardInput.WriteLine("python ./change_data_molecule.py ./data_new.txt ./data.1014");*/

       // Thread.Sleep(500);
    }

    int add_new_atom_id = 376;
    public void CreateNewAtom(int element_type)
    {
        GameObject container = GameObject.Find("container");                    // 找到模型存放容器
        GameObject atom = Resources.Load("Prefabs/O") as GameObject;
        GameObject atomInstance = Instantiate(atom);
        
        atomInstance.transform.parent = container.transform; 
        atomInstance.name = "sphere " + add_new_atom_id;
        ++add_new_atom_id;
        print("atom name: " + atomInstance.name);
        atomInstance.transform.position = VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position;
        print("atom positioned");
        
        if (element_type == 1)
        {
            atomInstance.GetComponent<Renderer>().material.color = Color.blue;
            atomInstance.transform.localScale = new Vector3(8.0f, 8.0f, 8.0f);
        }
        else if (element_type == 2)
        {
            atomInstance.GetComponent<Renderer>().material.color = Color.red;
            atomInstance.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
        }

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

        print(VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position.x);
        print(VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position.y);
        print(VRTK.VRTK_DeviceFinder.GetControllerRightHand().transform.position.z);
        print("test");

    }
}
