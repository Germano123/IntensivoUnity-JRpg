using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatInterface : MonoBehaviour {

    GameManager gameManager;

    [SerializeField] GameObject combatParent;
    [SerializeField] GameObject actionsParent;
    [SerializeField] Image mapBG;
    [SerializeField] CharacterSlotUI[] alliesSlots;
    [SerializeField] CharacterSlotUI[] enemiesSlots;

    // Start is called before the first frame update
    void Start() {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.onEncounter += OnEncounter;
        gameManager.onFightEnd += OnFightEnd;
        OnFightEnd(false);
        ShowUI(false);
    }

    public void ShowUI(bool show) {
        combatParent.SetActive(show);
        actionsParent.SetActive(show);
        mapBG.gameObject.SetActive(show);
    }

    void OnEncounter(MapData map, List<CharacterBase> allies, List<CharacterBase> enemies) {
        ShowUI(true);
        Debug.Log($"enemies.Count from combatinterface: {enemies.Count}");
        mapBG.sprite = map.spr;
        for (int i = 0; i < allies.Count; i++) {
            alliesSlots[i].gameObject.SetActive(true);
            alliesSlots[i].SetCharacter(allies[i]);
        }
        for (int i = 0; i < enemies.Count; i++) {
            enemiesSlots[i].gameObject.SetActive(true);
            enemiesSlots[i].SetCharacter(enemies[i]);
        }
    }

    void OnFightEnd(bool winFight) {
        ShowUI(false);
        mapBG.sprite = null;
        for (int i = 0; i < alliesSlots.Length; i++) {
            alliesSlots[i].gameObject.SetActive(false);
            alliesSlots[i].ResetSlot();
        }
        for (int i = 0; i < enemiesSlots.Length; i++) {
            enemiesSlots[i].gameObject.SetActive(false);
            enemiesSlots[i].ResetSlot();
        }
    }

    void OnDestroy() {
        gameManager.onEncounter -= OnEncounter;
        gameManager.onFightEnd -= OnFightEnd;
    }
}
