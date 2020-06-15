using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "character_stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    public enum WeaponType {Light, Normal, Heavy}
    
    [Title("Character Name")]
    public string hierarchyName = "Base";
    [Space(5), Title("Combat Stats")]
    public float basicHP;
    public float attackMultiplier = 0f;
    public float basicSpd;
    public float speedMultiplier = 0f;
    public float hitBoxHeight = 2f;
    public float hitBoxRadius = .75f;
    public float defenceAgainstNotEnraged = 0f;
    public float defenceAgainstEnraged = 0f;
    public RageStats rageStats = null;
    [Space(5), Title("Input Stats")]
    public InputType basicInputType;
    [Space(5), Title("Faction Stats")]
    public Faction basicFaction;
    [Space(5), Title("Firing Stats")]
    public FiringMode basicFiringMode;
    public ProjectileType basicProjectileType;
    public ProjectileType basicEnragedProjectileType;
    [Space(5), Title("Graphic Stats")]
    public WeaponType weaponType = WeaponType.Light;
    public GameObject weaponPrefab;
    public ModelKey modelKey = ModelKey.None;
    public float sizeMultiplier = 1f;
    [Space(5), Title("Sound")]
    public bool playsHitSound = false;
    public Sound hitSound = null;
    public bool playsDefendedHitSound = false;
    public Sound defendedHitSound = null;
    public bool playsRageSound = true;
    public Sound rageSound = null;
    public bool playsRageDrainSound = true;
    public Sound rageDrainSound = null;
    public bool playsShootSound = true;
    public Sound shootSound = null;

    public enum ModelKey
    {
        None,
        Alien_Male_01,
        Alien_Male_02,
        Android_Female_01,
        Augmented_Male_01,
        Cop_01,
        Cyber_Female_01,
        Cyber_Male_01,
        CyberPunk_Male_01,
        CyborgNinja_01,
        Garbage_Male_01,
        Hacker_Female_01,
        Hologram_Female_01,
        Junky_Female_01,
        Junky_Male_01,
        Medical_Male_01,
        Monk_Male_01,
        Muscle_Male_01,
        Rich_Female_01,
        Rich_Male_01,
        Robot_01
    }
}
