using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	Animator animator;
	public float health;

	public GameObject healthUI;
	public Slider slider;

	void Start()
	{
		slider.value = CalculateHealth();

		animator = GetComponent<Animator>();
	}

	public void TakeDamage(float damage)
	{
		//animator.Play("Z_Damage", -1, 0f);

		slider.value = CalculateHealth();

		health -= damage;
		//Debug.Log("Enemy health = " + health);

		if (health <= 0f)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);
	}

	float CalculateHealth()
	{
		return health;
	}
}
