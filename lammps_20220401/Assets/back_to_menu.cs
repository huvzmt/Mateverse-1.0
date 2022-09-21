using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back_to_menu : MonoBehaviour
{
    public void shift_scene()
    {
        SceneManager.LoadScene("main");
    }
}
