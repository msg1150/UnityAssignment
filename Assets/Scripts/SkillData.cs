using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill Tree/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("ÀúÀå¿ë ID")]
    [SerializeField] private string skillId;

    [SerializeField] private string skillName;
    [SerializeField] private List<SkillEffect> effects = new List<SkillEffect>();
    
    public string SkillId => skillId;
    public string SkillName => skillName;
    public IReadOnlyList<SkillEffect> Effects => effects;
}