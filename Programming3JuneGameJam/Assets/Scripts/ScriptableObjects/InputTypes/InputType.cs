using UnityEngine;

public abstract class InputType : ScriptableObject
{
    #region Methods

    public abstract Vector3 GetMovementInput();
    public abstract Vector3 LookAtInput(Transform transform, Camera cam = null);
    public abstract void ShootInput(FiringMode firingMode, Transform firingPoint, ProjectileType type);

    #endregion
}
