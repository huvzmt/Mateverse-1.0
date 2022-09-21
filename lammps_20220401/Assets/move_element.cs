using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VRTK;

namespace Assets
{
    class move_element : VRTK_InteractableObject
    {
        private static string CmdPath = @"C:\Windows\System32\cmd.exe";

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            print(usingObject.name);
            Destroy(this.gameObject);
        }
        /*public override void Ungrabbed(GameObject previousGrabbingObject)
        {
            base.Ungrabbed(previousGrabbingObject);

            //1.获取该Object的身份信息以及新的position
            string element_name = previousGrabbingObject.name;
            string[] tmp_strs = element_name.Split(' ');
            int elem_id = int.Parse(tmp_strs[1]);
            float elem_x = previousGrabbingObject.transform.position.x;
            float elem_y = previousGrabbingObject.transform.position.y;
            float elem_z = previousGrabbingObject.transform.position.z;

            //2.作为参数，调用python改写data中对应的分子坐标
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
            p.StandardInput.WriteLine(@"cd D:\project_data\lammps\water_single\hbond_fromServer");
            p.StandardInput.WriteLine("python ./change_data_molecule.py ./data.1014 " + elem_id + " " + elem_x + " " + elem_y + " " + elem_z);
            Thread.Sleep(500);
        }*/
    }
}
