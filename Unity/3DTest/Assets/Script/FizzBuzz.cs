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
            if (i % 3==0 && i % 5 ==0)　{
                print(i + "は3と5で割り切れます");
            } else if (i % 3 == 0){
                print(i + "は3の倍数");
            } else if (i % 5 == 0) {
                print(i + "は5の倍数");
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
