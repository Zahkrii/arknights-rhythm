public class HoldPool : BasePool<HoldScript>
{
    private void Awake()
    {
        Initialize();
    }

    protected override HoldScript OnCreateObject()
    {
        var hold = base.OnCreateObject();
        hold.SetDestroyAction(delegate { Release(hold); });

        return hold;
    }

    protected override void OnGetObject(HoldScript obj)
    {
        base.OnGetObject(obj);
        obj.transform.position = this.transform.position;
    }
}