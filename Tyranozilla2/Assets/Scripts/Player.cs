using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_movementForce = 10.0f;

    [SerializeField]
    private float m_maxSpeed = 10.0f;

    [SerializeField]
    private float m_jumpForce = 10.0f;

    [SerializeField]
    private float m_animationTick = 1.0f;

    [SerializeField]
    private Sprite m_stationarySprite = null;
    [SerializeField]
    private List<Sprite> m_movementSprites = null;

    private float m_currentAnimationTick = 0.0f;
    private int m_currentAnimationFrame = 0;

    private Rigidbody2D m_rigidbody = null;
    private SpriteRenderer m_renderer = null;
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 lateralDirection = Vector2.zero;
        if(Input.GetKey(KeyCode.A))
        {
            lateralDirection += Vector2.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            lateralDirection += Vector2.right;
        }

        m_rigidbody.AddForce(lateralDirection * m_movementForce * Time.deltaTime);

        Vector2 verticalDirection = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalDirection += Vector2.up;
        }

        m_rigidbody.AddForce(verticalDirection * m_jumpForce, ForceMode2D.Impulse);

        Vector2 velocity = m_rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -m_maxSpeed, m_maxSpeed);
        m_rigidbody.velocity = velocity;

        if(velocity.x < -0.1f)
        {
            m_renderer.flipX = false;
        }
        else if (velocity.x > 0.1f)
        {
            m_renderer.flipX = true;
        }

        if (lateralDirection == Vector2.zero)
        {
            m_renderer.sprite = m_stationarySprite;
        }
        else
        {
            m_currentAnimationTick += Time.deltaTime;
            if(m_currentAnimationTick > m_animationTick)
            {
                m_currentAnimationTick = 0.0f;
                m_currentAnimationFrame++;
                if(m_currentAnimationFrame >= m_movementSprites.Count)
                {
                    m_currentAnimationFrame = 0;
                }
            }

            m_renderer.sprite = m_movementSprites[m_currentAnimationFrame];
        }
    }
}
