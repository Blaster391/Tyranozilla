using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Grabable
{
    private Player m_player = null;

    [SerializeField]
    private GameObject m_bulletPrefab = null;

    [SerializeField]
    private float m_aggroRange = 10.0f;

    [SerializeField]
    private float m_attackCooldown = 1.0f;

    private float m_currentCooldown = 0.0f;

    private SpriteRenderer m_renderer = null;

    private GameMaster m_gameMaster = null;

    public override void Use(Vector2 _targetPosition)
    {

    }

    protected void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_player = FindObjectOfType<Player>();
        m_gameMaster = FindObjectOfType<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_held || m_gameMaster.IsGameOver())
        {
            return;
        }

        Vector3 directionToPlayer = m_player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if(distanceToPlayer < m_aggroRange)
        {
            m_currentCooldown -= Time.deltaTime;
            if(m_currentCooldown < 0.0f)
            {
                m_currentCooldown = m_attackCooldown;

                directionToPlayer.Normalize();

                var bullet = Instantiate(m_bulletPrefab);
                bullet.transform.position = transform.position + directionToPlayer * 0.5f;

                float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

            }


            m_renderer.flipX = (directionToPlayer.x > 0);
        }

    }
}
