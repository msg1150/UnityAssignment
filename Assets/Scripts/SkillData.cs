using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill Tree/Skill Data")]
public class SkillData : ScriptableObject
{
    [SerializeField] private string skillName;
    [SerializeField] private List<SkillEffect> effects = new List<SkillEffect>();
    
    public string SkillName => skillName;
    public IReadOnlyList<SkillEffect> Effects => effects;
}