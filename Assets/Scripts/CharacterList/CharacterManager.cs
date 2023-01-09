using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [Header("角色列表单元格")]
    public GameObject characterCellPrefab;

    [Header("角色列表")]
    public Character_SO[] characterSOList;
    Dictionary<Character_SO, bool> characterListSorted = new Dictionary<Character_SO, bool>();

    [Header("角色列表 UI")]
    public GameObject listParent;
    private GameObject[] list;

    [Header("角色详情面板")]
    public GameObject characterDetail;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化每个单元格
        InitialAllCharacterCells();
        // 给角色列表排序 解锁情况>星级>名字拼音首字母
        SortCharacterList();
        // UI赋值
        SetCharacterUI();
    }

    # region 给角色列表排序

    /// <summary>
    /// 根据存档情况判断每个角色的解锁情况
    /// </summary>
    private Dictionary<Character_SO, bool> CheckCharacterUnlocked()
    {
        // 存储解锁情况
        Dictionary<Character_SO, bool> characterList = new Dictionary<Character_SO, bool>();

        // 解锁情况
        for (int i = 0; i < characterSOList.Length; i++)
        {
            // 初始标记为未解锁
            characterList[characterSOList[i]] = false;

            // 后期修改,测试用
            if (characterSOList[i].characterName_cn == "阿米娅" ||
            characterSOList[i].characterName_cn == "Logos" ||
            characterSOList[i].characterName_cn == "矢车菊" ||
            characterSOList[i].characterName_cn == "山")
            {
                // 标记为已解锁
                characterList[characterSOList[i]] = true;
            }
            //characterList[characterSOList[i]] = SaveManager.PlayerSave.opreators.Exists(item => item.id == characterSOList[i].character_id);
        }

        return characterList;
    }

    /// <summary>
    /// 给角色列表排序 解锁情况>星级>名字拼音首字母
    /// </summary>
    private void SortCharacterList()
    {
        // 获取解锁情况
        Dictionary<Character_SO, bool> characterList = CheckCharacterUnlocked();
        // 获取解锁情况分类结果
        List<Character_SO>[] character_lockSort = SortCharacterListByLock(characterList);
        // 对两种情况分星级结果
        List<Character_SO>[] character_lock_starSort = SortCharacterListByStar(character_lockSort[0]);
        List<Character_SO>[] character_unlock_starSort = SortCharacterListByStar(character_lockSort[1]);
        // 对两种情况分拼音结果
        List<Character_SO> character_lock_nameSort = SortCharacterListByName(character_lock_starSort);
        List<Character_SO> character_unlock_nameSort = SortCharacterListByName(character_unlock_starSort);

        // 重新排序 先已解锁 再未解锁
        foreach (var character in character_unlock_nameSort)
        {
            characterListSorted.Add(character, characterList[character]);
        }
        foreach (var character in character_lock_nameSort)
        {
            characterListSorted.Add(character, characterList[character]);
        }
    }

    /// <summary>
    /// 根据解锁情况划分，result[0]是未解锁，result[1]是已解锁
    /// </summary>
    private List<Character_SO>[] SortCharacterListByLock(Dictionary<Character_SO, bool> characterList)
    {
        List<Character_SO> character_lock = new List<Character_SO>();
        List<Character_SO> character_unlock = new List<Character_SO>();

        // 区分解锁与未解锁
        foreach (var character in characterList)
        {
            // 如果未解锁
            if (!character.Value) character_lock.Add(character.Key);
            // 如果已解锁
            else character_unlock.Add(character.Key);
        }

        List<Character_SO>[] result = new List<Character_SO>[2];
        result[0] = character_lock;
        result[1] = character_unlock;

        return result;
    }

    private List<Character_SO>[] SortCharacterListByStar(List<Character_SO> characterList)
    {
        // 获取最高星级
        int maxStar = 0;
        foreach (var character in characterList)
        {
            if (maxStar <= character.character_Star) maxStar = character.character_Star;
        }

        // 0,1,2,3,4,5,6,7,...,最高星级
        List<Character_SO>[] character_starSort = new List<Character_SO>[maxStar + 1];
        for (int i = 0; i < character_starSort.Length; i++)
        {
            character_starSort[i] = new List<Character_SO>();
        }
        foreach (var character in characterList)
        {
            character_starSort[character.character_Star].Add(character);
        }

        return character_starSort;
    }

    private List<Character_SO> SortCharacterListByName(List<Character_SO>[] characterList)
    {
        List<Character_SO> character_nameSort = new List<Character_SO>();
        // 星级从高到低
        for (int i = characterList.Length - 1; i >= 0; i--)
        {
            // 对应星级有角色
            if (characterList[i].Count != 0)
            {
                // 星级排序
                characterList[i].Sort((x, y) => x.characterName_py.CompareTo(y.characterName_py));
            }
            character_nameSort.AddRange(characterList[i]);
        }

        return character_nameSort;
    }

    # endregion

    private void InitialAllCharacterCells()
    {
        list = new GameObject[characterSOList.Length];

        for (int i = 0; i < characterSOList.Length; i++)
        {
            GameObject cell = Instantiate(characterCellPrefab, listParent.transform.position, Quaternion.identity, listParent.transform);
            cell.GetComponent<CharacterCell>().InitialCharacterCell();
            list[i] = cell;
        }
    }

    private void SetCharacterUI()
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].SetActive(true);
            // 设置单元格属性
            list[i].GetComponent<CharacterCell>().SetCharacterData(characterListSorted.ElementAt(i).Key, characterListSorted.ElementAt(i).Value);
            // 增加干员单元格监听事件
            list[i].GetComponent<Button>().onClick.AddListener(() => characterDetail.SetActive(true));
            list[i].GetComponent<Button>().onClick.AddListener(list[i].GetComponent<CharacterCell>().OnClick);
        }
    }
}
