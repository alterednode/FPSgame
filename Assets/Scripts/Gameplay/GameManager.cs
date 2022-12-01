using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Linq;// for checking contains()

public class GameManager : MonoBehaviour
{

    public int score = 0;
    public float spawnRate = 10.0f;

	public GameObject player;
    public GameObject stationaryEnemy;
    public GameObject missliePrefab;
    public GameObject bulletPrefab;

    public bool isGameActive = false;

    
    // Start is called before the first frame update
    void Start()
	{
    	
    }

    private void Update()
    {
        if (isGameActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }



    public void GameStart() //what to do when the game starts
    {

    }

    public void GameOver()
    {
       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
        
    }
}