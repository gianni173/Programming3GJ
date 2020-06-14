using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    #region Fields

    [NonSerialized] public InputType inputType = null;
    [NonSerialized] public Character closestTarget;

    [SerializeField] private Character character = null;
    [SerializeField] private Weapon weapon = null;

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

    private void OnDrawGizmos()
    {
        if(closestTarget)
        {
            var prevColor = Gizmos.color;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up, closestTarget.transform.position + Vector3.up);
            Gizmos.color = prevColor;
        }
    }

    private void Update()
    {
        GetUpdateTimeInput();
    }

    private void FixedUpdate()
    {
        GetFixedTimeInput();
    }

    #endregion

    #region Methods

    private void GetUpdateTimeInput()
    {
        if (inputType)
        {
            character.Movement.LookAt(inputType.LookAtInput(closestTarget, transform, cam));
            if(weapon.CanShoot() && inputType.ShootInput(closestTarget, weapon.firingPoint))
            {
                weapon.Shoot();
            }
        }
    }

    private void GetFixedTimeInput()
    {
        if (inputType)
        {
            character.Movement.Move(inputType.GetMovementInput(closestTarget, transform), inputType as AIInput != null);
        }
    }

    #endregion
}