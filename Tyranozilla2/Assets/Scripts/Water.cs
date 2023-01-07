using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private GameObject m_boatPrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();

        if(player != null)
        {
            player.GameOver();
        }
        else if(enemy != null)
        {
            var boat = Instantiate(m_boatPrefab);
            boat.transform.position = enemy.transform.position;
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }

    }
}
