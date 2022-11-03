using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable/MapData", order = 0)]
public class MapData : ScriptableObject {
    public new string name;
    public Sprite spr;

    public EnemyData[] enemies;
}