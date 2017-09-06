using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour 
{
	public static GameControl instance;			
	public Text scoreText, timeText;						
	public GameObject gameOvertext;				

	int score = 0, remaingTime = 30;					
	public bool gameOver = false;				
	public float scrollSpeed = -2.0f;


	void Awake()
	{
   
        if (instance == null)
			instance = this;
		else if(instance != this)
			Destroy (gameObject);

        remaingTime = PlayerManager.Duration;
        StartCoroutine(DecreaseTimeCounter());
	}

    IEnumerator DecreaseTimeCounter()
    {
        do
        {

            remaingTime = remaingTime - 1;
            timeText.text = remaingTime.ToString();
            yield return new WaitForSeconds(1f);
        } while (remaingTime > 0 && gameOver == false);

        if(gameOver==false)
            Die();

    }

	void Update()
	{
		if (gameOver && Input.GetMouseButtonDown(0)) 
		{
			SceneManager.LoadScene("MainScene");
		}
	}

	public void Score()
	{
		if (gameOver)	
			return;

        score = score + PlayerManager.PointsPerItem;
		scoreText.text = "Puntaje: " + score.ToString();
	}

	public void Die()
	{
        if(gameOver==false)
            ClientManager.SaveScore(score, PlayerManager.Token);
        gameOver = true;

        gameOvertext.SetActive (true);
		
        
	}
}
