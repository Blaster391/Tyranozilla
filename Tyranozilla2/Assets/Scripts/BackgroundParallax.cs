using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField]
    private float m_parallaxDistance = 5.0f;
    [SerializeField]
    private float m_realDistance = 50.0f;

    [SerializeField]
    private GameObject m_zilla = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = m_zilla.transform.position.x;

        float offset = m_parallaxDistance / m_realDistance;
        float parallaxDistance = -xPos * offset;
        parallaxDistance = Mathf.Clamp(parallaxDistance, -m_parallaxDistance, m_parallaxDistance);

        Vector3 position = transform.localPosition;
        position.x = parallaxDistance;
        transform.localPosition = position;

    }
}
