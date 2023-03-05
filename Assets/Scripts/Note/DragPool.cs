public class DragPool : BasePool<DragScript>
{
    private void Awake()
    {
        Initialize();
    }

    protected override DragScript OnCreateObject()
    {
        var drag = base.OnCreateObject();
        drag.SetDestroyAction(delegate { Release(drag); });

        return drag;
    }

    protected override void OnGetObject(DragScript obj)
    {
        base.OnGetObject(obj);
        obj.transform.position = this.transform.position;
    }
}