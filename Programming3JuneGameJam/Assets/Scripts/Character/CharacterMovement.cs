using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Fields

    public event Action<Vector3> OnLookAt;

    public Transform graphics;

    [SerializeField] private Character character = null;

    private Camera mainCam;
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    #endregion

    private void Start()
    {
        mainCam = Camera.main;
        cameraForward = mainCam.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        cameraRight = Quaternion.Euler(new Vector3(0, 90, 0)) * cameraForward;
    }

    #region Methods

    public void Move(Vector3 direction, bool isLocal)
    {
        var worldDirection = direction;
        if (!isLocal)
        {
            var rightMovement = direction.x * cameraRight;
            var forwardMovement = direction.z * cameraForward;
            worldDirection = (rightMovement + forwardMovement).normalized;
        }
        transform.position += worldDirection * Time.fixedDeltaTime * character.Spd;

        if (character.Animation)
        {
            character.Animation.SetMovementDirection(worldDirection * Time.fixedDeltaTime * character.Spd * 1000);
        }
    }
    
    public void LookAt(Vector3 target)
    {
        if (target != Vector3.zero)
        {
            transform.LookAt(target);
            OnLookAt?.Invoke(target);
        }
    }

    #endregion
}