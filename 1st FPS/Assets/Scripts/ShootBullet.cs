using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private float m_bulletImpulse = 100.0f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(m_bulletPrefab, transform);
            var rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * m_bulletImpulse, ForceMode.Impulse);
        }
    }
}
