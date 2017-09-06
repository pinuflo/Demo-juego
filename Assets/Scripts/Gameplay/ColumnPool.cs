using UnityEngine;
using System.Collections;

public class ColumnPool : MonoBehaviour 
{
	public GameObject columnPrefab;									//Column Gameobject
    public GameObject coinPrefab;                                   //Coin Gameobject
    public GameObject bombPrefab;                                   //Bomb Gameobject
    public GameObject fishPrefab;                                   //Fish Gameobject

    public int columnPoolSize = 12;									
	public float spawnRate = 2f;									
	public float columnMin = -1f;									
	public float columnMax = 3.5f;									

	private GameObject[] columns;									
	private int currentColumn = 0;									

	private Vector2 objectPoolPosition = new Vector2 (-15,-25);		
	private float spawnXPosition = 10f;

	private float timeSinceLastSpawned;


    void Awake()
    {


    }

    void Start()
	{
		timeSinceLastSpawned = 0f;
		columns = new GameObject[columnPoolSize];
		for(int i = 0; i < columnPoolSize; i++)
		{
			columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
		}
	}


	void Update()
	{

        int velocityFactor = (PlayerManager.Speed < 20) ? PlayerManager.Speed : 20;
        if (velocityFactor <= 1)
            velocityFactor = 1;

        float multiplier = (float)(1 + 0.06 * velocityFactor);
        timeSinceLastSpawned += Time.deltaTime* multiplier;

        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate) 
		{	
			timeSinceLastSpawned = 0f;

			float spawnYPosition = Random.Range(columnMin, columnMax);
			columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
			currentColumn ++;

			if (currentColumn >= columnPoolSize) 
			{
				currentColumn = 0;
			}



            // Lógica para crear monedas y bombas de bomba aleatorea 
            float spawnYPositionCoin;
            Vector2 auxPos;
            GameObject aux;

            int random = Random.Range(0, 8);
            if( random < 5 )
            {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 1.0f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(coinPrefab, auxPos, Quaternion.identity);
            }

            random = Random.Range(0, 10);
            if (random < 8)
            {
                if (random < 6)
                {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 2.0f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(coinPrefab, auxPos, Quaternion.identity);
                }
                else if (random == 7)
                {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 2.0f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(fishPrefab, auxPos, Quaternion.identity);
                }
                else
                {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 2.0f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(bombPrefab, auxPos, Quaternion.identity);
                }
            }

            random = Random.Range(0, 10);
            if (random < 8)
            {
                if (random < 6)
                {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 3.5f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(coinPrefab, auxPos, Quaternion.identity);
                }
                else if (random == 7)
                {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 2.0f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(fishPrefab, auxPos, Quaternion.identity);
                }
                else
                {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 3.5f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(bombPrefab, auxPos, Quaternion.identity);
                }
            }

            random = Random.Range(0, 15);
            if (random < 7)
            {
                    spawnYPositionCoin = Random.Range(columnMin, columnMax);
                    auxPos = new Vector2(spawnXPosition + 4.5f, spawnYPositionCoin);
                    aux = (GameObject)Instantiate(bombPrefab, auxPos, Quaternion.identity);
            }


        }
	}
}