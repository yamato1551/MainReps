using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    int[] A;
    int an;
    // Start is called before the first frame update
    void Start()
    {
        A = new int[5] {1,4,-1,3,2};
        for(int i = 0; i < 5; i++)
        {
            an = A[i];
            Debug.Log(an);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
