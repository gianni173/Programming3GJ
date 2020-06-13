using System;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    #region Fields

    [HideInInspector]
    public Character character;
    public Camera cam;
    public InputType inputType;
    public Weapon weapon;

    #endregion

    #region UnityCallbacks

    private void Awake()
    {
        character = GetComponent<Character>();
        if (cam == null)
            cam = Camera.main;
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