using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets
{
    /// <summary>
    /// 添加VR模块1.SteamVR_TrackedController
    /// 以下代码负责发射物体
    /// </summary>
    public class SteamVR_TrackedController_Shoot : MonoBehaviour
    {
        SteamVR_TrackedController stc;    //控制器对象
        void Start()
        {
            print("SteamVR_TrackedController_Shoot");
            stc = GetComponent<SteamVR_TrackedController>();    //得到手柄控制器的对象
            stc.TriggerClicked += OnTriggerClicked;    //响应手柄扣动事件

        }
        void OnTriggerClicked(object sender, ClickedEventArgs e)
        //用来响应扳机扣动事件的行为
        {
            GameObject testObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //创造一个原始的圆形模块(CreatePrimitive创造原始)(PrimitiveType原始模型)
            testObject.transform.position = transform.position;
            //创造模型的位置就是手柄的位置
            testObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //将原始模块按0.1比例进行缩放
          //  testObject.AddComponent<Rigidbody>().AddForce(transform.forward * 1000);
            //将这个模块增加刚体组件(AddComponent)并增加力(AddForce)
            testObject.tag = "Can Cach";
            //增加标签 这个标签设定的是可以拾取的物体
            testObject.AddComponent<Rigidbody>().useGravity = false;
        }

    }
}