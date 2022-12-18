using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCell : MonoBehaviour
{
    [Header("角色信息")]
    [HideInInspector] public Character_SO characterData;
    [HideInInspector] public bool characterUnlock;
    [Header("单元格UI")]
    public Image characterSprite;
    public Button characterPress;
    public GameObject characterLock;

    public void InitialCharacterCell()
    {
        characterSprite.sprite = null;
        characterLock.SetActive(false);
        characterPress.interactable = false;
        gameObject.SetActive(false);
    }

    public void SetCharacterData(Character_SO characterSO, bool unlock)
    {
        // 赋值
        characterData = characterSO;
        characterUnlock = unlock;

        // 设置UI
        SetCharacterCell();
    }

    private void SetCharacterCell()
    {
        if (characterUnlock)
        {
            characterSprite.sprite = characterData.characterSprite_unlock;
            characterLock.SetActive(false);
            characterPress.interactable = true;
        }
        else
        {
            if (characterData.characterSprite_lock != null)
                characterSprite.sprite = characterData.characterSprite_lock;
            characterLock.SetActive(true);
            characterPress.interactable = false;
        }
    }
}
