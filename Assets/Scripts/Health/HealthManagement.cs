using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManagement : MonoBehaviour
{
    public Texture damage;
    public float health = 100;
    float maxHealth;
    Color alpha;
    private bool collide = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0 && collide == true)
            health -= Time.deltaTime;

        if (health >= 0 && collide == false)
            health += Time.deltaTime;

        if (health > maxHealth)
            health = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collide = true;
        }
    }

	private void OnTriggerExit(Collider other)
	{
        if (other.GetComponent<Collider>().gameObject.CompareTag("Player"))
        {
            collide = false;
        }
    }

	private void OnGUI()
	{
		if (health <= 100)
		{
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), damage);
		}
	}
}
