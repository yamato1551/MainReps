using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TapErrorDebug : MonoBehaviour
{
    Image thisImage;
    public int buttonNum;   
    StreamWriter sw;
    private bool colorchangeflag=true;
    private float colorFixTime=0.1f;
    void Start()
    {
        thisImage = this.gameObject.GetComponent<Image>();
    }
    
    void Update()
    {
        colorFixTime += Time.deltaTime;
        if (colorchangeflag && colorFixTime >= 0.1f)
        {
            thisImage.color = new Color(1, 1, 1, 0);
        }
        colorchangeflag = true;

        if (SceneMaster.tapBaseChange == false)
        {
            Debug.Log("Scope");
            ScopeTap();
        }
        
            
        
    }
    void ScopeTap()
    {
        var buttonpos = this.gameObject.transform.position;
        var buttonsize = this.gameObject.GetComponent<RectTransform>().sizeDelta;
        thisImage.color = new Color(1, 1, 1, 0);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var touchpos = touch.position;

            if (buttonpos.x - (buttonsize.x / 2) < touchpos.x
           && buttonpos.x + (buttonsize.x / 2) > touchpos.x
           && buttonpos.y - (buttonsize.y / 2) < touchpos.y
           && buttonpos.y + (buttonsize.y / 2) > touchpos.y)
            {
                thisImage.color = new Color(1, 1, 1, 100f / 255f);
                if (touch.phase == TouchPhase.Ended)
                {
                    //SceneMaster.touchTimes++;
                    sw = new StreamWriter(Application.dataPath + "/TextData.txt", true);
                    sw.WriteLine("タッチしたボタン:" + buttonNum + "タッチ回数:" + SceneMaster.touchTimes);// ファイルに書き出したあと改行
                    sw.Flush();// StreamWriterのバッファに書き出し残しがないか確認
                    sw.Close();// ファイルを閉じる

                }
            }
            
        }
    }
    public void OnTap()
    {
        if (SceneMaster.tapBaseChange == true)
        {
            colorchangeflag = false;
            colorFixTime = 0;
            thisImage.color = new Color(1, 1, 1, 100f / 255f);
            sw = new StreamWriter(Application.dataPath + "/TextData.txt", true);
            sw.WriteLine("タッチしたボタン:" + buttonNum + "タッチ回数:" + SceneMaster.touchTimes);// ファイルに書き出したあと改行
            sw.Flush();// StreamWriterのバッファに書き出し残しがないか確認
            sw.Close();// ファイルを閉じる
            Debug.Log("OnTap");
        }
    }
}
