using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTrap : MonoBehaviour
{
    private float m_timePassed;
    private bool m_renderAndPhysics;
    private bool m_hidden;

    private float m_translateYStart;
    //private float m_translateYEnd;

    // Use this for initialization
    void Start()
    {
        m_timePassed = (float) Random.Range(0, 3);
        m_renderAndPhysics = true;
        m_hidden = false;
        m_translateYStart = this.transform.position.y;
        Debug.Log(m_translateYStart);
        //m_translateYEnd = m_translateYStart + 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_translateYStart > 2.0f)
        {
            m_translateYStart = this.transform.position.y;
        }
        if (m_translateYStart == 2.0f)
        {
            //this.transform.Rotate(new Vector3(0.0f, 60.0f * Time.deltaTime, 0.0f));
            this.transform.Rotate(new Vector3(0.0f, 0.0f, 60.0f * Time.deltaTime));


            m_timePassed += Time.deltaTime;

            float t = (3.0f - m_timePassed) / 3.0f;
            t = Mathf.Clamp(t, 0.0f, 1.0f);

            if (m_timePassed >= 3.0f)
            {
                m_timePassed = 0.0f;

                //var playerCollider = GameObject.Find("Player").GetComponent<Collider>();
                //var trapCollider = GetComponentInParent<Collider>();	
                //Physics.IgnoreCollision (playerCollider, trapCollider, !m_renderAndPhysics );

                //GetComponent<Renderer> ().enabled = m_renderAndPhysics;

                m_renderAndPhysics = !m_renderAndPhysics;
                m_hidden = !m_hidden;
            }


            {
                Vector3 pointCurrent = new Vector3(this.transform.position.x, this.transform.position.y,
                    this.transform.position.z);

                float param = 4 * Mathf.Abs(Mathf.Sin(Time.time));
                pointCurrent.y = m_translateYStart + param;

                this.transform.position = pointCurrent;
            }
        }
    }
}