using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    /// <summary>
    /// ����ʱִ��0.4����Ի٣���Чʱ��Ҳ��0.4��
    /// </summary>
    private void OnEnable()
    {
        Destroy(gameObject, 0.4f);
    }
}