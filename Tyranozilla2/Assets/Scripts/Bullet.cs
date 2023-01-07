using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 4.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (transform.right * m_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.TakeDamage();
        }

        Destroy(gameObject);
    }
}
