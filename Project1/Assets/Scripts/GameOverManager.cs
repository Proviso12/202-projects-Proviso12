using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.text = "You lost. your score was "+ CollisionManager.playerScore;
        gameOverMenu.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
