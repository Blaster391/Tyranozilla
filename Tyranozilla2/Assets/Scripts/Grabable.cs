using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grabable : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Collider2D m_collider;

    protected virtual void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<Collider2D>();
    }

    public void OnPickup()
    {
        m_rigidbody.simulated = false;
    }

    public void OnDrop()
    {
        m_rigidbody.simulated = true;
    }

    public abstract void Use(Vector2 _targetPosition);
}
