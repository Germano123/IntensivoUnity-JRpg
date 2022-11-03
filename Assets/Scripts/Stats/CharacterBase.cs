using System.Collections.Generic;
using UnityEngine;

public class CharacterBase {
    #region Callbacks
    public delegate void OnDied(CharacterBase thisChar);
    public OnDied onDied;
    #endregion

    CharacterStats characterStats;
    public Sprite Spr { get; private set; }
    public string Name { get; private set; }
    
    public CharacterBase(CharacterData charData) {
        Name = charData.name;

        List<Stat> stats = new List<Stat>();
        stats.Add(new Stat(charData.health, EStatType.Health));
        stats.Add(new Stat(charData.strength, EStatType.Strength));
        stats.Add(new Stat(charData.armor, EStatType.Armor));
        stats.Add(new Stat(charData.dexterity, EStatType.Dexterity));
        stats.Add(new Stat(charData.knowledge, EStatType.Knowledge));

        characterStats = new CharacterStats(stats);

        Spr = charData.spr;
    }

    public int GetStat(EStatType type) {
        return characterStats.Stats[type].Value;
    }

    public void TakeDamage(int amount) {
        // TODO
        if (GetStat(EStatType.Health) - amount <= 0) {
            amount -= GetStat(EStatType.Health);
            onDied?.Invoke(this);
        }
        characterStats.Stats[EStatType.Health].RemoveValue(amount);
    }

    public void HealAmount(int amount) {
        // TODO
    }

    public bool IsDead() {
        return (GetStat(EStatType.Health) <= 0);
    }
}