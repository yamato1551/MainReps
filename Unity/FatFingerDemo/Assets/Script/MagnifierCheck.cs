using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MagnifierCheck : MonoBehaviour
{
    public bool Checkflag=true;
    GameObject cimage;
    Text text;
    void Start()
    {
        text = GameObject.Find("Canvas/LensOnOff/OnOffText").GetComponent<Text>();
        cimage = GameObject.Find("Canvas/LensOnOff/checkfield/checkimage");
    }

    // Update is called once per frame
    void Update()
    {
        var buttonpos = this.gameObject.transform.position;
        var buttonsize = this.gameObject.GetComponent<RectTransform>().sizeDelta;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchpos = touch.position;

            if (buttonpos.x - (buttonsize.x / 2) < touchpos.x
           && buttonpos.x + (buttonsize.x / 2) > touchpos.x
           && buttonpos.y - (buttonsize.y / 2) < touchpos.y
           && buttonpos.y + (buttonsize.y / 2) > touchpos.y)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    if (Checkflag)
                    {
                        Checkflag = false;
                    }
                    else
                    {
                        Checkflag = true;
                    }
                }
            }
        }
        if (Checkflag)
        {
            cimage.SetActive(true);
            text.text = "ON";
        }
        else
        {
            cimage.SetActive(false);
            text.text = "OFF";
        }
    }
}
