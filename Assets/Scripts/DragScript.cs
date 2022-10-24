using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
    private float Timer = -10f / 5;

    private bool add = true, remove = true;

    private void Awake()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Timer += Time.deltaTime;
        if (add && Timer > -0.06f)
        {
            DataManager.Instance.dragPaddingList.Add(this);
            add = false;
            //this.GetComponent<SpriteRenderer>().color = Color.yellow;
            this.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (remove && Timer > 0.06f)
        {
            DataManager.Instance.dragPaddingList.Remove(this);
            remove = false;
            //this.GetComponent<SpriteRenderer>().color = Color.white;
            this.GetComponent<MeshRenderer>().material.color = Color.white;
            Miss();
        }
        transform.Translate(0, -5 * Time.deltaTime, 0);
    }

    private void Miss()
    {
        //生成特效
        //计算分数
        DataManager.Instance.dragPaddingList.Remove(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// 判定是否击中 Note
    /// </summary>
    /// <param name="xPos"></param>
    /// <returns></returns>
    public bool PaddingNote(float xPos)
    {
        float x = Mathf.Abs(transform.position.x - xPos);
        if (x < 0.4)
        {
            //生成特效
            //计算分数
            DataManager.Instance.dragPaddingList.Remove(this);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}