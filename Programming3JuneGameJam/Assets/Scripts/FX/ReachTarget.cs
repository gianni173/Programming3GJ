using UnityEngine;

public class ReachTarget : MonoBehaviour
{
    #region Fields

    public Transform target;

    [SerializeField] private float speed;

    #endregion

    #region UnityCallbacks

    private void Update()
    {
        if (target)
        {
            transform.LookAt(target.position);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target)
        {
            if (other.gameObject == target.gameObject)
            {
                GetComponent<ParticleSystem>().Stop();
                target = null;
            }
        }
    }

    #endregion

    #region Methods

    public void SetTarget(Transform t)
    {
        target = t;
    }

    #endregion
}