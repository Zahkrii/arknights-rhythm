using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject perfectVFX;
    [SerializeField] private PerfectVFXPool perfectVFXPool;
    [SerializeField] private GameObject goodVFX;
    [SerializeField] private GameObject missVFX;

    public static VFXManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowPaddingEffect(float hitTime, float xPos)
    {
        if (Mathf.Abs(hitTime) <= 0.085f)
        {
            Instantiate(
                perfectVFX,
                new Vector3(xPos, 1.001f, -4.5f),
                Quaternion.Euler(new Vector3(0, 0, 0)),
                this.transform);
            //var vfx = perfectVFXPool.Get();
            //vfx.transform.position = new Vector3(xPos, 0, 0);
        }
        else if (Mathf.Abs(hitTime) <= 0.17f)
        {
            Instantiate(
                goodVFX,
                new Vector3(xPos, 1.001f, -4.5f),
                Quaternion.Euler(new Vector3(0, 0, 0)),
                this.transform);
        }
    }

    public void ShowDragEffect(float xPos)
    {
        Instantiate(
                perfectVFX,
                new Vector3(xPos, 1.001f, -4.5f),
                Quaternion.Euler(new Vector3(0, 0, 0)),
                this.transform);
        //var vfx = perfectVFXPool.Get();
        //vfx.transform.position = new Vector3(xPos, 0, 0);
    }

    public void ShowMissEffect(float xPos)
    {
        Instantiate(
                missVFX,
                new Vector3(xPos, 1.001f, -4.5f),
                Quaternion.Euler(new Vector3(0, 0, 0)),
                this.transform);
    }
}