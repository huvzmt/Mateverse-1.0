namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using System.Diagnostics;
    using System.Threading;
    using System.Text;
    using UnityEditor;

    public class uicontrol : VRTK_InteractableObject
    {
        public static string cmd = "lmp_serial.exe -in in.Ag-Au1";
        //Thread t = new Thread(new ThreadStart(NewThread));
        ///static Thread t1 = new Thread(NewThread);
        static bool k = false;
        bool b = true;
        int i = 0;
        bool control = false;
        // Start is called before the first frame update


        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            //if(control ==false)
                k = !k;
                control = true;
            //else
            //{
            //    k = !k;
            //    control = false;
            //}
            print(k);
            //print(usingObject.name);
        }

        public override void StopUsing(GameObject usingObject)
        {
            base.StopUsing(usingObject);
                k = !k;
                control = false;
            print(k);
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if ((k == true) && (b == true)&&(control==true))
            //while ((t1.ThreadState == System.Threading.ThreadState.Stopped)&&(k==true)&&(b==true))
            {
                k = false;
                i++;
                Thread t1 = new Thread(NewThread);
                print("done3");
                print("k2" + k);
                t1.Start();
                //print(t1.ThreadState);
                if (i > 5)
                {
                    b = false;
                }
            }
            //print("t1" + t1.ThreadState);

            //print(t1.IsAlive);

            //this.transform.Rotate(new Vector3(360 * Time.deltaTime, 0f, 0f));
        }


        //void Start()
        //{
        //    t1.Start();
        //}

        static void NewThread()
        {
            string str = RunCMD(cmd);
            //print(str);
            print("done2");
            k = true;
            print("k " + k);
            //print(t1.ThreadState);
        }

        //void Update()
        //{
        //    if ((k == true) && (b == true))
        //    //while ((t1.ThreadState == System.Threading.ThreadState.Stopped)&&(k==true)&&(b==true))
        //    {
        //        k = false;
        //        i++;
        //        Thread t1 = new Thread(NewThread);
        //        print("done3");
        //        print("k2" + k);
        //        t1.Start();
        //        //print(t1.ThreadState);
        //        if (i > 5)
        //        {
        //            b = false;
        //        }
        //    }
        //    //print("t1" + t1.ThreadState);

        //    //print(t1.IsAlive);
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

            p.WaitForExit();//等待程序执行完退出进程
            print("done1");
            p.Close();
            return "return";
            ///return p.StandardOutput.ReadToEnd();     //输出流取得命令行结果
        }
    }
}