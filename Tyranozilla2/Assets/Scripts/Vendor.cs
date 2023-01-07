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

        Grabable grabable = newObject.GetComponent<Grabable>();
        if (grabable != null)
        {
            _player.SetHeldObject(grabable);
        }

        float targetX = _player.GetTargetPosition().x;
        targetX -= _player.transform.position.x;

        newObject.transform.position = _player.transform.position + Vector3.right * targetX;
    }
}
