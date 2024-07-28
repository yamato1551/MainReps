using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStatus")]
public class EnemyDatas : ScriptableObject
{
    public List<EnemyStatus> Datas;

}

[System.Serializable]
public class EnemyStatus
{
    public string Name;
    public int Hp, Attack;

}
