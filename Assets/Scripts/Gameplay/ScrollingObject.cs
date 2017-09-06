using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
	private Rigidbody2D _rigidBody;

	void Start () 
	{

        int velocityFactor = (PlayerManager.Speed < 20) ? PlayerManager.Speed : 20;
        if (velocityFactor <= 1)
            velocityFactor = 1;

        double finalSpeed = GameControl.instance.scrollSpeed*(1 + 0.06*velocityFactor);

        _rigidBody = GetComponent<Rigidbody2D>();
		_rigidBody.velocity = new Vector2 ((float)finalSpeed, 0);
	}

	void Update()
	{
		if(GameControl.instance.gameOver == true)
		{
			_rigidBody.velocity = Vector2.zero;
		}
	}
}
