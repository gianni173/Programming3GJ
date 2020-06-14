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
            if(child.name != gameObject.name)
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

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    #endregion
}
