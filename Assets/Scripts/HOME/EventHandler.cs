using System;
using UnityEngine;
/// <summary>
/// �¼���Ϣ����վ
/// </summary>
public static class EventHandler 
{
    public static event Action<int> UpdateAnimationEvent;
    public static void CallUpdateAnimationEvent(int index)
    {
        UpdateAnimationEvent?.Invoke(index);
    }
}
