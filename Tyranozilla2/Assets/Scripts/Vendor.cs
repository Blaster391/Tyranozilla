using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField]
    private GameObject m_prefab;

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
}
