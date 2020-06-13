using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ai_basic_aggro", menuName = "InputType/AI/BasicAggressive")]
public class BasicAggressive : AIInput
{
    #region Methods

    public override Vector3 GetMovementInput(Character target, Transform transform)
    {
        var direction = Vector3.zero;
        if (target)
        {
            direction = target.transform.position - transform.position;
            direction.y = 0;

            if(direction.magnitude <= shootingRange)
            {
                direction = Vector3.zero;
            }

            direction.Normalize();
        }
        return direction;
    }

    public override Vector3 LookAtInput(Character target, Transform transform, Camera cam = null)
    {
        if (target)
        {
            return target.transform.position;
        }
        return Vector3.zero;
    }

    public override bool ShootInput(Character target, Transform firingPoint)
    {
        if (target)
        {
            var direction = Vector3.zero;
            direction = firingPoint.position - target.transform.position;
            direction.y = 0;

            if (direction.magnitude <= shootingRange)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}
