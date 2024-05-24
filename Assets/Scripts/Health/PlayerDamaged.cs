using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamaged : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Transform respawnPoint;
    public float playerHealth = 100f;
    public GameObject onHitScreen;
    int dmg = 10;

    // Timer to track collision time
    float collisionTime;
    // Time before damage is taken, 1 second default
    public float collisionThreshold = 1f;

    // colliding with the enemy
    bool isColliding = false;

    private int lives = 3;

    public GameObject Life1;
    public GameObject Life2;
    public GameObject Life3;

    private GameObject enemyColliding;


    private Vector3 lootInitialPosition;
    private Quaternion lootInitialRotation;

    public GameObject medkit;
    private bool isLootPicked = false;


    // Start is called before the first frame update
    void Start()
    {
        //print("Player Health: "+playerHealth);
    }

    void Update()
    {
        if (onHitScreen.GetComponent<Image>().color.a > 0)
        {
            var color = onHitScreen.GetComponent<Image>().color;
            color.a -= 0.01f;
            onHitScreen.GetComponent<Image>().color = color;
        }


        if (playerHealth <= 0)
        {
            lives--;
            Player.transform.position = respawnPoint.transform.position;
            playerHealth = 100;
        }

        if (lives == 2)
		{
            Life3.SetActive(false);
		}else if (lives == 1)
		{
            Life3.SetActive(false);
            Life2.SetActive(false);
        }else if (lives == 0)
		{
            Life3.SetActive(false);
            Life2.SetActive(false);
            Life1.SetActive(false);
        }

        if (lives == 0)
		{
            SceneManager.LoadScene("Game Over");
		}

        // player getting damaged every second when colliding with the enemy
        if (isColliding && enemyColliding != null){

            // If the time is below the threshold, add one second
            if (collisionTime < collisionThreshold) {
                collisionTime += Time.deltaTime;
            } else {
                var color = onHitScreen.GetComponent<Image>().color;
                color.a = 0.8f;
                onHitScreen.GetComponent<Image>().color = color;

                // Time is over theshold, player takes damage
                playerHealth = playerHealth - dmg;

                print("Player Health = " + playerHealth);

                // Reset timer
                collisionTime = 0f;
            }
        }
    }

    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Enemy")
        {
            enemyColliding = obj.gameObject;

            var color = onHitScreen.GetComponent<Image>().color;
            color.a = 0.8f;
            onHitScreen.GetComponent<Image>().color = color;

            playerHealth = playerHealth - dmg;
            //print("You took damage! Health: " + playerHealth);

            collisionTime = 0f;

            isColliding = true;
        }

        if (obj.gameObject.tag == "Poison")
		{
            var color = onHitScreen.GetComponent<Image>().color;
            color.a = 0.8f;
            onHitScreen.GetComponent<Image>().color = color;

            playerHealth = playerHealth - 10;
        }
    }

    void OnCollisionExit(Collision obj)
    {
        if (obj.gameObject.tag == "Enemy")
        {
            isColliding = false;
        }
    }


	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Medkit"))
        {

            if (SceneManager.GetActiveScene().name == "Final Boss")
            {
                if (playerHealth != 100f)
                {
                    if (!isLootPicked)
                    {
                        isLootPicked = true;
                        lootInitialPosition = other.gameObject.transform.position;
                        lootInitialRotation = other.gameObject.transform.rotation;

                        FindObjectOfType<AudioManager>().Play("Healing");

                        playerHealth += 50f;

                        if (playerHealth > 100f)
                        {
                            playerHealth = 100f;
                        }


                        Destroy(other.gameObject);

                        StartCoroutine(RespawnLoot(medkit, 10f));
                    }
                }
            }
            else
            {
                if (playerHealth != 100f)
                {
                    FindObjectOfType<AudioManager>().Play("Healing");

                    playerHealth += 50f;

                    if (playerHealth > 100f)
                    {
                        playerHealth = 100f;
                    }

                    Destroy(other.gameObject);
                }
            }
        }
    }


    private IEnumerator RespawnLoot(GameObject loot, float sec)
    {
        yield return new WaitForSeconds(sec);

        Instantiate(loot, lootInitialPosition, lootInitialRotation);

        isLootPicked = false;
    }
}
