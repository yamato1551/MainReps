using UnityEngine;
public class UI3Dtouch : MonoBehaviour
{
    [SerializeField]
    GameObject Button,_Button;
    Vector2 pos,Size;
    bool Push;
    int ButtonNum, TouchPressure;
    [SerializeField]
    GameObject[] instansButton=new GameObject[4];
    void Start()
    {
        Push = false;
        Button = Resources.Load<GameObject>("Prefab/3DtouchButton");
        _Button = this.gameObject;
        pos = this.gameObject.transform.position;
        Size = _Button.GetComponent<RectTransform>().sizeDelta;
    }
    public void ButtonPressed()
    {
        Push = true;
        Debug.Log("押された");
    }
    public void ButtonRelease()
    {
        Push = false;
    }
    void Update()
    {
        //Debug.Log(TouchPressure);
        
        if (Input.touches.Length > 0)
        {
            Debug.Log(Input.touches[0].pressure);
        }
        
        if (Push)//3Dtouchで圧力によるUI生成
        {
            switch (TouchPressure) {
                case 0:
                    ButtonNum = 0;
                    TouchPressure = 0;
                    Button = Resources.Load<GameObject>("Prefab/3DtouchButton");
                    if (Input.touches[0].pressure > 1)
                    {
                        pos.y += Size.y + 10;
                        Placement();
                        TouchPressure += 1;
                    }
                    break;

                case 1:
                    Button = Resources.Load<GameObject>("Prefab/3DtouchButton");

                    if (Input.touches[0].pressure > 2)
                    {
                        ButtonNum += 1;
                        pos.x -= Size.x - 10;
                        Placement();
                        TouchPressure += 1;
                    }

                    if (Input.touches[0].pressure < 1)
                    {
                        ButtonNum -= 1;
                        Destroy(instansButton[0]);
                        TouchPressure -= 1;
                    }
                    break;

                case 2:
                    Button = Resources.Load<GameObject>("Prefab/3DtouchButton");

                    if (Input.touches[0].pressure > 3)
                    {
                        ButtonNum += 1;
                        pos.y -= Size.y - 10;
                        Placement();
                        TouchPressure += 1;
                    }

                    if (Input.touches[0].pressure < 2)
                    {
                        ButtonNum -= 1;
                        Destroy(instansButton[1]);
                        TouchPressure -= 1;
                    }
                    break;

                case 3:
                    Button = Resources.Load<GameObject>("Prefab/3DtouchButton");

                    if (Input.touches[0].pressure > 4)
                    {
                        ButtonNum += 1;
                        pos.x += Size.x + 10;
                        Placement();
                        TouchPressure += 1;
                    }

                    if (Input.touches[0].pressure < 3)
                    {
                        ButtonNum -= 1;
                        Destroy(instansButton[2]);
                        TouchPressure -= 1;
                    }
                    break;

                case 4:
                    if (Input.touches[0].pressure < 4)
                    {
                        //ButtonNum -= 1;
                        Destroy(instansButton[3]);
                        TouchPressure -= 1;
                    }
                    break;
            }
        }
        else
        {
            ButtonNum = 0;
            TouchPressure = 0;
            Button = Resources.Load<GameObject>("Prefab/3DtouchButton");
            for (int i = 0; i < 4; i++) {
                Destroy(instansButton[i]);
            }
        }

    }
    void Placement()
    {
        Button = Instantiate(Button, pos, Quaternion.identity) as GameObject;
        instansButton[ButtonNum] = Button;
        Button.transform.parent = this.gameObject.transform;
        pos = this.gameObject.transform.position;
    }
}
