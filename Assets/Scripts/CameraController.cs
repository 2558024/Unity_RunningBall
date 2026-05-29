using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    private float zDistance;

    void Awake()
    {
        if (target != null)
        {
            zDistance = target.position.z - transform.position.z;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 position = transform.position;
        position.z = target.position.z - zDistance;
        transform.position = position;
    }
}
