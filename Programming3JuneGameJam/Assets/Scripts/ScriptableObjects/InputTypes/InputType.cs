using UnityEngine;

public abstract class InputType : ScriptableObject
{
    #region Methods

    public abstract Vector3 GetMovementInput(Character target, Transform transform);
    public abstract Vector3 LookAtInput(Character target, Transform transform, Camera cam = null);
    public abstract bool ShootInput(Character target, Transform firingPoint);

    #endregion
}