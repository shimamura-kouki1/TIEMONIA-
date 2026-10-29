using System;

//戦闘中にLevelUpした時にステータスをアップデートしたほうがよろし

/// <summary>
/// 戦闘に参加しているモンスターの状態を管理するクラス
/// </summary>
public sealed class BattleMonster
{
    public Monster Owner { get; }
    public BattleSide Side { get; }

    public string Name => Owner.Name;
    public int CurrentHp { get; private set; }

    private int _maxHp;
    private int _attack;
    private int _defense;
    private int _speed;

    public int MaxHp => _maxHp;
    public int Attack => _attack;
    public int Defense => _defense;
    public int EffectiveSpeed => _speed;

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

        _maxHp = owner.MaxHp;
        _attack = owner.Attack;
        _defense = owner.Defense;
        _speed = owner.Speed;

        CurrentHp = owner.CurrentHp;
        MajorStatus = owner.Status;
        BattleStatus = BattleStatus.None;
    }

    /// <summary>
    /// レベルアップしたときのステータス更新
    /// レベルアップ処理に呼ぶ
    /// </summary>
    public void RefreshStats()
    {
        int oldMaxHp = _maxHp;

        //個体の最新ステータスを戦闘中のデータに反映する
        _maxHp = Owner.MaxHp;
        _attack = Owner.Attack;
        _defense = Owner.Defense;
        _speed = Owner.Speed;

        //最大HPの増加分だけ現在HPも増やす
        int hpIncrease = _maxHp - oldMaxHp;

        if(hpIncrease > 0 && !IsFainted)
        {
            CurrentHp = Math.Min(_maxHp, CurrentHp + hpIncrease);
        }
        else
        {
            //もし最大HPが減った場合、現在HPが上限を超えないようにする
            CurrentHp = Math.Min(CurrentHp, _maxHp);
        }
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

    /// <summary>HPを指定した分回復させ蘇生させる</summary>
    public void Revive(int hp)
    {
        if (hp <= 0 || !IsFainted) return;

        CurrentHp = Math.Min(MaxHp, hp);
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
        BattleStatus = BattleStatus.None;
    }

    /// <summary>戦闘中の状態をすべて解除する</summary>
    public void ClearBattleStatus()
    {
        BattleStatus = BattleStatus.None;
    }

    /// <summary>持続する状態異常を解除する</summary>
    public void ClearMajorStatus()
    {
        MajorStatus = MajorStatus.None;
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
