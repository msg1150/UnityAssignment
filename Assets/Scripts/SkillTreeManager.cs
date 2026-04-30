using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    [Header("전체 스킬 목록")]
    [SerializeField] private List<SkillNode> allSkills = new List<SkillNode>();

    [Header("처음부터 열려있는 시작 스킬")]
    [SerializeField] private SkillNode startSkill;

    [Header("서비스")]
    [SerializeField] private SkillUnlockService unlockService;
    [SerializeField] private SkillEffectLogService effectLogService;
    [SerializeField] private SkillTreeSaveService saveService;

    [Header("초기화 버튼")]
    [SerializeField] private Button resetButton;

    private void OnEnable()
    {
        foreach (SkillNode skill in allSkills)
        {
            if (skill != null)
            {
                skill.OnLearned += HandleSkillLearned;
            }
        }

        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetSkillTree);
        }
    }

    private void OnDisable()
    {
        foreach (SkillNode skill in allSkills)
        {
            if (skill != null)
            {
                skill.OnLearned -= HandleSkillLearned;
            }
        }

        if (resetButton != null)
        {
            resetButton.onClick.RemoveListener(ResetSkillTree);
        }
    }

    private void Start()
    {
        LoadSkillTree();
    }

    private void HandleSkillLearned(SkillNode learnedSkill)
    {
        if (effectLogService != null)
        {
            effectLogService.PrintSkillLearnedLog(learnedSkill);
        }

        if (unlockService != null)
        {
            unlockService.UnlockLinkedSkills(learnedSkill);
        }

        SaveSkillTree();
    }

    private void LoadSkillTree()
    {
        ResetAllSkillsToLocked();

        if (saveService == null)
        {
            OpenStartSkill();
            return;
        }

        List<string> learnedSkillIds = saveService.LoadLearnedSkillIds();

        if (learnedSkillIds.Count <= 0)
        {
            OpenStartSkill();
            return;
        }

        RestoreLearnedSkills(learnedSkillIds);
        RebuildAvailableSkills();

        OpenStartSkill();

        Debug.Log("스킬트리 불러오기 완료");
    }

    private void SaveSkillTree()
    {
        if (saveService == null) return;

        List<string> learnedSkillIds = new List<string>();

        foreach (SkillNode skill in allSkills)
        {
            if (skill == null) continue;

            if (!skill.IsLearned()) continue;

            if (string.IsNullOrEmpty(skill.SkillId)) continue;

            learnedSkillIds.Add(skill.SkillId);
        }

        saveService.SaveLearnedSkillIds(learnedSkillIds);
    }

    private void ResetSkillTree()
    {
        if (saveService != null)
        {
            saveService.ClearSaveData();
        }

        ResetAllSkillsToLocked();
        OpenStartSkill();

        Debug.Log("스킬트리가 초기화되었습니다.");
    }

    private void ResetAllSkillsToLocked()
    {
        foreach (SkillNode skill in allSkills)
        {
            if (skill != null)
            {
                skill.ResetToLocked();
            }
        }
    }

    private void OpenStartSkill()
    {
        if (startSkill == null) return;

        startSkill.SetAvailable();

        if (startSkill.SkillData != null && startSkill.IsAvailable())
        {
            Debug.Log($"{startSkill.SkillData.SkillName} 스킬이 열렸습니다.");
        }
    }

    private void RestoreLearnedSkills(List<string> learnedSkillIds)
    {
        foreach (SkillNode skill in allSkills)
        {
            if (skill == null) continue;

            if (string.IsNullOrEmpty(skill.SkillId)) continue;

            if (learnedSkillIds.Contains(skill.SkillId))
            {
                skill.RestoreLearned();
            }
        }
    }

    private void RebuildAvailableSkills()
    {
        if (unlockService == null) return;

        foreach (SkillNode skill in allSkills)
        {
            if (skill == null) continue;

            if (skill.IsLearned())
            {
                unlockService.UnlockLinkedSkills(skill, false);
            }
        }
    }
}