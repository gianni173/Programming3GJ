using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields

    public ProjectileType type;
    public Rigidbody rb;

    #endregion

    #region UnityCallbacks

    private void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    #endregion

    #region Methods

    public void Push(Vector3 direction)
    {
        rb.AddForce(direction * type.speed * Time.deltaTime, ForceMode.VelocityChange);
        transform.LookAt(transform.position + direction);
    }

    #endregion
}