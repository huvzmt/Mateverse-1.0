namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class destroyatom : VRTK_InteractableObject
    {
        GameObject obj;

        // Start is called before the first frame update
        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            print(usingObject.name);
            Destroy(this.gameObject);
        }

        //public override void StopUsing(GameObject usingObject)
        //{
        //    base.StopUsing(usingObject);
        //    spinSpeed = 0f;
        //}

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            //this.transform.Rotate(new Vector3(360 * Time.deltaTime, 0f, 0f));
        }

    }
}

