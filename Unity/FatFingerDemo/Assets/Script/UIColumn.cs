using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIColumn : MonoBehaviour
{
    GameObject Button;
    Button buttonscale;
    GameObject canvas;
    RectTransform rt;
    Vector2 pos, scale;
    public Vector2 buttonNum;
    public float _scale;
    // Start is called before the first frame update
    public enum trans
    {
        Side,
        Center,
    }
    public trans _trans;
    void Start()
    {
        //一つの値で変更できるように
        scale.x = _scale;
        scale.y = _scale;
        Button = Resources.Load<GameObject>("Prefab/Button");
        buttonscale = Resources.Load<Button>("Prefab/Button");
        canvas = GameObject.Find("Canvas/BackGround");
        rt = GetComponent<RectTransform>();
        RectTransform buttonRect = buttonscale.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(scale.x, scale.y);
        pos = rt.anchoredPosition;

        switch (_trans) {
            case trans.Side://左端に配置
                pos.x = -Screen.width * 0.5f + rt.rect.width * 0.5f + scale.x / 2;
                pos.y = Screen.height * 0.5f + rt.rect.height * 0.5f - scale.y / 2;
                for (int I = 0; I < buttonNum.y; I++)
                {
                    for (int i = 0; i < buttonNum.x; i++)
                    {

                        Placement();
                        pos.x += (Screen.width - scale.x) / (buttonNum.x - 1);
                    }
                    pos.x = scale.x / 2;
                    pos.y -= (Screen.height - scale.y) / (buttonNum.y - 1);
                }
                break;

            case trans.Center://中央に配置
                pos.x = Screen.width*0.5f;
                pos.y = Screen.height*0.5f;
                Placement();
                break;
        }
        
       
    }
    void Placement()
    {
        Button = Instantiate(Button, pos, Quaternion.identity) as GameObject;
        Button.transform.parent = canvas.transform;
    }
    
}
