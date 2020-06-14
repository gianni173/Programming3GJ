using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterGraphic : MonoBehaviour
{
    #region Fields

    [Required]
    public Transform weaponHandler;

    #endregion
    
    
    #region Methods

    public void SetUpGraphic(CharacterStats stats)
    {
        GameObject model = transform.Find(stats.model.name).gameObject;
            if (model)
                model.SetActive(true);
    }

    public void SetWeapon(Character character)
    {
        var instance = Instantiate(character.Stats.weaponPrefab, Vector3.zero, Quaternion.identity, weaponHandler);
        character.animation.SetWeaponType(character.Stats.weaponType);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    #endregion
}
