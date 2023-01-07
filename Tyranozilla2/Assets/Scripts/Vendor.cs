using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    private GameObject m_vendPosition;

    public void Vend(Player _player)
    {
        var newObject = Instantiate(m_prefab);

        if(_player.IsFacingLeft())
        {
            newObject.transform.position = _player.transform.position + Vector3.left * 1.0f;
        }
        else
        {
            newObject.transform.position = _player.transform.position + Vector3.right * 1.0f;
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            player.SetVendor(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.SetVendor(null);
        }
    }
}
