using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choose_ui : MonoBehaviour
{
    // public static int region_0 = 0;         //0-1
    // public static int region_1 = 0;         //0-1
    // public static int col_1 = 0;            //0-4
    // public static int col_2 = 0;            //0-5
    public static int time_delay = 0;

    // public static GameObject[] region0 = new GameObject[2];
    // public static GameObject[] region1 = new GameObject[2];
    // public static GameObject[] col1 = new GameObject[5];
    // public static GameObject[] col2 = new GameObject[6];

    public static GameObject[] coloum1 = new GameObject[5];
    public static GameObject[] coloum2 = new GameObject[7];

    public static int col_1 = 0, col_2 = 0, col_3 = 0, col_4 = 0;    //only 0 and 1
    public static int row_1 = 0, row_2 = 0, row_3 = 0, row_4 = 0;    //when col == 0, row is 0~4, when col == 1, row = 0~6
    int temp_row, temp_col;

    // int[,] arr = new int[2][7] { {0, 0, 0, 0, 0, 0, 0},{ 0} };
    public static int[,] mark = new int[,] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 } };
   // int[,] arr = new int[2, 7];

    void Start()
    {
        int j = 0;
        foreach (Transform kid in GameObject.Find("region0").transform)
        {
            coloum1[j] = kid.gameObject;
            j += 1;
        }
        j = 0;
        foreach (Transform kid in GameObject.Find("region1").transform)
        {
            coloum2[j] = kid.gameObject;
            j += 1;
        }
        /*j = 0;
        foreach (Transform kid in GameObject.Find("col1").transform)
        {
            col1[j] = kid.gameObject;
            j += 1;
        }
        j = 0;
        foreach (Transform kid in GameObject.Find("col2").transform)
        {
            col2[j] = kid.gameObject;
            j += 1;
        }*/

        initColors();

        print("colors initialized.");
    }

    // Update is called once per frame
    void Update()
    {
        InputJoystick_1();
        InputJoystick_2();
        InputJoystick_3();
        InputJoystick_4();

        for (int i = 0; i < 5; i++) {
            if (mark[0,i] == 0)
            {
                toGray(0, i);
            }
        }
        for (int i = 0; i < 7; i++)
        {
            if (mark[1, i] == 0)
            {
                toGray(1, i);
            }
        }

        time_delay += 1;
    }

    

    void InputJoystick_1()
    {
        temp_row = row_1;
        temp_col = col_1;

        if (mark[col_1, row_1] == 0 || mark[col_1, row_1] == 1) {
            mark[col_1, row_1] = 1;
            toGreen(col_1, row_1);
        }

        if ((Input.GetAxis("joy_right_y") == 1) && (time_delay > 50))       //down
        {
            temp_row += 1;
            if (col_1 == 0)
            {
                temp_row %= 5;
            }
            else if (col_1 == 1)
            {
                temp_row %= 7;
            }

            if (mark[col_1, temp_row] == 0)
            {
                mark[col_1, temp_row] = 1;
            }
            if (mark[col_1, row_1] == 1)
            {
                mark[col_1, row_1] = 0;
            }
          //  toGray(col_1, row_1);
            row_1 = temp_row;
            time_delay = 0;
     //       toGreen(col_1, row_1);
        }
        else if ((Input.GetAxis("joy_right_y") == -1) && (time_delay > 50))       //up
        {
            temp_row -= 1;
            if (temp_row < 0 && col_1 == 0)
            {
                temp_row += 5;
            }
            else if (temp_row < 0 && col_1 == 1)
            {
                temp_row += 7;
            }

            if (mark[col_1, temp_row] == 0)
            {
                mark[col_1, temp_row] = 1;
            }
            if (mark[col_1, row_1] == 1)
            {
                mark[col_1, row_1] = 0;
            }
         //   toGray(col_1, row_1);
            row_1 = temp_row;
            time_delay = 0;
     //       toGreen(col_1, row_1);
        }
        else if ((Input.GetAxis("joy_right_x") == 1) && (time_delay > 50))       //right
        {
            temp_col += 1;
            temp_col %= 2;

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 1;
            }
            if (mark[col_1, row_1] == 1)
            {
                mark[col_1, row_1] = 0;
            }
         //   toGray(col_1, row_1);
            col_1 = temp_col;
            row_1 = 0;
            time_delay = 0;
      //      toGreen(col_1, row_1);
        }
        else if ((Input.GetAxis("joy_right_x") == -1) && (time_delay > 50))       //left
        {
            temp_col -= 1;
            if (temp_col < 0)
            {
                temp_col += 2;
            }

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 1;
            }
            if (mark[col_1, row_1] == 1)
            {
                mark[col_1, row_1] = 0;
            }
       //     toGray(col_1, row_1);
            col_1 = temp_col;
            row_1 = 0;
            time_delay = 0;
      //      toGreen(col_1, row_1);
        }
    }

    void InputJoystick_2()
    {
        temp_row = row_2;
        temp_col = col_2;

        if (mark[col_2, row_2] == 0 || mark[col_2, row_2] == 2)
        {
            mark[col_2, row_2] = 2;
            toRed(col_2, row_2);
        }

        if ((Input.GetAxis("joy1_right_y") == 1) && (time_delay > 50))       //down
        {
            temp_row += 1;
            if (col_2 == 0)
            {
                temp_row %= 5;
            }
            else if (col_2 == 1)
            {
                temp_row %= 7;
            }

            if (mark[col_2, temp_row] == 0)
            {
                mark[col_2, temp_row] = 2;
            }
            if (mark[col_2, row_2] == 2)
            {
                mark[col_2, row_2] = 0;
            }
        //    toGray(col_2, row_2);
            row_2 = temp_row;
            time_delay = 0;
    //        toRed(col_2, row_2);
        }
        else if ((Input.GetAxis("joy1_right_y") == -1) && (time_delay > 50))       //up
        {
            temp_row -= 1;
            if (temp_row < 0 && col_2 == 0)
            {
                temp_row += 5;
            }
            else if (temp_row < 0 && col_2 == 1)
            {
                temp_row += 7;
            }

            if (mark[col_2, temp_row] == 0)
            {
                mark[col_2, temp_row] = 2;
            }
            if (mark[col_2, row_2] == 2)
            {
                mark[col_2, row_2] = 0;
            }
       //     toGray(col_2, row_2);
            row_2 = temp_row;
            time_delay = 0;
     //       toRed(col_2, row_2);
        }
        else if ((Input.GetAxis("joy1_right_x") == 1) && (time_delay > 50))       //right
        {
            temp_col += 1;
            temp_col %= 2;

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 2;
            }
            if (mark[col_2, row_2] == 2)
            {
                mark[col_2, row_2] = 0;
            }
        //    toGray(col_2, row_2);
            col_2 = temp_col;
            row_2 = 0;
            time_delay = 0;
     //       toRed(col_2, row_2);
        }
        else if ((Input.GetAxis("joy1_right_x") == -1) && (time_delay > 50))       //left
        {
            temp_col -= 1;
            if (temp_col < 0)
            {
                temp_col += 2;
            }

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 2;
            }
            if (mark[col_2, row_2] == 2)
            {
                mark[col_2, row_2] = 0;
            }
        //    toGray(col_2, row_2);
            col_2 = temp_col;
            row_2 = 0;
            time_delay = 0;
     //       toRed(col_2, row_2);
        }
    }

    void InputJoystick_3()
    {
        temp_row = row_3;
        temp_col = col_3;

        if (mark[col_3, row_3] == 0 || mark[col_3, row_3] == 3)
        {
            mark[col_3, row_3] = 3;
            toBlue(col_3, row_3);
        }

        if ((Input.GetAxis("joy2_right_y") == 1) && (time_delay > 50))       //down
        {
            temp_row += 1;
            if (col_3 == 0)
            {
                temp_row %= 5;
            }
            else if (col_3 == 1)
            {
                temp_row %= 7;
            }

            if (mark[col_3, temp_row] == 0)
            {
                mark[col_3, temp_row] = 3;
            }
            if (mark[col_3, row_3] == 3)
            {
                mark[col_3, row_3] = 0;
            }
       //     toGray(col_3, row_3);
            row_3 = temp_row;
            time_delay = 0;
     //       toBlue(col_3, row_3);
        }
        else if ((Input.GetAxis("joy2_right_y") == -1) && (time_delay > 50))       //up
        {
            temp_row -= 1;
            if (temp_row < 0 && col_3 == 0)
            {
                temp_row += 5;
            }
            else if (temp_row < 0 && col_3 == 1)
            {
                temp_row += 7;
            }

            if (mark[col_3, temp_row] == 0)
            {
                mark[col_3, temp_row] = 3;
            }
            if (mark[col_3, row_3] == 3)
            {
                mark[col_3, row_3] = 0;
            }
       //     toGray(col_3, row_3);
            row_3 = temp_row;
            time_delay = 0;
      //      toBlue(col_3, row_3);
        }
        else if ((Input.GetAxis("joy2_right_x") == 1) && (time_delay > 50))       //right
        {
            temp_col += 1;
            temp_col %= 2;

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 3;
            }
            if (mark[col_3, row_3] == 3)
            {
                mark[col_3, row_3] = 0;
            }
        //    toGray(col_3, row_3);
            col_3 = temp_col;
            row_3 = 0;
            time_delay = 0;
     //       toBlue(col_3, row_3);
        }
        else if ((Input.GetAxis("joy2_right_x") == -1) && (time_delay > 50))       //left
        {
            temp_col -= 1;
            if (temp_col < 0)
            {
                temp_col += 2;
            }

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 3;
            }
            if (mark[col_3, row_3] == 3)
            {
                mark[col_3, row_3] = 0;
            }
        //    toGray(col_3, row_3);
            col_3 = temp_col;
            row_3 = 0;
            time_delay = 0;
     //       toBlue(col_3, row_3);
        }
    }

    void InputJoystick_4()
    {
        temp_row = row_4;
        temp_col = col_4;

        if (mark[col_4, row_4] == 0 || mark[col_4, row_4] == 4)
        {
            mark[col_4, row_4] = 4;
            toYellow(col_4, row_4);
        }

        if ((Input.GetAxis("joy3_right_y") == 1) && (time_delay > 50))       //down
        {
            temp_row += 1;
            if (col_4 == 0)
            {
                temp_row %= 5;
            }
            else if (col_4 == 1)
            {
                temp_row %= 7;
            }

            if (mark[col_4, temp_row] == 0)
            {
                mark[col_4, temp_row] = 4;
            }
            if (mark[col_4, row_4] == 4)
            {
                mark[col_4, row_4] = 0;
            }
       //     toGray(col_4, row_4);
            row_4 = temp_row;
            time_delay = 0;
       //     toYellow(col_4, row_4);
        }
        else if ((Input.GetAxis("joy3_right_y") == -1) && (time_delay > 50))       //up
        {
            temp_row -= 1;
            if (temp_row < 0 && col_4 == 0)
            {
                temp_row += 5;
            }
            else if (temp_row < 0 && col_4 == 1)
            {
                temp_row += 7;
            }

            if (mark[col_4, temp_row] == 0)
            {
                mark[col_4, temp_row] = 4;
            }
            if (mark[col_4, row_4] == 4)
            {
                mark[col_4, row_4] = 0;
            }
      //      toGray(col_4, row_4);
            row_4 = temp_row;
            time_delay = 0;
     //       toYellow(col_4, row_4);
        }
        else if ((Input.GetAxis("joy3_right_x") == 1) && (time_delay > 50))       //right
        {
            temp_col += 1;
            temp_col %= 2;

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 4;
            }
            if (mark[col_4, row_4] == 4)
            {
                mark[col_4, row_4] = 0;
            }
       //     toGray(col_4, row_4);
            col_4 = temp_col;
            row_4 = 0;
            time_delay = 0;
      //      toYellow(col_4, row_4);
        }
        else if ((Input.GetAxis("joy3_right_x") == -1) && (time_delay > 50))       //left
        {
            temp_col -= 1;
            if (temp_col < 0)
            {
                temp_col += 2;
            }

            if (mark[temp_col, 0] == 0)
            {
                mark[temp_col, 0] = 4;
            }
            if (mark[col_4, row_4] == 4)
            {
                mark[col_4, row_4] = 0;
            }
       //     toGray(col_4, row_4);
            col_4 = temp_col;
            row_4 = 0;
            time_delay = 0;
       //     toYellow(col_4, row_4);
        }
    }

    /*void InputJoystick0() {
        if ((Input.GetAxis("joy_right_y") == 1) && (time_delay > 50))
        {
            // changing the region, where there are three, denoting the charges, force fields, and the coefficients.
            // THE Y AXIS INPUT IS 1 TOWARDS THE USER, AND -1 THE OTHER WAY.
            toGray(0, region_0, -1, -1);
            region_0 += 1;
            region_0 %= 2;
            time_delay = 0;
            toGreen(0, region_0, -1, -1);
            //print("Method region-1");
        }
        else if ((Input.GetAxis("joy_right_y") == -1) && (time_delay > 50))
        {
            toGray(0, region_0, -1, -1);
            region_0 -= 1;
            if (region_0 < 0) {
                region_0 += 2;
            }
            time_delay = 0;
            toGreen(0, region_0, -1, -1);
            //print("Method region+1");
        }
    }

    void InputJoystick1()
    {
        if ((Input.GetAxis("joy1_right_y") == 1) && (time_delay > 50))
        {
            // changing the region, where there are three, denoting the charges, force fields, and the coefficients.
            // THE Y AXIS INPUT IS 1 TOWARDS THE USER, AND -1 THE OTHER WAY.
            toGray(1, region_1, -1, -1);
            region_1 += 1;
            region_1 %= 2;
            time_delay = 0;
            toGreen(1, region_1, -1, -1);
            //print("Method region-1");
        }
        else if ((Input.GetAxis("joy1_right_y") == -1) && (time_delay > 50))
        {
            toGray(1, region_1, -1, -1);
            region_1 -= 1;
            if (region_1 < 0)
            {
                region_1 += 2;
            }
            time_delay = 0;
            toGreen(1, region_1, -1, -1);
            //print("Method region+1");
        }
    }

    void InputJoystick2()
    {
        if ((Input.GetAxis("joy2_right_y") == 1) && (time_delay > 50))
        {
            // changing the region, where there are three, denoting the charges, force fields, and the coefficients.
            // THE Y AXIS INPUT IS 1 TOWARDS THE USER, AND -1 THE OTHER WAY.
            toGray(-1, -1, 0, col_1);
            col_1 += 1;
            col_1 %= 5;
            time_delay = 0;
            toGreen(-1, -1, 0, col_1);
            //print("Method region-1");
        }
        else if ((Input.GetAxis("joy2_right_y") == -1) && (time_delay > 50))
        {
            toGray(-1, -1, 0, col_1);
            col_1 -= 1;
            if (col_1 < 0)
            {
                col_1 += 5;
            }
            time_delay = 0;
            toGreen(-1, -1, 0, col_1);
            //print("Method region+1");
        }
    }

    void InputJoystick3()
    {
        if ((Input.GetAxis("joy3_right_y") == 1) && (time_delay > 50))
        {
            // changing the region, where there are three, denoting the charges, force fields, and the coefficients.
            // THE Y AXIS INPUT IS 1 TOWARDS THE USER, AND -1 THE OTHER WAY.
            toGray(-1, -1, 1, col_2);
            col_2 += 1;
            col_2 %= 6;
            time_delay = 0;
            toGreen(-1, -1, 1, col_2);
            //print("Method region-1");
        }
        else if ((Input.GetAxis("joy3_right_y") == -1) && (time_delay > 50))
        {
            toGray(-1, -1, 1, col_2);
            col_2 -= 1;
            if (col_2 < 0)
            {
                col_2 += 6;
            }
            time_delay = 0;
            toGreen(-1, -1, 1, col_2);
            //print("Method region+1");
        }
    }*/


  /*  void toGray(int is_region, int region_num, int is_col, int col_num)
    {
        if (is_region == 0)
        {
            region0[region_num].GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (is_region == 1)
        {
            region1[region_num].GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (is_col == 0)
        {
            col1[col_num].GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (is_col == 1)
        {
            col2[col_num].GetComponent<Renderer>().material.color = Color.gray;
        }
    }
*/

    void toGray(int col, int row)
    {
        if (col == 0)
        {
            coloum1[row].GetComponent<Renderer>().material.color = Color.gray;
        }
        else if (col == 1) {
            coloum2[row].GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    void toGreen(int col, int row)
    {
        if (col == 0)
        {
            coloum1[row].GetComponent<Renderer>().material.color = Color.green;
            print(row);
        }
        else if (col == 1)
        {
            coloum2[row].GetComponent<Renderer>().material.color = Color.green;
        }
    }

    void toRed(int col, int row)
    {
        if (col == 0)
        {
            coloum1[row].GetComponent<Renderer>().material.color = Color.red;
        }
        else if (col == 1)
        {
            coloum2[row].GetComponent<Renderer>().material.color = Color.red;
        }
    }

    void toBlue(int col, int row)
    {
        if (col == 0)
        {
            coloum1[row].GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (col == 1)
        {
            coloum2[row].GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    void toYellow(int col, int row)
    {
        if (col == 0)
        {
            coloum1[row].GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (col == 1)
        {
            coloum2[row].GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
    /*void toGreen(int is_region, int region_num, int is_col, int col_num)
    {

        if (is_region == 0)
        {
            region0[region_num].GetComponent<Renderer>().material.color = Color.green;
        }
        else if (is_region == 1)
        {
            region1[region_num].GetComponent<Renderer>().material.color = Color.green;
        }
        else if (is_col == 0)
        {
            col1[col_num].GetComponent<Renderer>().material.color = Color.green;
        }
        else if (is_col == 1)
        {
            col2[col_num].GetComponent<Renderer>().material.color = Color.green;
        }
    }*/

    void initColors()
    {
        foreach (GameObject gameobject in coloum1)
        {
            gameobject.GetComponent<Renderer>().material.color = Color.gray;
        }
      //  toGreen(0, region_0, -1, -1);
        foreach (GameObject gameobject in coloum2)
        {
            gameobject.GetComponent<Renderer>().material.color = Color.gray;
        }
      //  toGreen(1, region_1, -1, -1);
        /*foreach (GameObject gameobject in col1)
        {
            gameobject.GetComponent<Renderer>().material.color = Color.gray;
        }
        toGreen(-1, -1, 0, col_1);
        foreach (GameObject gameobject in col2)
        {
            gameobject.GetComponent<Renderer>().material.color = Color.gray;
        }
        toGreen(-1, -1, 1, col_2);*/
    }
}
