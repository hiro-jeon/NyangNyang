using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]

public class Upgrade : ScriptableObject
{
    public enum UpgradeType
    {
        TECH,
        DRONE,
        SKILL
    }
    
    public string objectName;
    public Sprite sprite;
    public string details;
    public int level;
    public UpgradeType upgradeType;
}
