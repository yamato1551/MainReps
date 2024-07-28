using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizzBuzz : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 100; i++)
        {
            if (i % 3==0 && i % 5 ==0)@{
                print(i + "‚Í3‚Æ5‚ÅŠ„‚èØ‚ê‚Ü‚·");
            } else if (i % 3 == 0){
                print(i + "‚Í3‚Ì”{”");
            } else if (i % 5 == 0) {
                print(i + "‚Í5‚Ì”{”");
            } else {
                print(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
