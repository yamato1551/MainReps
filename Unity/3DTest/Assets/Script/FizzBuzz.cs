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
            if (i % 3==0 && i % 5 ==0)�@{
                print(i + "��3��5�Ŋ���؂�܂�");
            } else if (i % 3 == 0){
                print(i + "��3�̔{��");
            } else if (i % 5 == 0) {
                print(i + "��5�̔{��");
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
