using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    public static int selectedWeapon = 0;

    public Text displayAmmo;

    public Animator animator;

    public GameObject primaryIcon, secondaryIcon, meleeIcon;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
		{
            selectedWeapon = 0;
		}
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        if (previousSelectedWeapon != selectedWeapon)
		{
            SelectWeapon();
		}
    }


    void SelectWeapon()
	{
        int i = 0;

        foreach (Transform weapon in transform)
		{
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);

                // Disable all animations to avoid animation bugs when switching weapon
                animator.SetBool("Reloading", false);
                animator.SetBool("Shooting_AR", false);
                animator.SetBool("Shooting_Pistol", false);
                animator.SetBool("Aiming_AR", false);
                animator.SetBool("Running_AR", false);
                animator.SetBool("Walking_AR", false);
                animator.SetBool("Walking_Pistol", false);
                animator.SetBool("Running_Pistol", false);
                animator.SetBool("Aiming_Pistol", false);

                if (selectedWeapon == 0)
				{
                    primaryIcon.SetActive(true);
                    secondaryIcon.SetActive(false);
                    meleeIcon.SetActive(false);

                    int currentAmmo = AR_Weapon.currentAmmo;
                    int ammoInMagazine = AR_Weapon.ammoInMagazine;

                    displayAmmo.text = currentAmmo + "/" + ammoInMagazine;
				}else if (selectedWeapon == 1)
				{
                    primaryIcon.SetActive(false);
                    secondaryIcon.SetActive(true);
                    meleeIcon.SetActive(false);

                    int currentAmmo = SecondaryGun.currentAmmo;
                    int ammoInMagazine = SecondaryGun.ammoInMagazine;

                    displayAmmo.text = currentAmmo + "/" + ammoInMagazine;
                }else if (selectedWeapon == 2)
				{
                    primaryIcon.SetActive(false);
                    secondaryIcon.SetActive(false);
                    meleeIcon.SetActive(true);

                    displayAmmo.text = "";
				}
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i++;
		}
	}
}
