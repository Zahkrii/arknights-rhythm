using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BasePool<T> : MonoBehaviour where T : Component
{
    [Header("对象池设置")]
    [Tooltip("预制体")]
    [SerializeField] private T prefab;

    [Tooltip("对象池初始默认容量，超过则进行OnCreate实例化操作")]
    [SerializeField] private int defaultSize = 20;

    [Tooltip("对象池最大容量，超过将不再添加对象到对象池，而执行OnDestroy操作")]
    [SerializeField] private int maxSize = 50;

    private ObjectPool<T> _pool;//对象池实例

#if UNITY_EDITOR

    [Header("对象池监测")]
    [SerializeField] private string tip = "以下内容仅供监测，不要修改";

    [Tooltip("实例总计数")]
    [SerializeField] private int totalCount;

    [Tooltip("激活实例计数")]
    [SerializeField] private int activeCount;

    [Tooltip("未激活实例计数")]
    [SerializeField] private int inactiveCount;

    private void Update()
    {
        totalCount = _pool.CountAll;
        activeCount = _pool.CountActive;
        inactiveCount = _pool.CountInactive;
    }

#endif

    /// <summary>
    /// 对象池初始化
    /// </summary>
    /// <param name="collectionCheck">是否进行集合检查，检查重复元素，仅在 Unity Editor 生效</param>
    protected void Initialize(bool collectionCheck = true)
    {
        _pool = new ObjectPool<T>(OnCreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, collectionCheck, defaultSize, maxSize);
    }

    /// <summary>
    /// 对象池元素不足时，进行实例化操作
    /// </summary>
    /// <returns></returns>
    protected virtual T OnCreateObject()
    {
        return Instantiate(prefab, this.transform);
    }

    /// <summary>
    /// 从对象池获取元素时执行的方法
    /// </summary>
    /// <param name="obj">对象</param>
    protected virtual void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
    }

    /// <summary>
    /// 将元素存入对象池时执行的方法
    /// </summary>
    /// <param name="obj">对象</param>
    protected virtual void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 超过对象池最大容量，或执行清除操作时执行的方法
    /// </summary>
    /// <param name="obj">对象</param>
    protected virtual void OnDestroyObject(T obj)
    {
        Destroy(obj.gameObject);
    }

    //以下是可被外部调用的方法

    /// <summary>
    /// 从对象池获取一个对象
    /// </summary>
    /// <returns>对象</returns>
    public T Get()
    {
        return _pool.Get();
    }

    /// <summary>
    /// 将对象存入对象池
    /// </summary>
    /// <param name="obj">对象</param>
    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    /// <summary>
    /// 清空对象池，非必要不使用
    /// </summary>
    public void Clear()
    {
        _pool.Clear();
    }
}