using UnityEngine;
using System.Collections;


public class background_rotate : MonoBehaviour
{
    public Transform firstPosition;
    public Transform secondPosition;
    public float rotationDuration = 3;

    int speed = 1;

    void Start()
    {
      //  transform.rotation = firstPosition.rotation;
      //  StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        var deltaTime = 0.0f;
        var distance = firstPosition.rotation.eulerAngles - secondPosition.rotation.eulerAngles;



        while (deltaTime < rotationDuration)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation = firstPosition.rotation.eulerAngles + deltaTime / rotationDuration * distance;
            transform.rotation = Quaternion.Euler(rotation);
            deltaTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = secondPosition.rotation;
    }

    void Update()
    {
   //     this.transform.Rotate(Vector3.up * speed / 20);    
    }
}


