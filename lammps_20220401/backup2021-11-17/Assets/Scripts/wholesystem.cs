using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;

public class wholesystem : MonoBehaviour
{
    private static string CmdPath = @"C:\Windows\System32\cmd.exe";
    private FileSystemWatcher watcher1 = new FileSystemWatcher();
    private FileSystemWatcher watcher2 = new FileSystemWatcher();

    //int atomnum = 40;
    //public static string cmd = "lmp.exe -in in.Ag-Au1";

    int atomnum = 24;
    public static string cmd = "lmp.exe -in in.bulk";


    static Thread t = new Thread(NewThread);
    //string output = "";
    bool isupdate = false;
    public bool runl = true;
    static bool rundone = false;
    bool b = true;
    bool control = false;
    int i = 0;
    Color origincolor;
    // Start is called before the first frame update
    void Start()
    {

        watcher1.BeginInit();
        watcher1.IncludeSubdirectories = true;

        //watcher1.Path = @"D:\project_data\lammps\test-new";
        //watcher1.Filter = "dump1.t_5000.cfg";


        watcher1.Path = @"D:\project_data\lammps\water\TIP4P-2005\T273K";
        watcher1.Filter = "wat.DENS";


        watcher1.Changed += new FileSystemEventHandler(OnChanged);
        watcher1.EndInit();
        watcher1.EnableRaisingEvents = true;

        watcher2.BeginInit();
        watcher2.IncludeSubdirectories = true;

        //watcher2.Path = @"D:\project_data\lammps\test-new";
        //watcher2.Filter = "data.Ag-Au1";

        watcher2.Path = @"D:\project_data\lammps\water\TIP4P-2005\T273K";
        watcher2.Filter = "wat.MSD";

        watcher2.Changed += new FileSystemEventHandler(OnChanged);
        watcher2.EndInit();
        watcher2.EnableRaisingEvents = true;

    }

    static void NewThread()
    {
        RunLmp(cmd);
        //print("donelmp");
        rundone = true;
    }

    public void Button_start()
    {
        control = true;
    }

    public void Button_stop()
    {
        control = false;
    }

    private void OnChanged(object source, FileSystemEventArgs e)
    {

        //if (e.FullPath == @"D:\project data\lammps\test-new\dump1.t_5000.cfg")
        //{
        //    watcher1.EnableRaisingEvents = false; //监控到改变事件，文件被修改了
        //    CreateModel();
        //    print("shu chu gai");
        //    isupdate = false;
        //    CreateData();
        //    watcher1.EnableRaisingEvents = true;//执行操作后，再次启动监控
        //}
        if (e.FullPath == @"D:\project data\lammps\test-new\data.Ag-Au1")
        {
            watcher2.EnableRaisingEvents = false; //监控到改变事件，文件被修改了
            runl = true;
            //RunLmp(cmd);
            print("zuo biao gai");
            watcher2.EnableRaisingEvents = true;//执行操作后，再次启动监控
        }

    }

    public void CreateModel()
    {
        //TextAsset textAsset = (TextAsset)Resources.Load("tmp1");//载入数据
        //string[] map_row_string = textAsset.text.Trim().Split('\n');//清除这个Map.csv前前后后的换行，空格之类的，并按换行符分割每一行
        string[] map_row_string = File.ReadAllLines(@"D:\project data\lammps\test-new\tmp.dump");
        int map_row_max_cells = 7;//这个二维表中，最大列数，也就是在一行中最多有个单元格
        List<List<string>> map_Collections = new List<List<string>>();//设置一个C#容器map_Collections

        GameObject container = GameObject.Find("container");//找到模型存放容器

        //foreach (Transform child in container.transform)
        //{
        //    Destroy(child.gameObject);
        //}
        //for (int i = 0; i < container.transform.childCount; i++)
        //{
        //    GameObject go = container.transform.GetChild(i).gameObject;
        //    DestroyImmediate(go);
        //}
        print(container.transform.childCount);

        //GameObject container = new GameObject("container");//设置一个容器存放模型
        //print(map_row_string.Length);

        for (int i = 9; i < atomnum + 9; i++)//读取每一行的数据
        //for (int i = 9; i < map_row_string.Length; i++)//读取每一行的数据
        {
            List<string> map_row = new List<string>(map_row_string[i].Split(' '));//按空格分割每个一个单元格
            if (map_row_max_cells < map_row.Count)
            {//求一行中最多有个单元格
                map_row_max_cells = map_row.Count;
            }
            map_Collections.Add(map_row);//整理好，放到容器map_Collections中
        }
        if (isupdate == false)
        {
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
                atom.GetComponent <Text>().text = map_Collections[i][5];
            }

            isupdate = true;
            print(container.transform.childCount);
        }

        if (isupdate == true)
        {
            for (int i = 0; i < map_Collections.Count; i++)//Z方向是长度就是容器的大小，也就是*读入数据*有多少有效的行
            {

                GameObject atom = container.transform.GetChild(i).gameObject;
                atom.transform.position = new Vector3(float.Parse(map_Collections[i][2]), float.Parse(map_Collections[i][3]), float.Parse(map_Collections[i][4]));
                atom.GetComponent<Text>().text = map_Collections[i][5];
            }
            print(container.transform.childCount);
        }


    }

    public void showack()
    {
        GameObject container = GameObject.Find("container");//找到模型存放容器
        foreach (Transform child in container.transform)
        {
            if (child.GetComponent<Text>().text == "0")
            {
                child.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.1f, 1f);
            }
            else if (child.GetComponent<Text>().text == "1")
            {
                child.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.6f, 0f, 1f);
            }
            else if (child.GetComponent<Text>().text == "2")
            {
                child.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 0.6f, 1f);
            }
            else if (child.GetComponent<Text>().text == "3")
            {
                child.GetComponent<MeshRenderer>().material.color = new Color(1f, 0.1f, 0f, 1f);
            }
            else if (child.GetComponent<Text>().text == "4")
            {
                child.GetComponent<MeshRenderer>().material.color = new Color(0f, 0.6f, 0f, 1f);
            }

        }
    }

    public void CreateData()
    {

        GameObject container = GameObject.Find("container");//找到模型存放容器
        //print(container.name);
        print(container.transform.childCount);
        atomnum = container.transform.childCount;

        File.Delete(@"D:\project data\lammps\test-new\data.Ag-Au1");
        CreateFile(@"D:\project data\lammps\test-new\data.Ag-Au1", "#test \r\n \r\n" + atomnum + " atoms \r\n1 atom types \r\n \r\n-300.0 300.0  xlo xhi \r\n-300.0 300.0  ylo yhi \r\n-300.0 300.0  zlo zhi \r\n \r\nMasses \r\n \r\n1  196.966 \r\n \r\nAtoms \r\n");

        //for (int i = 0; i < container.transform.childCount; i++)//读取每个原子的数据
        //{
        //    CreateFile("D:/data.Ag-Au1", i+1 + " " + 1 + " " + 0 + " " );
        //}
        int i = 1;
        foreach (Transform child in container.transform)
        {
            CreateFile(@"D:\project data\lammps\test-new\data.Ag-Au1", i + " " + 1 + " " + 0 + " " + child.transform.localPosition.x + " " + child.transform.localPosition.y + " " + child.transform.localPosition.z);
            i++;
        }
        //isupdate = true;
    }

    public static void RunLmp(string cmd)
    {

        //cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
        Process p = new Process();

        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = "D:";                   //指定程式命令行
        p.StartInfo.UseShellExecute = false;            //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;       //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;     //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;      //重定向标准错误输出
        p.StartInfo.CreateNoWindow = true;              //不显示程序窗口
        p.Start();                                      //启动程序


        //向cmd窗口写入命令
        p.StandardInput.WriteLine(@"cd D:\project data\lammps\test-new");
        p.StandardInput.WriteLine(cmd);
        p.StandardInput.WriteLine("exit");
        //p.StandardInput.AutoFlush = true;

        //获取cmd窗口的输出信息
        //output = p.StandardOutput.ReadToEnd();

        p.WaitForExit();//等待程序执行完退出进程
        print("done1");
        //bool rundone = true;
        p.Close();
        return;

    }

    public void CreateFile(string _filePath, string _data)
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(_filePath);
        //如果文件不存在
        if (!fi.Exists)
        {
            //创建文件
            sw = fi.CreateText();
        }
        else
        {
            //打开文件
            sw = fi.AppendText();
        }
        sw.WriteLine(_data);//以行的形式写入
                            //   sw.Write(_data);//首位衔接的方式写入
        sw.Close();//关闭流
        sw.Dispose();//销毁流
    }

    // Update is called once per frame


    // Update is called once per frame
    void Update()
    {
        if((runl==true)&&(control==true))
        {
            print("kaipao lmp");
            //RunLmp(cmd);
            Thread t = new Thread(NewThread);
            print(t.ThreadState);
            t.Start();
            runl = false;
            //if ((t.ThreadState == System.Threading.ThreadState.Stopped)&&(rundone==true))
        }
        if ((rundone == true) && (b == true)&&(control==true))
        {
            print("gai");
            rundone = false;
            //GameObject container = GameObject.Find("container");//找到模型存放容器
            CreateModel();
            print("shu chu gai");
            //isupdate = false;
            //Thread t = new Thread(NewThread);
            print(t.ThreadState);
            CreateData();
            //i++;
            ////print(t1.ThreadState);
            //if (i > 3)
            //{
            //    b = false;
            //}

        }
    }
}
