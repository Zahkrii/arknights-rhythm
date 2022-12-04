using UnityEngine;

// 创建资源创建菜单，文件名为fileName，菜单项名为menuName
[CreateAssetMenu(fileName = "NewChartAsset", menuName = "Chart Asset")]
public class ChartAsset : ScriptableObject //定义一个谱面资源，方便后期扩展以及加载
{
    [Header("曲绘资源")]
    public Sprite album;

    [Header("曲绘资源（模糊）")]
    public Sprite albumBlur;

    [Header("音乐资源")]
    public AudioClip music;

    [Header("谱面数据")]
    public TextAsset chart;
}