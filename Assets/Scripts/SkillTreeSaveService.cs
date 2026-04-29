using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeSaveService : MonoBehaviour
{
    [SerializeField] private string saveKey = "SkillTree_LearnedSkills";

    public void SaveLearnedSkillIds(List<string> learnedSkillIds)
    {
        SkillTreeSaveData saveData = new SkillTreeSaveData();
        saveData.learnedSkillIds = learnedSkillIds;

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();

        Debug.Log("스킬트리 저장 완료");
    }

    public List<string> LoadLearnedSkillIds()
    {
        if (!PlayerPrefs.HasKey(saveKey)) return new List<string>();

        string json = PlayerPrefs.GetString(saveKey);

        if (string.IsNullOrEmpty(json)) return new List<string>();

        SkillTreeSaveData saveData = JsonUtility.FromJson<SkillTreeSaveData>(json);

        if (saveData == null || saveData.learnedSkillIds == null) return new List<string>();

        return saveData.learnedSkillIds;
    }

    public void ClearSaveData()
    {
        PlayerPrefs.DeleteKey(saveKey);
        PlayerPrefs.Save();

        Debug.Log("스킬트리 저장 데이터 삭제 완료");
    }

    [Serializable]
    public class SkillTreeSaveData
    {
        public List<string> learnedSkillIds = new List<string>();
    }
}