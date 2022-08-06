using UnityEngine;
using System.Collections;

public class LoseLives : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject[] allLives = new GameObject[3];
    public GameObject[] allLivesDestroyed = new GameObject[3];
    public bool bossLevel;

    private MoveTexts _parent;
    private bool _livesDestroyed;
    private int _lives;

    private void Start ()
    {
        _parent = gameObject.GetComponentInParent<MoveTexts>();
        _lives = playerStats.lives;
    }

	private void FixedUpdate ()
    {
        LoseLife();
	}

    private void LoseLife()
    {
        if (!bossLevel)
        {
            if (_lives != playerStats.lives)
            {
                if (playerStats.lives >= 0)
                {
                    _lives = playerStats.lives;

                    if (_lives == 2) StartCoroutine(DestroyLife(2));
                    if (_lives == 1) StartCoroutine(DestroyLife(1));
                    if (_lives == 0) StartCoroutine(DestroyLife(0));
                }
            }
        }
        if (bossLevel)
        {
            if (!_livesDestroyed)
            {
                StartCoroutine(DestroyLives());
                _livesDestroyed = true;
            }
        }
    }

    private void MakeAnimation(int index)
    {
        Destroy(allLives[index].gameObject);
        allLivesDestroyed[index].gameObject.SetActive(true);
        Destroy(allLivesDestroyed[index].gameObject, 4f);
    }

    IEnumerator DestroyLife(int index)
    {
        yield return new WaitForSeconds(3f);
        _parent.active = true;
        yield return new WaitForSeconds(2.9f);
        MakeAnimation(index);
        _parent.StayTime = 1f;
        StopCoroutine("DestroyLife");
    }

    IEnumerator DestroyLives()
    {
        yield return new WaitForSeconds(0.1f);
        _parent.active = true;
        yield return new WaitForSeconds(2.9f);
        for (int i = 0; i < allLives.Length; i++) MakeAnimation(i);
        _parent.StayTime = 1f;
        StopCoroutine(DestroyLives());
    }
}