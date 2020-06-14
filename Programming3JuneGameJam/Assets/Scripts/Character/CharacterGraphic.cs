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

    #endregion
}
