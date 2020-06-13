using UnityEngine;

public class InputType : ScriptableObject
{
    #region Fields
    
    

    #endregion

    #region Methods

    public virtual Vector3 GetMovementInput()
    {
        return Vector3.zero;
    }

    public virtual Vector3 LookAtInput(Transform transform, Camera cam = null)
    {
        return Vector3.zero;
    }

    public virtual bool IsShooting()
    {
        return false;
    }

    public virtual void ShootInput(Character character, FiringMode firingMode, Transform firingPoint, ProjectileType type)
    {}

    #endregion
}