using UnityEngine;
using System.Collections;

public class EndBall : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameDataManager gameDataManager;
    public FadeScreen fadeScreen;
    public ParticleSystem[] fireworks = new ParticleSystem[2];
    public Material[] materials = new Material[2];
    public bool active;

    private ParticleSystem _particleSystem;
	
    public void Start ()
    {
        fireworks[0].enableEmission = false;
        fireworks[1].enableEmission = false;
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _particleSystem.enableEmission = false;
    }

	public void Update ()
    {
        ManageMaterials();
	}

    public void ManageMaterials()
    {
        if (active)
        {
            _particleSystem.enableEmission = true;
            gameObject.GetComponent<Renderer>().sharedMaterial = materials[K.CHECKPOINTARRAY_SAVED];
            fireworks[0].enableEmission = true;
            fireworks[1].enableEmission = true;
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (!active)
        {
            if (collider.gameObject.layer == K.LAYER_PLAYER)
            {
                Player hitPlayer;
                hitPlayer = collider.GetComponent<Player>();

                if (hitPlayer is Player)
                {
                    playerStats.levelCompleted = true;
                    active = true;
                    StartCoroutine(GoToFadeScreen());
                }
            }
        }
    }

    public IEnumerator GoToFadeScreen()
    {
        yield return new WaitForSeconds(5f);
        gameDataManager.Save();
        fadeScreen.ActivateFade();
        StopCoroutine(GoToFadeScreen());
    }
}
