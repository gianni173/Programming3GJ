using System;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    #region Fields

    [SerializeField] private Character character;
    [SerializeField] private InputType inputType;
    [SerializeField] private Weapon weapon;

    private Camera cam;

    #endregion

    #region UnityCallbacks

    private void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void Update()
    {
        GetInput();
    }

    #endregion

    #region Methods

    private void GetInput()
    {
        if (inputType)
        {
            character.movement.Move(inputType.GetMovementInput());
            character.movement.LookAt(inputType.LookAtInput(transform, cam));
            inputType.ShootInput(weapon.firingMode, weapon.firingPoint, weapon.projectileType);
        }
    }

    #endregion
}