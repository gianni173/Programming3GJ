using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Fields

    [HideInInspector]
    public Character character;
    public Transform graphics;

    #endregion

    #region UnityCallbacks

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    #endregion

    #region Methods

    public void Move(Vector3 direction)
    {
        transform.Translate(direction * (character.Spd * Time.deltaTime), Space.World);
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