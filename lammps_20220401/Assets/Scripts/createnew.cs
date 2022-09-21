namespace VRTK
{
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class createnew : VRTK_InteractableObject
{
        public enum Types
        {
            H,
            He,
            Li,
            Be,
            B,
            C,
            N,
            O,
            F,
            Ne,
            Na,
            Mg,
            Al,
            Si,
            P,
            S,
            Cl,
            Ar,
            Au,
              
        }

        public Types elements;
        //public GameObject H;
        //public GameObject He;
        //public GameObject Li;
        public GameObject createObj;
        SteamVR_TrackedObject track;
        //   private Color selectedColor;

        //public void SetcreateObj(GameObject newobj)
        //{
        //    createObj = newobj;
        public override void StartUsing(GameObject usingObject)
        {
            print("create new start");
            base.StartUsing(usingObject);

            //if (elements == Types.H)
            //{
            //CreateShape(usingObject);
           // createObj = usingObject;
            CreateShape(createObj);
            print("CreateShape");
            //}
            //if (elements == Types.He)
            //{
            //    CreateShape(He);
            //}
            //if (elements == Types.Li)
            //{
            //    CreateShape(Li);
            //}

            ResetMenuItems();
            print("CreateShape");
        }

        private void CreateShape(GameObject element)
        {
                //     track = GetComponent<SteamVR_TrackedObject>();
                print(usingObject.transform.position);
                VRTK_InteractGrab grabbingController = (element.gameObject.GetComponent<VRTK_InteractGrab>() ? element.gameObject.GetComponent<VRTK_InteractGrab>() : element.gameObject.GetComponentInParent<VRTK_InteractGrab>());
                //Instantiate(element);
                //GameObject container = GameObject.Find("container");//找到模型存放容器
                //GameObject atom = Resources.Load("Prefabs/he") as GameObject;
                GameObject atomInstance = Instantiate(element);
               // atomInstance.transform.parent = container.transform;
                atomInstance.transform.position = usingObject.transform.position;
                //GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //atomInstance.name = "sphere" + i;
                grabbingController.gameObject.GetComponent<VRTK_InteractTouch>().ForceTouch(element);

                //     Instantiate(element, transform.position, Quaternion.identity);
                //       GameObject obj = GameObject.CreatePrimitive(shape);
                //     obj.transform.position = transform.position;
                //      obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        //private bool CanGrab(VRTK_InteractGrab grabbingController)
        //{
        //    return (grabbingController && grabbingController.GetGrabbedObject() == null && grabbingController.gameObject.GetComponent<VRTK_ControllerEvents>().grabPressed);
        //}

        //private bool NoArrowNotched(GameObject controller)
        //{
        //    if (VRTK_SDK_Bridge.IsControllerLeftHand(controller))
        //    {
        //        bow = VRTK_DeviceFinder.GetControllerRightHand().GetComponentInChildren<BowAim>();
        //    }
        //    else if (VRTK_SDK_Bridge.IsControllerRightHand(controller))
        //    {
        //        bow = VRTK_DeviceFinder.GetControllerLeftHand().GetComponentInChildren<BowAim>();
        //    }
        //    return (bow == null || !bow.HasArrow());
        //}

        private void ResetMenuItems()
        {
            foreach (createnew menuObjectSpawner in FindObjectsOfType<createnew>())
            {
                menuObjectSpawner.StopUsing(null);
            }
        }
    }
}
