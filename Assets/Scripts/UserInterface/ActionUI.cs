using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionUI : MonoBehaviour {

    GameManager gameManager;
    // TurnManager turnManager;
    FightManager fightManager;

    [SerializeField] Button actionBtn;
    [SerializeField] GameObject characterActionsParent;
    // List<TextMeshProUGUI> charActions = new List<TextMeshProUGUI>();
    
    [SerializeField] EnemyTargetButtonUI[] enemySlotsBtn;

    int enemiesCount = 0;

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onEncounter += SetEnemiesUI;

        characterActionsParent.SetActive(false);
        actionBtn.Select();
    }

    void SetEnemiesUI(MapData map, List<CharacterBase> allies, List<CharacterBase> enemies) {
        enemiesCount = enemies.Count;
        Debug.Log($"enemies.Count from actionui: {enemies.Count}");
        for (int i = 0; i < enemiesCount; i++) {
            enemies[i].onDied += OnEnemyDied;
            enemySlotsBtn[i].SetTarget(enemies[i].Name);
        }
    }

    public void OnClick_ActionSelected(int index) {
        characterActionsParent.SetActive(true);
        
    }

    void OnEnemyDied(CharacterBase character) {
        character.onDied -= OnEnemyDied;
        // enemySlotsBtn[enemiesCount].ResetSlot();
        // enemiesCount--;
    }

    void OnDestroy() {
        gameManager.onEncounter -= SetEnemiesUI;
    }
}
