using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlotUI : MonoBehaviour {

    [SerializeField] Image gfx;
    CharacterBase charData;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void UpdateUI() {
        gfx.sprite = charData.Spr ? charData.Spr : null;
    }

    public void SetCharacter(CharacterBase charData) {
        this.charData = charData;
        UpdateUI();
    }

    public void ResetSlot() {
        gfx.sprite = null;
    }
}
