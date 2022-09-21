using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_to_liquid : MonoBehaviour
{
    public void shift_scene()
    {
        SceneManager.LoadScene("Liquid");
    }
}
