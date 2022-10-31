using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 0.4f);
    }
}