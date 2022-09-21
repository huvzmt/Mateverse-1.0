using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class cmdrun2 : MonoBehaviour
{

    public static string cmd = "lmp.exe -in in.Ag-Au1";
    //Thread t = new Thread(new ThreadStart(NewThread));
    static Thread t1 = new Thread(NewThread);
    //public enum ThreadState
    //{
    //    Running = 0,
    //    StopRequested =1,
    //    SuspendRequested = 2,
    //    background = 4,
    //    Unstarted = 8,
    //    Stopped = 16,
    //    WaitSleepJoin = 32,
    //    Suspended = 64,
    //    AbortRequested = 128,
    //    Aborted = 256,
    //}
    static bool k = true;
    bool b = true;
    int i = 0;
    bool c = false;
    // Start is called before the first frame update
    void Start()
    {

       // t1.Start();


    }

    static void NewThread()
    {
        string str=RunCMD(cmd);
        //print(str);
        print("done2");
        k = true;
        print("k " + k);
        //print(t1.ThreadState);
    }

    public void Button_start()
    {
        c=true;
    }

    public void Button_stop()
    {
        c = false;
    }

    //public static string RunCMD(string command)
    //{
    //    Process p = new Process();
    //    p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";  //确定程序名
    //    p.StartInfo.Arguments = "/k" + command;  //指定程式命令行
    //    p.StartInfo.UseShellExecute = false;   //是否使用Shell
    //    p.StartInfo.RedirectStandardInput = true;   //重定向输入
    //    p.StartInfo.RedirectStandardOutput = false;   //重定向输出
    //    p.StartInfo.RedirectStandardError = false;    //重定向输出错误
    //    p.StartInfo.CreateNoWindow = false;        //设置不显示窗口
    //    p.Start();

    //    return p.StandardOutput.ReadToEnd();     //输出流取得命令行结果
    //}

    // Update is called once per frame
    void Update()
    {
        if ((k == true)&&(b==true)&&(c==true))
        //while ((t1.ThreadState == System.Threading.ThreadState.Stopped)&&(k==true)&&(b==true))
        {

            k = false;
            i++;
            Thread t1 = new Thread(NewThread);
            print("done3");
            print("k2" + k);
            t1.Start();
            //print(t1.ThreadState);
            if(i>5)
            {
                b = false;
            }

        }
        //print("t1" + t1.ThreadState);

        //print(t1.IsAlive);
    }

    //private void Start()
    //{
    //    string path = "Ping baidu.com";

    //    StartCmd(path);
    //}

    //[MenuItem("cmd/excueDoc")]
    //public static void ProcessExcuteDoc()
    //{
    //    string path = "Ping baidu.com";

    //    StartCmd(path);

    //}


    private static string RunCMD(string command)
    {

        //Command = Command.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
        // string output = null;
        Process p = new Process();//创建进程对象
        p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";//设定需要执行的命令

        p.StartInfo.Arguments = "D:";  //指定程式命令行
        p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;  //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;   //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;          //不显示程序窗口
        p.Start();//启动程序


        //向cmd窗口写入命令
        p.StandardInput.WriteLine(@"cd D:\project data\lammps\test-new");
        p.StandardInput.WriteLine(cmd);
        p.StandardInput.WriteLine("exit");
        //p.StandardInput.AutoFlush = true;

        //获取cmd窗口的输出信息
        //output = p.StandardOutput.ReadToEnd();
        
        p.WaitForExit();//等待程序执行完退出进程
        print("done1");
        p.Close();
        return "return";
        ///return p.StandardOutput.ReadToEnd();     //输出流取得命令行结果
    }

    private static void CmdExcute()
    {
        Process.Start(@"C:\Windows\system32\cmd.exe", "c:");
        Process.Start(@"C:\Windows\system32\cmd.exe", "ping baidu.com");
    }

    private static void SimpleExcute()
    {
        Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", "https://www.cnblogs.com/yangxiaohang");
    }



}
