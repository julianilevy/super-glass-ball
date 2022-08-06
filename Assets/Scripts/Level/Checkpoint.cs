using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public Checkpoint nextCheckpoint;
    public Material[] materials = new Material[2];
    public bool active;

    private ParticleSystem _particleSystem;
	
    public void Start ()
    {
        _particleSystem = gameObject.GetComponent<ParticleSystem>();
        _particleSystem.enableEmission = false;
    }

	public void Update ()
    {
        ManageCheckpoints();
	}

    public void ManageCheckpoints()
    {
        if (nextCheckpoint != null) if (nextCheckpoint.active) active = false;

        if (active)
        {
            _particleSystem.enableEmission = true;
            gameObject.GetComponent<Renderer>().sharedMaterial = materials[K.CHECKPOINTARRAY_SAVED];
        }
        else
        {
            _particleSystem.enableEmission = false;
            gameObject.GetComponent<Renderer>().sharedMaterial = materials[K.CHECKPOINTARRAY_NORMAL];
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == K.LAYER_PLAYER)
        {
            Player hitPlayer;
            hitPlayer = collider.GetComponent<Player>();

            if (hitPlayer is Player)
            {
                hitPlayer.playerStats.lastCheckpoint = this;
                active = true;
            }
        }
    }
}
