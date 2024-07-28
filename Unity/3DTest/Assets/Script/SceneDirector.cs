using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDirector : MonoBehaviour
{
    [SerializeField] EnemyDatas enemyDatas;
    // Start is called before the first frame update
    void Start()
    {
        print(enemyDatas.Datas[0].Name);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
