using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveandDestroy : MonoBehaviour
{
    float acceleration=1;
    void Update()
    {

        acceleration = acceleration * 1.05f;
        this.gameObject.transform.Translate(0, acceleration, 0);
        Destroy(this.gameObject, 3);
    }
}
