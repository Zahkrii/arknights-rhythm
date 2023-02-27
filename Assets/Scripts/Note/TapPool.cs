public class TapPool : BasePool<TapScript>
{
    private void Awake()
    {
        Initialize();
    }

    protected override TapScript OnCreateObject()
    {
        var tap = base.OnCreateObject();
        tap.SetDestroyAction(delegate { Release(tap); });

        return tap;
    }

    protected override void OnGetObject(TapScript obj)
    {
        base.OnGetObject(obj);
        obj.transform.position = this.transform.position;
    }
}