using System;
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

    //[Tooltip("��Ч����ʱ��")]
    //[SerializeField] private float lifeTime = 0.4f;

    //private bool startTimer;

    //private float timer;

    //���ٴ���
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