using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class filewatch : MonoBehaviour
{
    private FileSystemWatcher watcher1 = new FileSystemWatcher();
    private FileSystemWatcher watcher2 = new FileSystemWatcher();

    //static void Main(string[] args)
    //{
    //    System.ServiceProcess.ServiceBase.Run(new Service1());//主函数调用，启动服务
    //}

    //public Service1()
    //{
    //    InitializeComponent();
    //    this.ServiceName = "FileMonitorService";//初始化服务名
    //    this.CanPauseAndContinue = true;//服务可以暂停和继续
    //    this.CanStop = true;//服务可以被停止
    //}
    void Start()//重载start方法
    {
        //try
        //{
        //设置监控类的基础信息
        watcher1.BeginInit();
        watcher1.IncludeSubdirectories = true;
        watcher1.Path = @"D:\project data\lammps\test-new";
        watcher1.Filter = "dump1.t_20000.cfg";
        watcher1.Changed += new FileSystemEventHandler(OnChanged);
        watcher1.EndInit();
        watcher1.EnableRaisingEvents = true;

        watcher2.BeginInit();
        watcher2.IncludeSubdirectories = true;
        watcher2.Path = @"D:\project data\lammps\test-new";
        watcher2.Filter = "data.Ag-Au1";
        watcher2.Changed += new FileSystemEventHandler(OnChanged);
        watcher2.EndInit();
        watcher2.EnableRaisingEvents = true;

        //}
        //catch (System.IO.IOException ext)
        //{
        //    Console.WriteLine(ext.Message);
        //}

        print("weigai");
    }

    private void OnChanged(object source, FileSystemEventArgs e)
    {
        if (e.FullPath == @"D:\project data\lammps\test-new\dump1.t_20000.cfg")
        {
            watcher1.EnableRaisingEvents = false; //监控到改变事件，文件被修改了
            print("shu chu gai");
            watcher1.EnableRaisingEvents = true;//执行操作后，再次启动监控
        }
        if (e.FullPath == @"D:\project data\lammps\test-new\data.Ag-Au1")
        {
            watcher2.EnableRaisingEvents = false; //监控到改变事件，文件被修改了
            print("zuo biao gai");
            watcher2.EnableRaisingEvents = true;//执行操作后，再次启动监控
        }

        // Start is called before the first frame update
        //void Start()
        //{
    }
        //}

        // Update is called once per frame
        void Update()
    {
        
    }
}
