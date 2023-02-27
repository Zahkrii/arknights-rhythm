using System;
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

    //[Tooltip("特效持续时长")]
    //[SerializeField] private float lifeTime = 0.4f;

    //private bool startTimer;

    //private float timer;

    //销毁处理
    private Action<VFXScript> destroyAction;

    public void SetDestroyAction(Action<VFXScript> destroyAction) => this.destroyAction = destroyAction;

    //private void OnEnable()
    //{
    //    timer = lifeTime;
    //    startTimer = true;
    //}

    //private void Update()
    //{
    //    if (!startTimer)
    //        return;

    //    timer -= Time.deltaTime;
    //    if (timer <= 0 && this.gameObject.activeSelf)
    //    {
    //        destroyAction.Invoke(this);
    //    }
    //}

    //private void OnDisable()
    //{
    //    startTimer = false;
    //}
}