using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields

    [NonSerialized] public ProjectileType type;
    [NonSerialized] public Character owner;
    [NonSerialized] public float damage;

    private float currLifeTime = 0f;
    private bool hasHit = false;

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

    private void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region Methods

    public void SetDirection(Vector3 direction)
    {
        transform.LookAt(transform.position + direction);
    }

    private void Move()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * type.speed;
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHit)
        {
            if (other.tag == "Wall")
            {
                hasHit = true;
                if (type.hitParticlePrefab)
                {
                    Instantiate(type.hitParticlePrefab, transform.position, Quaternion.identity);
                }
                Despawn();
            }
            else 
            {
                var characterHit = other.GetComponent<Character>();
                if (characterHit != null && characterHit.Faction.IsEnemy(owner.faction))
                {
                    var damageInflicted = type.ProjectileBehaviour(characterHit, this);
                    owner.DamageInflicted(damageInflicted);
                    hasHit = true;
                    Despawn();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit)
        {
            if (collision.gameObject.tag == "Wall")
            {
                hasHit = true;
                Despawn();
            }
        }
    }

    #endregion
}
