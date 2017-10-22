using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenTrap : MonoBehaviour {
	private float m_timePassed;
	private bool m_renderAndPhysics;

	// Use this for initialization
	void Start () {
		m_timePassed = (float) Random.Range (0, 3);
		m_renderAndPhysics = true;
	}

	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(0.0f, 60.0f * Time.deltaTime, 0.0f));


		m_timePassed += Time.deltaTime;

		if (m_timePassed >= 3.0f) 
		{			
			m_timePassed = 0.0f;

			var playerCollider = GameObject.Find("Player").GetComponent<Collider>();
			var trapCollider = GetComponentInParent<Collider>();	
			Physics.IgnoreCollision (playerCollider, trapCollider, !m_renderAndPhysics );

			GetComponent<Renderer> ().enabled = m_renderAndPhysics;


			m_renderAndPhysics = !m_renderAndPhysics;
		}


	}
}
