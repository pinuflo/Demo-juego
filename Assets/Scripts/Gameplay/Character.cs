using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{
	public float upForce;					
	private bool isDead = false;			

	private Animator anim;			
	private Rigidbody2D rb2d;				
    public Camera camera;
    public GameObject explotionFx;

	void Start()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{

		if (isDead == false && GameControl.instance.gameOver == false) 
		{

            //se genera un vector unitario en la direccion entre la estrella y el mouse
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 auxDir = camera.ScreenToWorldPoint(mousePos);
            auxDir = auxDir - this.gameObject.transform.position;
            auxDir = auxDir.normalized;

            anim.SetTrigger("Flap");

            //Spin corto
            if (Input.GetMouseButtonDown(0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(400f * auxDir.x, 400f * auxDir.y));
                StartCoroutine(StopSpin());
            }

            //Spin largo
            if (Input.GetMouseButtonDown(1))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(1200f*auxDir.x, 1200f * auxDir.y));
                StartCoroutine(StopSpin());
            }

        }
	}

    IEnumerator StopSpin()
    {
        yield return new WaitForSeconds(0.1F);
        rb2d.velocity = Vector2.zero;
    }

	void OnCollisionEnter2D(Collision2D other)
	{

        //Al colisionar con una moneda se gana puntaje, sino, se muerre
        if (other.gameObject.tag == "Coin" )
        {
            Destroy(other.gameObject);
            GameControl.instance.Score();

        }
        else
        {
            if (other.gameObject.tag != "Wall" && other.gameObject.tag != "Scenery")
            {
                Instantiate(explotionFx, other.gameObject.transform.position, Quaternion.identity);
            }
            

            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameControl.instance.Die();
        }

	
     
	}
}
