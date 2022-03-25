using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollBar;
    float scroll_pos = 0;
    float[]pos;
    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if(scroll_pos < pos[i] + (distance/2) && scroll_pos > pos[i] - (distance/2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector3.Lerp(transform.GetChild(i).localScale, new Vector3(1.5f, 1.5f,1.5f), 0.1f);
                for(int a = 0; a<pos.Length; a++)
                {
                    if( a != i)
                    {
                        transform.GetChild(a).localScale = Vector3.Lerp(transform.GetChild(a).localScale, new Vector3(0.8f, 0.8f,0.8f), 0.1f);
                    }
                }
            }
        }
    }
}
