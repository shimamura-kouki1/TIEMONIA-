using UnityEngine;

/// <summary>
/// モンスターの種族ごとの固定データ。
/// Unity エディターでアセットとして作成する。
/// </summary>
[CreateAssetMenu(menuName = "TIEMONIA/Monster Data", fileName = "MonsterData")]
public sealed class MonsterData : ScriptableObject
{
    [SerializeField] private string _monsterName;
    [Header("レベル1の基礎ステータス")]
    [SerializeField] private int _baseHP = 30;
    [SerializeField] private int _baseAttack = 10;
    [SerializeField] private int _baseDefense = 8;
    [SerializeField] private int _baseSpeed = 7;

    [Header("1レベルごとの成長値")]
    [SerializeField] private int _hpGrowth = 5;
    [SerializeField] private int _attackGrowth = 2;
    [SerializeField] private int _defenseGrowth = 1;
    [SerializeField] private int _speedGrowth = 1;

    public string MonsterName => _monsterName;

    // レベルに応じたステータスを計算する
    public int CalculateMaxHP(int level)
        => _baseHP + (level - 1) * _hpGrowth;

    public int CalculateAttack(int level)
        => _baseAttack + (level - 1) * _attackGrowth;

    public int CalculateDefense(int level)
        => _baseDefense + (level - 1) * _defenseGrowth;

    public int CalculateSpeed(int level)
        => _baseSpeed + (level - 1) * _speedGrowth;
}

//レベルに応じたステータスを計算する形になっているが、成長テーブルのようなものを使って、モンスターごとに成長曲線を作るのもいいかも？