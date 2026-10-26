using System;

/// <summary>
/// 戦闘中に付与される状態異常。
/// 複数の状態を同時に保持できるビットマスク。
/// </summary>
[System.Flags]
public enum MajorStatus
{
    None = 0,
    Poison = 1 << 0,
    Sleep = 1 << 1,
    Paralysis = 1 << 2,
    Burn = 1 << 3,
    Freeze = 1 << 4,
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