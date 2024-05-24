using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour
{
    public GameObject sequence1;
    public GameObject sequence2;
    public GameObject sequence3;
    public GameObject sequence4;
    public GameObject sequence5;
    public GameObject sequence6;

    public GameObject darkScreen;

    public GameObject skip;

    float timer1 = 8;
    float timer2 = 8;
    float timer3 = 8;
    float timer4 = 8;
    float timer5 = 10;
    float timer6 = 10;

    bool isStoryOver = false;

    // Start is called before the first frame update
    void Start()
    {
        sequence1.SetActive(true);
        sequence2.SetActive(false);
        sequence3.SetActive(false);
        sequence4.SetActive(false);
        sequence5.SetActive(false);
        sequence6.SetActive(false);

        darkScreen.SetActive(false);

        skip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer1 -= Time.deltaTime;

        if (timer1 < 0)
		{
            skip.SetActive(true);

            sequence1.SetActive(false);
            sequence2.SetActive(true);

            timer2 -= Time.deltaTime;

            if (timer2 < 0)
			{
                sequence2.SetActive(false);
                sequence3.SetActive(true);

                timer3 -= Time.deltaTime;

                if (timer3 < 0)
                {
                    sequence3.SetActive(false);
                    sequence4.SetActive(true);

                    timer4 -= Time.deltaTime;

                    if (timer4 < 0)
                    {
                        sequence4.SetActive(false);
                        sequence5.SetActive(true);

                        timer5 -= Time.deltaTime;

                        if (timer5 < 0)
                        {
                            sequence5.SetActive(false);
                            sequence6.SetActive(true);

                            timer6 -= Time.deltaTime;

                            if (timer6 < 0)
                            {
                                SceneManager.LoadScene("Level 1");

                                /*if (!isStoryOver)
                                    StartCoroutine(FadeLoadingScreen());*/
                            }
                        }
                    }
                }
            }
        }
    }

    public void SkipStory()
	{
        SceneManager.LoadScene("Level 1");
    }


/*    IEnumerator FadeLoadingScreen()
    {
        isStoryOver = true;

        darkScreen.SetActive(true);

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        yield return new WaitForSeconds(7);

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 5;
            yield return null;
        }

        canvasGroup.interactable = false;

        if (canvasGroup.alpha == 0)
        {
            SceneManager.LoadScene("Level 1");
        }

        yield return null;
    }*/
}
