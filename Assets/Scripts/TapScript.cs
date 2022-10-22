using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapScript : MonoBehaviour
{
    private float Timer = 0;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 1)
        {
            Destroy(gameObject);
        }
    }
}