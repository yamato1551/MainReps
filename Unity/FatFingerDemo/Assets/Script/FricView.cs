using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FricView : MonoBehaviour
{
    private RectTransform imageRect;
    private Vector3 pos;
    public int nullPos;
    [System.Serializable]
    struct RangeClass
    {
        public int min, max;
    }
    [SerializeField]
    private RangeClass posRange;

    private Vector3 beganTouch, endTouch;
    void Start()
    {
        pos.y = nullPos;
        imageRect = this.gameObject.GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {

        fric();
        pos.y = Mathf.Clamp(pos.y, posRange.min, posRange.max);
        imageRect.localPosition = new Vector3(pos.x,pos.y , pos.z);
        /*
        if (pos.y <= posRange.min)
        {
            pos.y = posRange.min;
        }
        else if (pos.y >= posRange.max)
        {
            pos.y = posRange.max;
        }
        */

    }
    public void fric()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchPos = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                beganTouch = touchPos;
                //Debug.Log("began:" + beganTouch);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouch = touchPos;
                //Debug.Log("end:" + endTouch);
            }
            if (1720 <= touchPos.y)
            {
                pos.y -= 10 ; 
                    //(touchPos.y / 0.5f) - beganTouch.y;
            }
            else if (200 >= touchPos.y)
            {
                pos.y += 10 ;
                    //beganTouch.y - (touchPos.y / 0.5f);
            }
        }
    }
}
