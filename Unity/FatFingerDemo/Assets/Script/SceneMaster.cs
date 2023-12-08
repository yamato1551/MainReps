using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMaster : MonoBehaviour
{
    public static int touchTimes = 0;
    public static int buttonBumbers = 0;
    public int touchs;
    public static bool tapBaseChange=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                touchTimes++;
            }

            touchs = touchTimes;
        }
    }
}
