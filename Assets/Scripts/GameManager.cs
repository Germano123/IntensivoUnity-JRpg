using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Callbacks
    public delegate void OnEncounter(MapData map, List<CharacterBase> allies, List<CharacterBase> enemies);
    public OnEncounter onEncounter;

    public delegate void OnFightEnd(bool winFight);
    public OnFightEnd onFightEnd;
    #endregion

    public bool OnFight { get; private set; }

    [SerializeField] MapData currentMap;
    [SerializeField] CharacterData[] alliesData;

    public List<CharacterBase> allies { get; private set; }
    public List<CharacterBase> enemies { get; private set; }

    // Start is called before the first frame update
    void Start() {
        OnFight = false;
        allies = new List<CharacterBase>();
        enemies = new List<CharacterBase>();

        foreach (CharacterData ally in alliesData) {
            CharacterBase newAlly = new CharacterBase(ally);
            allies.Add(newAlly);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Encounter();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            EndFight(false);
        }
    }

    public void Encounter() {
        // Debug.Log("Encounter!");
        if (!OnFight) {
            OnFight = true;
            enemies = RandEnemiesFromMap(currentMap);
            onEncounter?.Invoke(currentMap, allies, enemies);
        }
    }

    List<CharacterBase> RandEnemiesFromMap(MapData map) {
        List<CharacterBase> randEnemies = new List<CharacterBase>();
        int enemiesCount = Random.Range(1, 5);
        for (int i = 0; i < 2; i++) {
            CharacterData _enemyData = map.enemies[Random.Range(0, map.enemies.Length)];
            CharacterBase _enemy = new CharacterBase(_enemyData);
            randEnemies.Add(_enemy);
        }
        return randEnemies;
    }

    public void EndFight(bool winFight) {
        OnFight = false;
        onFightEnd?.Invoke(winFight);
    }
}
