using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indexer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Stats stats = new Stats();
        for (int i = 0; i < 10; i++)
        {
            stats[i] = 10000;
            print(i+ " : " + stats[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Stats
    {
        int hp;
        int attack;

        // インデクサ
        public int this[int index]
        {
            get
            {
                if (0 == index) return hp;
                if (1 == index) return attack;
                else return 0;
            }
            set
            {
                if (0 == index)
                {
                    if (9999 < value) value = 9999;
                    //print(value);
                    hp = value;
                }
            }
        }
    }
}
