using System;

/// <summary>
/// 選択した行動１回分のデータ
/// </summary>
public sealed class BattleAction
{
    /// <summary>行動するモンスター</summary>
    public BattleMonster User { get; }

    /// <summary>行動の種類</summary>
    public ActionType Type { get; }

    /// <summary>使用する技</summary>
    public SkillData Skill { get; }

    /// <summary>行動の対象</summary>
    public BattleMonster[] Targets { get; }

    public BattleAction(
       BattleMonster user,
       ActionType type,
       SkillData skill,
       BattleMonster[] targets)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Type = type;
        Skill = skill;
        Targets = targets ?? Array.Empty<BattleMonster>();
    }
}

/// <summary>
/// 行動の種類
/// </summary>
public enum ActionType
{
    Attack,       // 技
    Item,         // アイテム
    Change,       // 交代
    Capture,      // 捕獲
    PrayerSkill,  // ※独自の行動として必要な場合
    Escape        // 逃走
}
