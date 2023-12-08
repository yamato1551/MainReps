using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI3DtouchZoom : MonoBehaviour
{
    GameObject panel;
    Vector2 scale;
    bool push;
    RectTransform rect,canvas;
    Vector2 touchpos,_touchpos;
    public int BlankArea;
    public float AdjustedvValue;
    int speed;
    [System.Serializable]
    struct RangeClass
    {
        public int min, max;
    }
    [SerializeField]
    private RangeClass ScaleLimit;
    public enum MoveChange
    {
        ConstantVelocity,
        ExpansionSpeed,
        ScreenMove,
    }
    public MoveChange _movechange;
    void Start()
    {
        rect = GameObject.Find("Canvas/BackGround").GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        panel = GameObject.Find("Canvas/BackGround");
    }

    void Update()
    {
       
        touchZoom();
        touchMove();
        //UIOutSide();

    }

    void touchZoom()
    {
   
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchpos = touch.position;
            if (touch.phase == TouchPhase.Began)
            {
                rect.position = new Vector2(touchpos.x, touchpos.y);

                push = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                push = false;
                rect.localPosition = new Vector2(0,0);
            }
        }
        if (push)
        {
            if (Input.touches[0].pressure > 0)
            {
                //Debug.Log(Input.touches[0].pressure);
                scale.x = (Input.touches[0].pressure + 1);
                scale.x = Mathf.Clamp(scale.x, ScaleLimit.min, ScaleLimit.max);
                scale.y = scale.x;
            }
          
            panel.transform.localScale = new Vector2(scale.x, scale.y);
        }
        else
        {
            panel.transform.localScale = new Vector2(1, 1);
            rect.localPosition = new Vector2(0, 0);
        }
      
    }
    void touchMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchpos = touch.position;
            Vector2 localpos = touch.deltaPosition;
            //Debug.Log(touchpos);
            switch (_movechange)
            {
                case MoveChange.ScreenMove:
                    if (push)
                    {
                        if ((Screen.width * 0.5f) + BlankArea < touchpos.x)//右
                        {
                            _touchpos.x -= localpos.x*AdjustedvValue;

                        }
                        if ((Screen.width * 0.5f) - BlankArea > touchpos.x)//左
                        {
                            _touchpos.x += localpos.x*AdjustedvValue;
                        }
                        if ((Screen.height * 0.5f) + BlankArea < touchpos.y)//上
                        {
                            _touchpos.y -= localpos.y*AdjustedvValue;                            
                        }
                        if ((Screen.height * 0.5f) - BlankArea > touchpos.y)//下
                        {
                            _touchpos.y += localpos.y*AdjustedvValue;
                        }

                    }
                    break;

                case MoveChange.ExpansionSpeed:
                    if (touch.phase == TouchPhase.Began)
                    {
                        _touchpos = touchpos;
                    }
                    if (localpos.x < 0)
                    {
                        localpos.x *= -1;
                    }
                    if (localpos.y < 0)
                    {
                        localpos.y *= -1;
                    }
                    if (scale.x < 1 && scale.y < 1)
                    {
                        speed = 8;
                    }
                    if (scale.x > 2 && scale.y > 2
                        && scale.x < 3 && scale.y < 3)
                    {
                        speed = 6;
                    }
                    if (scale.x > 3 && scale.y > 3
                        && scale.x < 4 && scale.y < 4)
                    {
                        speed = 4;
                    }
                    if (scale.x > 4 && scale.y > 4)
                    {
                        speed = 2;
                    }
                    if (push)
                    {
                        if ((Screen.width * 0.5f) + BlankArea < touchpos.x)//右
                        {
                            //_touchpos.x -= localpos.x*AdjustedvValue;
                            _touchpos.x -= speed;
                        }
                        if ((Screen.width * 0.5f) - BlankArea > touchpos.x)//左
                        {
                            //_touchpos.x += localpos.x*AdjustedvValue;
                            _touchpos.x += speed;
                        }
                        if ((Screen.height * 0.5f) + BlankArea < touchpos.y)//上
                        {
                            //_touchpos.y -= localpos.y*AdjustedvValue;
                            _touchpos.y -= speed;
                        }
                        if ((Screen.height * 0.5f) - BlankArea > touchpos.y)//下
                        {
                            //_touchpos.y += localpos.y*AdjustedvValue;
                            _touchpos.y += speed;

                        }

                    }
                    break;
                case MoveChange.ConstantVelocity:
                    if (push)
                    {
                        if ((Screen.width * 0.5f) + BlankArea < touchpos.x)//右
                        {
                            _touchpos.x -= 2;
                        }
                        if ((Screen.width * 0.5f) - BlankArea > touchpos.x)//左
                        {
                            _touchpos.x += 2;
                        }
                        if ((Screen.height * 0.5f) + BlankArea < touchpos.y)//上
                        {
                            _touchpos.y -= 2;
                        }
                        if ((Screen.height * 0.5f) - BlankArea > touchpos.y)//下
                        {
                            _touchpos.y += 2;
                        }

                    }
                    break;

            }
           
            Debug.Log(Input.touches[0].pressure);
            Debug.Log(speed);
           
           
            rect.position = new Vector2(_touchpos.x, _touchpos.y);
        }
    }
    void UIOutSide()
    {
        var pos = rect.localPosition;
        var _pos = panel.transform.position;

        //var _pos = pos;//最初の値格納
        //右端
        if (pos.x + _pos.x < Screen.width / 2)
        {
            //pos.x = pos.x+_pos.x;
        }
        //左端
        if (pos.x-_pos.x < Screen.width - Screen.width)
        {
            //pos.x = 0;
        }
        // Debug.Log(pos.x - _pos.x);
        Debug.Log(pos);
        rect.localPosition = new Vector2(pos.x, pos.y);

    }
}
