using UnityEngine;

public abstract class CharacterData : ScriptableObject {
    [Header("Character Data")]
    public new string name;
    public Sprite spr;
    public Animator anim;

    [Header("Stats")]
    public int health;
    public int strength;
    public int armor;
    public int dexterity;
    public int knowledge;    
}