using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){ 
            //QUIT GAME
            Application.Quit(); 
        }

        if (player != null)
        {
            if (player.hp <= 0)
            {
                //load death scene
                SceneManager.LoadSceneAsync(1);
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1) {
            if(Input.GetKey(KeyCode.R)){
                //Restart game when dead
                SceneManager.LoadSceneAsync(0);
            }
        }
    }
}
