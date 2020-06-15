using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterGraphic : MonoBehaviour
{
    #region Fields

    [Required]
    public Transform weaponHandler;

    #endregion
    
    
    #region Methods

    public void SetUpGraphic(CharacterStats.ModelKey modelKey)
    {
        foreach(Transform child in transform.GetComponentInChildren<Transform>())
        {
            if(child.name != gameObject.name &&
               child.name != "Root")
            {
                child.gameObject.SetActive(false);
            }
        }

        var model = transform.Find(modelKey.ToString());
        if (model)
        {
            model.gameObject.SetActive(true);
        }
    }

    public void SetWeapon(Character character)
    {
        var weaponCreated = Instantiate(character.Stats.weaponPrefab, weaponHandler);
        weaponCreated.transform.localPosition = Vector3.zero;
        weaponCreated.transform.localRotation = Quaternion.Euler(Vector3.zero);
        character.Animation.SetWeaponType(character.Stats.weaponType);
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    #endregion
}
