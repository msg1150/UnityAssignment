using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [Header("전체 스킬 목록")]
    [SerializeField] private List<SkillNode> allSkills = new List<SkillNode>();

    [Header("처음부터 열려있는 시작 스킬")]
    [SerializeField] private SkillNode startSkill;

    [Header("서비스")]
    [SerializeField] private SkillUnlockService unlockService;
    [SerializeField] private SkillEffectLogService effectLogService;

    private void OnEnable()
    {
        foreach (SkillNode skill in allSkills)
        {
            if (skill != null)
            {
                skill.OnLearned += HandleSkillLearned;
            }
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
    }

    private void Start()
    {
        if (startSkill != null)
        {
            startSkill.SetAvailable();

            if (startSkill.SkillData != null)
            {
                Debug.Log($"{startSkill.SkillData.SkillName} 스킬이 열렸습니다.");
            }
        }
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
    }
}