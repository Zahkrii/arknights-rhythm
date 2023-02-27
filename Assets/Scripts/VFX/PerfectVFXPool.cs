public class PerfectVFXPool : BasePool<VFXScript>
{
    private void Awake()
    {
        Initialize();
    }

    protected override VFXScript OnCreateObject()
    {
        var perfect = base.OnCreateObject();
        perfect.SetDestroyAction(delegate { Release(perfect); });

        return perfect;
    }
}