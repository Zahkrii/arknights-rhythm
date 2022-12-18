using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_SO", menuName = "Character/CharacterData")]
public class Character_SO : ScriptableObject
{
    [Header("角色名称")]
    public string characterName_cn;
    public string characterName_py;
    [Header("角色ID")]
    public OpreatorID character_id;
    [Header("角色列表立绘")]
    public Sprite characterSprite_unlock;
    public Sprite characterSprite_lock;
    [Header("角色详情立绘")]
    public Sprite characterSprite;
    [Header("角色语音")]
    [Header("角色星级")]
    public int character_Star;
}
