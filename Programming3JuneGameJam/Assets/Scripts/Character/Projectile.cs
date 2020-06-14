using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Fields

    public ProjectileType type;
    public bool targetIsPlayer;

    [NonSerialized] public Faction ownerFaction;

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

    public void Push(Vector3 direction)
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
            var characterHit = other.GetComponent<Character>();
            if (characterHit != null && characterHit.faction.IsEnemy(ownerFaction))
            {
                characterHit.HP -= type.damage;
                hasHit = true;
                Despawn();
            }
        }
    }

    #endregion
}
