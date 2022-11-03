[System.Serializable]
public class Stat {
    public int Value { get; private set; }
    public int MaxValue { get; private set; }
    public EStatType Type { get; private set; }

    public Stat(int maxValue, EStatType type) {
        MaxValue = maxValue;
        Value = MaxValue;
        Type = type; 
    }

    public void AddValue(int amount) {
        Value += (Value + amount > MaxValue) ? MaxValue : Value + amount;
    }

    public void RemoveValue(int amount) {
        Value -= (Value - amount < 0) ? 0 : Value - amount;
    }
}

public enum EStatType { Health, Strength, Armor, Dexterity, Knowledge }