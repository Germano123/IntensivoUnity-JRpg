using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour {

    GameManager gameManager;

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onEncounter += OnEncounter;
        Initiative = new Queue<CharacterBase>();

        Allies = new List<CharacterBase>();
        Enemies = new List<CharacterBase>();
    }

    public List<CharacterBase> Allies { get; private set; }
    public List<CharacterBase> Enemies { get; private set; }
    public Queue<CharacterBase> Initiative { get; private set; }

    void OnEncounter(MapData map, List<CharacterBase> allies, List<CharacterBase> enemies) {
        List<CharacterBase> all = new List<CharacterBase>();
        Debug.Log($"enemies.Count from fighmanager: {enemies.Count}");
        Allies.Clear();
        Enemies.Clear();
        foreach (CharacterBase ally in allies) {
            all.Add(ally);
            Allies.Add(ally);
        }
        foreach (CharacterBase enemy in enemies) {
            all.Add(enemy);
            Enemies.Add(enemy);
        }
        foreach (CharacterBase character in all) {
            character.onDied += OnCharacterDied;
        }

        OrganizeInitiatives(all, allies, enemies);
    }

    void OnCharacterDied(CharacterBase character) {
        character.onDied -= OnCharacterDied;
        // TODO: remove character from Initiative queue
        CharacterBase _char;
        while (true) {
            _char = Initiative.Dequeue();
            if (_char == character) break;
            else Initiative.Enqueue(_char);
        }
        if (!AllCharsDead(Enemies)) {
            // player win combat
            OnFightEnd(true);
        }
        if (!AllCharsDead(Allies)) {
            // player lost combat
            OnFightEnd(false);
        }
    }

    bool AllCharsDead(List<CharacterBase> characters) {
        foreach (CharacterBase _char in characters) {
            if (!_char.IsDead()) return true;
        }
        return false;
    }

    void OrganizeInitiatives(List<CharacterBase> all, List<CharacterBase> allies, List<CharacterBase> enemies) {
        List<Tuple<int, CharacterBase>> _initiatives = new List<Tuple<int, CharacterBase>>();
        int _initiative = 0;
        for (int i = 0; i < all.Count; i++) {
            int _randIniative = UnityEngine.Random.Range(0, 20) + all[i].GetStat(EStatType.Dexterity);
            _initiatives.Add(new Tuple<int, CharacterBase>(_randIniative, all[i]));
        }
        _initiatives.Sort((x, y) => x.Item1.CompareTo(y.Item1));
        // Initiative.Enqueue(_iniatives);
    }

    void OnFightEnd(bool winFight) {
        gameManager.EndFight(winFight);
    }

    void OnDestroy() {
        gameManager.onEncounter -= OnEncounter;

        CharacterBase _char;
        while (Initiative.Count > 0) {
            _char = Initiative.Dequeue();
            _char.onDied -= OnCharacterDied;
        }
    }
}
