using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    /// <summary>
    /// 启用时执行0.4秒后自毁，特效时长也是0.4秒
    /// </summary>
    private void OnEnable()
    {
        Destroy(gameObject, 0.4f);
    }
}