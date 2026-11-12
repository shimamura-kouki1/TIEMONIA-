/// <summary>
/// バトル中に発生した出来事を表す基底クラス
/// </summary>
public abstract class BattleEvent
{

}

/// <summary>
/// モンスターが技を使用したイベント
/// </summary>
public sealed class SkillUsedEvent : BattleEvent
{
    /// <summary> 技を使用したモンスター </summary>
    public BattleMonster User { get; }

    /// <summary> 使用した技 </summary>
    public SkillData Skill { get; }

    public SkillUsedEvent(BattleMonster user, SkillData skill)
    {
        User = user;
        Skill = skill;
    }
}

/// <summary>
/// モンスターがダメージを受けたときのイベント
/// </summary>
public sealed class DamageEvent : BattleEvent
{
    /// <summary>ダメージを受けたモンスター</summary>
    public BattleMonster Target { get; }

    /// <summary>受けたダメージ量</summary>
    public int Damage { get; }

    /// <summary>ダメージを受ける前のHP</summary>
    public int BeforeHp { get; }

    /// <summary>ダメージを受けた後のHP</summary>
    public int AfterHp { get; }

    public DamageEvent(
        BattleMonster target,
        int damage,
        int beforeHp,
        int afterHp)
    {
        Target = target;
        Damage = damage;
        BeforeHp = beforeHp;
        AfterHp = afterHp;
    }
}

/// <summary>
/// モンスターが倒れたときのイベント 
/// </summary>
public sealed class MonsterFaintedEvent : BattleEvent
{
    /// <summary>倒れたモンスター</summary>
    public BattleMonster Monster { get; }

    public MonsterFaintedEvent(BattleMonster monster)
    {
        Monster = monster;
    }
}

/// <summary>
/// モンスターの技が外れた時のイベント
/// </summary>
public sealed class SkillMissedEvent : BattleEvent
{
    /// <summary> 技を使用したモンスター </summary>
    public BattleMonster User { get; }

    /// <summary> 使用した技 </summary>
    public SkillData Skill { get; }

    /// <summary> 攻撃対象 </summary>
    public BattleMonster Target { get; }

    /// <summary>
    /// モンスターが攻撃を外した時のイベント
    /// </summary>
    /// <param name="user">技使用者</param>
    /// <param name="skill">使用技</param>
    /// <param name="target">攻撃される対象</param>
    public SkillMissedEvent(BattleMonster user,SkillData skill,BattleMonster target)
    {
        User = user;
        Skill = skill;
        Target = target;
    }
}



