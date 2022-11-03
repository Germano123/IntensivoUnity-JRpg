using System.Collections;
using System.Collections.Generic;

public class CharacterStats {
    public Dictionary<EStatType, Stat> Stats { get; private set; }

    public CharacterStats(List<Stat> stats) {
        Stats = new Dictionary<EStatType, Stat>();
        foreach (Stat stat in stats) {
            if (Stats.ContainsKey(stat.Type)) {
                Stats[stat.Type].AddValue(stat.Value);
            } else {
                Stats.Add(stat.Type, stat);
            }
        }
    }
}