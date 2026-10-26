/// <summary>
/// 選択した行動１回分のデータ
/// </summary>
public sealed class BattleAction
{
    /// <summary>行動するモンスター</summary>
    public BattleMonster User;
    /// <summary>行動の種類</summary>
    public ActionType Type;
    /// <summary>使用する技（技を使う場合）</summary>
    public SkillData Skill;
    /// <summary>行動の対象</summary>
    public BattleMonster[] Targets;
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
