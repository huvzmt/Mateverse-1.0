using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine;

public class cmdrun : MonoBehaviour
{
    private static string CmdPath = @"C:\Windows\System32\cmd.exe";
        // Start is called before the first frame update
    void Start()
    {

        string cmd = "lmp.exe -in in.Ag-Au1";
        string output = "";
        RunCmd(cmd, out output);
    }

    public static void RunCmd(string cmd, out string output)
    {
        //cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
        Process p = new Process();
        
        p.StartInfo.FileName = CmdPath;
        p.StartInfo.Arguments = "D:";  //指定程式命令行
        p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
        p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
        p.StartInfo.RedirectStandardOutput = false;  //由调用程序获取输出信息
        p.StartInfo.RedirectStandardError = false;   //重定向标准错误输出
        p.StartInfo.CreateNoWindow = false;          //不显示程序窗口
        p.Start();//启动程序


        //向cmd窗口写入命令
        p.StandardInput.WriteLine(@"cd D:\project data\lammps\test-new\");
        p.StandardInput.WriteLine(cmd);
        p.StandardInput.AutoFlush = true;

        //获取cmd窗口的输出信息
        output = p.StandardOutput.ReadToEnd();
        print(output);
        p.WaitForExit();//等待程序执行完退出进程
        p.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
