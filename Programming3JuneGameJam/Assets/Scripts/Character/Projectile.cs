using Sirenix.OdinInspector;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields

    public ProjectileType type;

    [Required, SerializeField] private Rigidbody rb;

    private float currLifeTime = 0f;

    #endregion

    #region Unity Callbacks

    private void Update()
    {
        currLifeTime += Time.deltaTime;
        if(currLifeTime > type.lifeTime)
        {
            Despawn();
        }
    }

    #endregion

    #region Methods

    public void Push(Vector3 direction)
    {
        rb.AddForce(direction * type.speed, ForceMode.VelocityChange);
        transform.LookAt(transform.position + direction);
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    #endregion
}
