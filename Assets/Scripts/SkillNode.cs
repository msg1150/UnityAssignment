using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode : MonoBehaviour
{
    [Header("스킬 데이터")]
    [SerializeField] private SkillData skillData;

    [Header("연결된 주변 스킬")]
    [SerializeField] private List<SkillNode> linkedSkills = new List<SkillNode>();

    private SkillNodeState state = SkillNodeState.Locked;

    public SkillData SkillData => skillData;
    public SkillNodeState State => state;
    public IReadOnlyList<SkillNode> LinkedSkills => linkedSkills;

    public event Action<SkillNode> OnStateChanged;
    public event Action<SkillNode> OnLearned;

    public void SetAvailable ()
    {
        if (state != SkillNodeState.Locked) return;

        state = SkillNodeState.Available;

        OnStateChanged?.Invoke(this);
    }

    public void Learn()
    {
        if (state != SkillNodeState.Available) return;

        state = SkillNodeState.Learned;

        OnStateChanged?.Invoke(this);
        OnLearned?.Invoke(this);
    }

    public bool IsLocked()
    {
        return state == SkillNodeState.Locked;
    }

    public bool IsAvailable()
    {
        return state == SkillNodeState.Available;
    }

    public bool IsLearned()
    {
        return state == SkillNodeState.Learned;
    }
}