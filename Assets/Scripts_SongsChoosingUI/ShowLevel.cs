using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevel : Data
{
    public GameObject[] icon;

    private void Awake()
    {
        for(int i = 0; i < icon.Length; i++)
        {
            if (icon[i] != null) { icon[i].SetActive(false); }
        }
    }
    private void Update()
    {
        if (Score[0,num] >= 10000)
        {
            if (icon[0] != null) { icon[0].SetActive(true); }
        }
        if (Score[1,num] >=10000)
        {
            if (icon[1] != null) { icon[1].SetActive(true); }
        }
        if (Score[2,num] >= 10000)
        {
            if (icon[2] != null) { icon[2].SetActive(true); }
        }
        if (Score[3,num] >= 10000)
        {
            if (icon[3] != null) { icon[3].SetActive(true); }
        }
    }
}
