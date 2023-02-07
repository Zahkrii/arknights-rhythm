using System;
using UnityEngine;
/// <summary>
/// 事件信息处理站
/// </summary>
public static class EventHandler 
{
    public static event Action<int> UpdateAnimationEvent;
    public static void CallUpdateAnimationEvent(int index)
    {
        UpdateAnimationEvent?.Invoke(index);
    }
}
