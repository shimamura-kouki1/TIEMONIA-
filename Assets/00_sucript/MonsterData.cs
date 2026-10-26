using UnityEngine;

/// <summary>
/// モンスターの種族ごとの固定データ。
/// Unity エディターでアセットとして作成する。
/// </summary>
[CreateAssetMenu(menuName = "TIEMONIA/Monster Data", fileName = "MonsterData")]
public sealed class MonsterData : ScriptableObject
{
    [SerializeField] private string monsterName;
    [SerializeField, Min(1)] private int maxHp = 1;
    [SerializeField, Min(0)] private int attack;
    [SerializeField, Min(0)] private int defense;
    [SerializeField, Min(0)] private int speed;

    public string Name => monsterName;
    public int MaxHp => maxHp;
    public int Attack => attack;
    public int Defense => defense;
    public int Speed => speed;
}