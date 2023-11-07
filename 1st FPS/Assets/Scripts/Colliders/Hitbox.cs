using System.Collections.Generic;
using UnityEngine;

	public enum EAgentType
	{
		Ally,
		Enemy,
		Neutral,
		Count
	}
public class Hitbox : MonoBehaviour
{
	[field:SerializeField] protected bool CanHit { get; set; }
	[field:SerializeField] protected bool CanReceiveHit { get; set; }
	[field: SerializeField] protected EAgentType AgentType { get; set; } = EAgentType.Count;
	[field: SerializeField] protected List<EAgentType> AffectedAgents { get; set; }

    // //////////////////////////

    public bool HasBeenHit { get; private set; } = false;

    private const float HIT_EXIT_TIMER = 0.5f;
    private float m_currentTimer = 0.0f;
    private bool m_activeTimer = false;

    private void Update()
    {
        if (m_activeTimer)
        {
            if (m_currentTimer <= 0)
            {
                HasBeenHit = false;
                m_activeTimer = false;
                return;
            }

            m_currentTimer -= Time.deltaTime;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
		var otherHitbox = collision.gameObject.GetComponent<Hitbox>();

        if (otherHitbox == null)
        {
            return;
        }

        if (CanGetHit(otherHitbox))
        {
            //Vector3 contactPoint = collision.GetContact(0).point;
            //FXManager.Instance.OnHit(AgentType, contactPoint);

            if (collision.gameObject.GetComponentInParent<EnemyHit>() != null && 
                HasBeenHit == false)
            {
                HasBeenHit = true;
                m_activeTimer = true;
                m_currentTimer = HIT_EXIT_TIMER;
            }

        }
    }

    protected bool CanGetHit(Hitbox otherHitbox)
	{
		return CanHit &&
            otherHitbox.CanReceiveHit && 
			AffectedAgents.Contains(otherHitbox.AgentType);
	}
}
