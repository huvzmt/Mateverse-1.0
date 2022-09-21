using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using UnityEngine.EventSystems;

public class game_connect : MonoBehaviour
{
    private string current;
    private string last;
    private float target;
    private float loss_c;
    private float loss_l;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        target = float.Parse(GameObject.Find("Target_rho").GetComponent<Text>().text);
    }

    // Update is called once per frame
    void Update()
    {
        if ((runpylammps.lammps_finished == true) && (read_density.dens_updated == true) && (read_last.finished == true))
        {
            current = GameObject.Find("read_density").GetComponent<Text>().text;
            last = GameObject.Find("read_density_last").GetComponent<Text>().text;
            var match_c = Regex.Match(current, @"([-+]?[0-9]*\.?[0-9]+)");
            var match_l = Regex.Match(last, @"([-+]?[0-9]*\.?[0-9]+)");
            if (match_c.Success && match_l.Success)
            {
                loss_c = Math.Abs(Convert.ToSingle(match_c.Groups[1].Value) - target);
                loss_l = Math.Abs(Convert.ToSingle(match_l.Groups[1].Value) - target);
                damage = loss_l - loss_c;
                print("The current damage is:" + damage.ToString());
                //if (0.1f <= damage && damage < 0.2f) 
                if (damage >= 0) {
                    // Execute "平A"
                    sendToSamurai("u");
                    print("Execute 平A");
                }
                else if (0.2f <= damage && damage < 0.5f)
                {
                    // Execute "技能"
                    sendToSamurai("u u");
                    print("Execute 技能");
                }
                else if (damage >= 0.5f)
                {
                    // Execute "大招"
                    sendToSamurai("d s ds u");
                    print("Execute 大招");
                }
            }
        }
    }

    public void sendToSamurai(string key_command)
    {
        Process p = new Process();
        p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
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

        p.StandardInput.WriteLine("python send_key_combinations.py " + key_command);

        p.StandardInput.WriteLine("exit");
        p.WaitForExit();

        print(key_command + " sent to Samurai.");
    }
}
