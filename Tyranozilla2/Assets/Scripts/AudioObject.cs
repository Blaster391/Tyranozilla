using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    AudioSource m_audioSource;

    GameObject m_trackingObject = null;

    public void SetTrackingObject(GameObject _obj)
    {
        m_trackingObject = _obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_audioSource.isPlaying)
        {
            Destroy(gameObject);
        }

        if(m_trackingObject != null)
        {
            transform.position = m_trackingObject.transform.position;
        }
    }
}
