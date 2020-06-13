using UnityEngine;

[CreateAssetMenu(fileName = "player_input", menuName = "InputType/Player")]
public class PlayerInput : InputType
{
    #region Methods

    public override Vector3 GetMovementInput()
    {    
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }

    public override Vector3 LookAtInput(Transform transform, Camera cam = null)
    {
        if (cam)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = ray.GetPoint(Vector3.Distance(cam.transform.position, transform.position));
            return new Vector3(pos.x, transform.position.y, pos.z);
        }
        return Vector3.zero;
    }

    public override void ShootInput(FiringMode firingMode, Transform firingPoint, ProjectileType type)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            firingMode.Shoot(firingPoint, type);
        }
    }

    #endregion
}