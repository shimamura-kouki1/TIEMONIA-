using System;

/// <summary>
/// 戦闘に参加しているモンスターの状態を管理するクラス
/// </summary>
public sealed class BattleMonster
{
    public Monster Owner { get; }
    public BattleSide Side { get; }

    public string Name => Owner.Data.Name;
    public int MaxHp => Owner.Data.MaxHp;
    public int CurrentHp { get; private set; }


    public int Attack => Owner.Data.Attack;
    public int Defense => Owner.Data.Defense;
    public int EffectiveSpeed => Owner.Data.Speed; // 能力変化を入れるまでは基礎速度

    // 戦闘後に残る状態を個体から引き継ぐ。
    public MajorStatus MajorStatus { get; private set; }

    // 混乱など、戦闘後に破棄する状態。
    public BattleStatus BattleStatus { get; private set; }

    public bool IsFainted => CurrentHp <= 0;

    /// <summary>
    /// 戦闘参加者を初期化する
    /// </summary>
    public BattleMonster(
        Monster owner, BattleSide side)
    {
        Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        Side = side;
        CurrentHp = owner.CurrentHp;
        MajorStatus = owner.Status;
        BattleStatus = BattleStatus.None;
    }

    /// <summary>HPを指定した分減らす</summary>
    public void TakeDamage(int amount)
    {
        if (amount <= 0 || IsFainted) return;

        CurrentHp = Math.Max(0, CurrentHp - amount);
    }

    /// <summary>HPを指定した分だけ回復する</summary>
    public void Heal(int amount)
    {
        if (amount <= 0 || IsFainted) return;
        CurrentHp = Math.Min(MaxHp, CurrentHp + amount);
    }

    public bool HasMajorStatus(MajorStatus status)
    {
        return (MajorStatus & status) != 0;
    }

    /// <summary>状態異常を追加する</summary>
    public void AddMajorStatus(MajorStatus status)
    {
        MajorStatus |= status;
    }


    /// <summary>指定した状態異常を取り除く</summary>
    public void RemoveMajorStatus(MajorStatus status)
    {
        MajorStatus &= ~status;
    }

    /// <summary>指定した状態異常があるかを調べる</summary>

    public bool HasBattleStatus(BattleStatus status)
    {
        return (BattleStatus & status) != 0;
    }

    /// <summary>すべての状態異常を取り除く</summary>
    public void ClearStatuses()
    {
        MajorStatus = MajorStatus.None;
    }

    public void ClearBattleStatus()
    {
        BattleStatus = BattleStatus.None;
    }

    public void AddBattleStatus(BattleStatus status)
    {
        BattleStatus |= status;
    }

    public void RemoveBattleStatus(BattleStatus status)
    {
        BattleStatus &= ~status;
    }

    /// <summary>戦闘終了時に、引き継ぐデータだけ仲間個体へ反映する。</summary>
    public void ApplyBattleResult()
    {
        Owner.SetHp(CurrentHp);
        Owner.SetStatus(MajorStatus);
        // BattleStatus は Owner に反映しないため、戦闘後は消える。
    }

}
