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

    [SerializeField]
    private GameObject m_leftHolder = null;

    [SerializeField]
    private GameObject m_rightHolder = null;

    [SerializeField]
    private Grabable m_heldObject = null;

    [SerializeField]
    private float m_useCooldown = 0.5f;

    [SerializeField]
    private float m_grabRange = 2.0f;
    [SerializeField]
    private float m_pushForce = 20.0f;

    private float m_currentUseCooldown = 0.0f;
    private Vendor m_currentVendor = null;

    private float m_currentAnimationTick = 0.0f;
    private int m_currentAnimationFrame = 0;

    private Rigidbody2D m_rigidbody = null;
    private SpriteRenderer m_renderer = null;

    public bool IsFacingLeft()
    {
        return !m_renderer.flipX;
    }

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

        if(m_heldObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PushObjects();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                PickupObject();
            }
        }
        else
        {
            m_currentUseCooldown -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && m_currentUseCooldown < 0.0f)
            {
                m_heldObject.Use();
                m_currentUseCooldown = m_useCooldown;
            }
            else if(Input.GetMouseButtonDown(1))
            {
                ThrowObject();
            }
        }

        if(m_heldObject != null)
        {
            if(IsFacingLeft())
            {
                m_heldObject.transform.position = m_leftHolder.transform.position;
            }
            else
            {
                m_heldObject.transform.position = m_rightHolder.transform.position;
            }
        }
    }

    private void PickupObject()
    {
        var objects = FindObjectsInRange();
        Grabable closest = null;
        float closestDistance = 0.0f;

        foreach (var grabable in objects)
        {
            float distance = (grabable.transform.position - transform.position).sqrMagnitude;
            if(closest == null || closestDistance > distance)
            {
                closest = grabable;
                closestDistance = distance;
            }
        }

        if(closest != null)
        {
            m_heldObject = closest;
            closest.OnPickup();
        }
        else if(m_currentVendor != null)
        {
            m_currentVendor.Vend(this);
        }
    }

    private void PushObjects()
    {
        var objects = FindObjectsInRange();
        foreach(var grabable in objects)
        {
            Rigidbody2D targetRigidbody = grabable.GetComponent<Rigidbody2D>();
            if(grabable.transform.position.x > transform.position.x)
            {
                targetRigidbody.AddForce(Vector2.right * m_pushForce, ForceMode2D.Impulse);
            }
            else
            {
                targetRigidbody.AddForce(Vector2.left * m_pushForce, ForceMode2D.Impulse);
            }
        }

    }

    private List<Grabable> FindObjectsInRange()
    {
        List<Grabable> results = new List<Grabable>();

        var overlaps = Physics2D.OverlapCircleAll(transform.position, m_grabRange);
        foreach(var overlap in overlaps)
        {
            Grabable grabbable = overlap.GetComponent<Grabable>();
            if(grabbable != null)
            {
                results.Add(grabbable);
            }
        }    


        return results;
    }

    private void ThrowObject()
    {
        m_heldObject.OnDrop();

        Vector2 throwDirection = Vector2.zero;

        if(throwDirection.x > 0.0f)
        {
            m_heldObject.transform.position = transform.position + Vector3.right * 2.0f;
        }
        else
        {
            m_heldObject.transform.position = transform.position + Vector3.left * 2.0f;
        }
        

        m_heldObject = null;
    }

    public void SetVendor(Vendor _vendor)
    {
        m_currentVendor = _vendor;
    }
}
