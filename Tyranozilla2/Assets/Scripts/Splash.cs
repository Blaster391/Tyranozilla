using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField]
    private float m_lifeTime = 0.25f;

    private float m_timeLeft = 0.0f;
    
    void Start()
    {
        m_timeLeft = m_lifeTime;

        var results = Physics2D.RaycastAll(transform.position, Vector2.down);
        foreach (var result in results)
        {
            if (result.collider.GetComponent<Grabable>() == null && result.collider.GetComponent<Player>() == null && result.collider.GetComponent<Shop>() == null && result.collider.GetComponent<Vendor>() == null && result.collider.GetComponent<Plant>() == null)
            {
                transform.position = result.point;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_timeLeft -= Time.deltaTime;

        if(m_timeLeft < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
