using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyTargetButtonUI : MonoBehaviour {
    
    [SerializeField] TextMeshProUGUI name;

    public void SetTarget(string name) {
        this.name.text = name;
    }

    public void ResetSlot() {
        name.text = "";
        gameObject.SetActive(false);
    }
}
