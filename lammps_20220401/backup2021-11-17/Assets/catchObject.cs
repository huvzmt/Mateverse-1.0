using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class catchObject : MonoBehaviour
    {

        SteamVR_LaserPointer slp;           //射线对象
        SteamVR_TrackedController stc;      //控制器对象
        GameObject target = null;           //指向可以拾取的物体
        void Start()
        {
            print("catchObject");
            slp = GetComponent<SteamVR_LaserPointer>();    //得到射线对象
            slp.PointerIn += OnpointerIn;    //响应射线的进入事件
            slp.PointerOut += OnpointerOut;    //响应射线的离开事件
            stc = GetComponent<SteamVR_TrackedController>();    //得到手柄控制器的对象
            stc.TriggerClicked += OnTriggerClicked;    //响应手柄扣动事件
            stc.TriggerUnclicked += OnTriggerUnclicked;    //响应手柄松开事件
        }
        void Update()
        {

        }
        void OnpointerIn(object sender, PointerEventArgs e) //射线进入事件
        {
            GameObject obj = e.target.gameObject;//得到指向的物体
            if (obj.tag.Equals("Can Cach")) //如果我们选择的物体他的标签是Can Cach
            {
                target = obj;  //用全局变量记录这个物体
            }
        }
        void OnpointerOut(object sender, PointerEventArgs e)//射线离开事件
        {
            if (target != null)  //如果是在能拾取的物体上离开
            {
                target = null;  //不再记录这个物体了
            }
        }
        void OnTriggerClicked(object sender, ClickedEventArgs e)//用来响应扳机扣动事件的行为
        {
            if (target != null)  //如果拾取到了东西
            {
                Rigidbody r = target.GetComponent<Rigidbody>();    //如果物体有刚体就拿到这个刚体
                Destroy(r);    //销毁掉这个对象
                target.transform.position = transform.position;//这个拾取的物体位置等于手柄位置
                target.transform.parent = transform;//这个可以拾取的物体的父节点是手柄的节点
            }
        }
        void OnTriggerUnclicked(object sender, ClickedEventArgs e)//用来响应扳机松开事件的行为
        {
            if (target != null)  //如果拾取到了东西
            {
           //     target.AddComponent<Rigidbody>().AddForce(transform.forward * 500);//给被拾取的物体增加刚体跟向前的力      
                target.transform.parent = null;//不再是手柄的子物体   
            }
        }
    }
}
