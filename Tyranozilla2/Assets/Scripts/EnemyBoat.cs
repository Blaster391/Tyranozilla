using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoat : MonoBehaviour
{
    [SerializeField]
    private float m_cullRange = 100.0f;

    [SerializeField]
    private float m_speed = 5.0f;

    private SpriteRenderer m_spriteRenderer = null;

    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if(position.x > 0)
        {
            m_spriteRenderer.flipX = true;
            position.x += m_speed * Time.deltaTime;
        }
        else
        {
            position.x -= m_speed * Time.deltaTime;
        }

        transform.position = position;

        if (Mathf.Abs(position.x) > m_cullRange)
        {
            Destroy(gameObject);
        }
    }
}
