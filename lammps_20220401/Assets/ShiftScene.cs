using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShiftScene : MonoBehaviour
{
    public void shift_scene() {
        SceneManager.LoadScene("Liquid");
    }
}
