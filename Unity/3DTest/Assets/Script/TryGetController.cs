using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryGetController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Text>(out var text))
        {
            text.text = "test";
        }

        Dictionary<string, int> stats = new Dictionary<string, int>()
        {
            {"HP", 100 }
        };

        // ƒL[î•ñ‚ÉMP‚ª‚ ‚Á‚½ê‡‚Ì‚İˆ—
        if (stats.TryGetValue("MP", out var mp))
        {
            print(mp);
        } else
        {
            stats.Add("MP", 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
