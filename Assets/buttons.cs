using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
    Image im;
    bool a;
    // Start is called before the first frame update
    void Awake()
    {
        im = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {

        if (a)
        {
            im.color = new Color32(225, 225, 225, 50);
        }
        else
        {
            im.color = new Color32(0, 0, 0, 0);
        }

    }
    public void bton()
    {
        a = true;
    }
    public void btoff()
    {
        a = false;
    }
}
