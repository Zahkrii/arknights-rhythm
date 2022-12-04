using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    private float Timer = -10f / 5;

    private bool add = true, remove = true;

    //Note����
    private float size = 1;

    public float Size
    { set { size = value; } }

    // Update is called once per frame
    private void Update()
    {
        Timer += Time.deltaTime;
        if (add && Timer >= -0.05f)
        {
            ChartManager.Instance.dragPaddingList.Add(this);
            add = false;
        }
        else if (remove && Timer > 0.17f)
        {
            ChartManager.Instance.dragPaddingList.Remove(this);
            remove = false;
            Miss();
        }
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //������Ч
        VFXManager.Instance.ShowMissEffect(this.transform.position.x);
        //�������
        ScoreManager.Instance.MissNote();
        //���ж������Ƴ�
        ChartManager.Instance.dragPaddingList.Remove(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// �ж��Ƿ���� Note
    /// </summary>
    /// <param name="xPos"></param>
    /// <returns></returns>
    public bool PaddingNote(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4 * size)
        {
            //������Ч
            VFXManager.Instance.ShowDragEffect(xPos);
            AudioManager.Instance.PlayHitSFX();
            //�������
            ScoreManager.Instance.ScoreDrag();
            //���ж������Ƴ�
            ChartManager.Instance.dragPaddingList.Remove(this);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}