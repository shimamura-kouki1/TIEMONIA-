using System;

/// <summary>
/// 戦闘中に付与される状態異常。
/// 同時に１種類だけ持つ
/// </summary>
[System.Flags]
public enum MajorStatus
{
    None,
    Poison,
    Burn,
    Paralysis,
    Sleep,
    Freeze
}

[System.Flags]
public enum BattleStatus
{
    None = 0,
    Confusion = 1 << 0,
}

/// <summary>戦闘参加者の陣営。</summary>
public enum BattleSide
{
    Player,
    Enemy,
}