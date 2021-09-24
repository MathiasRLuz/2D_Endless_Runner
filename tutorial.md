# Game Project Configuration
## File > Build Settings > Switch platform to WebGL
## Game > 16:9 Aspect
## Folder Creation
### Scripts
### Sprites
### Prefab

# Game Assets Creation
## Player
## Obstacles
## Background

# Player Controller
## Empty Game Object Player (Parent)
## Player Sprite (Child)
## Add Rigid Body 2D to parent
### Gravity Scale = 0
## Scripts > New > Player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;
    private Vector2 playerDirection;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(0, directionY).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, playerDirection.y * playerSpeed);    
    }
}

## Add Script to parent
## Set player speed 
## test

# Camera Movement
## Script CameraMovement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed = 15;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }
}

## Add script to Game Manager (parent of main camera and player)
## Add camera speed
## test

# Looping Background
## Add new game object > 3D > Quad > rename to Background > remove mesh collider
## Select the background sprite > Wrap mode = repeat
## Drag Background sprite to Background Inspector
## Shader > Unlit/Texture
## Make Background child of game manager
## Script LoopingBackground

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed;
    public Renderer backgroundRenderer;

    // Update is called once per frame
    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime,0);
    }
}

## attach the script to the background object and set background speed
## asign Mesh Renderer to Background Renderer

# Spawn Obstacles
## Script SpawnObjects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    public GameObject obstacle;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;

    // Start is called before the first frame update
    void Spawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Instantiate(obstacle, transform.position + new Vector3(randomX,randomY,0),transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }
}

## Create empty object named Spawn Point
## attach script to object
## Set variables min and max (X, Y) and set obstacle Prefab to obstacle object. X: 3 to 3.5
## determine Y range by dragging the obstacle in the screen
## Time between spawn = 0.6
## Position X = 25
## set spawn Point as child of game manager

# Prevent player off screen
## create an empty game object named borders
## Create an empty child name top border and add box collider, resize to fit the entire screen, move up
## Create an empty child name bottom border and add box collider, resize to fit the entire screen, move down

## Add box collider to player
## make Borders child of game manager

# Destroy obstacles
## Create side border on the left
## Hierarchy > Borders > Tag > Create new tag > Border
## Add this tag to all border objects

## Add collider to obstacle (Prefab)
## Check "Is trigger"
## new script named Obstacle
## add script to obstacle prefab
## add rigidbody 2d with gravity scale = 0 and freeze rotation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}

# Game over panel
## Create UI > panel > rename to "Game Over Panel"
## Change to black color
## Select canvas game object and change UI Scale mode to scale with screen size (X 1600 and Y 900)
## Create UI > Image > Drag sprite into source image > Scale up
## Create UI > Text > Write "Game Over" > Center alignment > resize > Increase font size > Set Color
## Create UI > Button > Add sprite to source image > resize > Change text to Restart > Increase font size > Set Color
## Disable Game Over Panel

## Obstacle script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }

        if (collision.tag == "Player")
        {
            Destroy(player.gameObject);
        }
    }
}

# Game Over 
## Create GameOver Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player")==null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

## add script to game mangaer and attach game over panel to it
## select restart button and add on click > add game manager to it and select GameOver.Restart function
## add "Player" tag to player

# SCORE
## Create new UI Text to canvas > rename to Score text > Text = 0
## New script > ScoreManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    private float score;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            score += 1 * Time.deltaTime;
            scoreText.text = ((int)score).ToString();
        }        
    }
}

## Add script to score text
## add text to scoreText variable

# Background Music
## New Script > BackgroundMusic

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic backgroundMusic;

    void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

## New empty gameObject named backgroundMusic
## add component > audio source
## add BackgroundMusic script
## Download music from asset store casual game bgm #5
## attach audio to audio clip in audio source and enable loop

# Particles
## Create Effects > Particle System > reset transform component
## make it child of game manager


