using UnityEngine;

[CreateAssetMenu(fileName = "player_input", menuName = "InputType/Player")]
public class PlayerInput : InputType
{
    [SerializeField] private LayerMask groundMask = default;

    #region Methods

    public override Vector3 GetMovementInput(Character target, Transform transform)
    {    
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }

    public override Vector3 LookAtInput(Character target, Transform transform, Camera cam)
    {
        if (cam)
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast (camRay, out hit, Mathf.Infinity, groundMask))
            {
                Vector3 playerToMouse = hit.point - transform.position;
                playerToMouse.y = transform.position.y;
                return transform.position + playerToMouse;
            }
            return Vector3.zero;
        }
        return Vector3.zero;
    }

    public override bool ShootInput(Character target, Transform firingPoint)
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    #endregion
}