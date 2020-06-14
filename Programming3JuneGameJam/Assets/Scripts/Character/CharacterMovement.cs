using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Fields

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

    public void Move(Vector3 direction)
    {
        var rightMovement = direction.x * cameraRight;
        var forwardMovement = direction.z * cameraForward;
        transform.position += (rightMovement + forwardMovement).normalized * Time.fixedDeltaTime * character.Spd;
    }
    
    public void LookAt(Vector3 target)
    {
        if (target == Vector3.zero)
        {
            target = transform.position + transform.forward;
        }
        transform.LookAt(target);
    }

    #endregion
}