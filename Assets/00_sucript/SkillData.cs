using System;
using UnityEngine;
[CreateAssetMenu(menuName = "TIEMONIA/SkillData Data")]
public class SkillData : ScriptableObject
{
    // 技を識別する固定ID
    public int SkillId { get; }

    // 技の名前
    public string Name { get; }

    // 技の威力
    public int Power { get; }

    // 命中率（0～100）
    public int Accuracy { get; }

    // 行動優先度
    public int Priority { get; }

    // 技の属性
    public ElementType Element { get; }

    // 技の対象
    public SkillTargetType TargetType { get; }

    /// <summary>
    /// 技の基本情報を初期化する。
    /// </summary>
    public SkillData(
        int skillId,
        string name,
        int power,
        int accuracy,
        int priority,
        ElementType element,
        SkillTargetType targetType)
    {
        if (skillId <= 0)
            throw new ArgumentOutOfRangeException(nameof(skillId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("技名を設定してください。", nameof(name));

        if (power < 0)
            throw new ArgumentOutOfRangeException(nameof(power));

        if (accuracy < 0 || accuracy > 100)
            throw new ArgumentOutOfRangeException(nameof(accuracy));

        SkillId = skillId;
        Name = name;
        Power = power;
        Accuracy = accuracy;
        Priority = priority;
        Element = element;
        TargetType = targetType;
    }
}
