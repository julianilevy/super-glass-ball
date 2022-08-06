using UnityEngine;
using System.Collections;

public class GrabCollectable : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject brokenCollectable;

    private Renderer _renderer;
    private BoxCollider _boxCollider;
    private float _colorDuration = 0.5f;
    private float _colorRatio;
    private float _absorbTimer = 0.5f;
    private float _addToPlayerStatsTimer;
    private int _colorStages;
    private bool _startAbsorbing;
    private bool _endAbsorbing;
    private bool _grabbed;
    private bool _addedToPlayerStats;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        UpdatePlayerStats();
        Rotate();
        ChangeColor();
        Absorb();
    }

    private void UpdatePlayerStats()
    {
        if (_grabbed)
        {
            _addToPlayerStatsTimer += Time.deltaTime;

            if (!_addedToPlayerStats && _addToPlayerStatsTimer >= 0.5f)
            {
                _addedToPlayerStats = true;
                playerStats.collectables += 1;
            }
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.forward, 1);
    }

    private void ChangeColor()
    {
        if (_colorRatio < 1) _colorRatio += Time.deltaTime/_colorDuration;

        if (_colorStages == 0) LerpColor(Color.red, Color.yellow, 1);
        if (_colorStages == 1) LerpColor(Color.yellow, Color.green, 2);
        if (_colorStages == 2) LerpColor(Color.green, Color.cyan, 3);
        if (_colorStages == 3) LerpColor(Color.cyan, Color.blue, 4);
        if (_colorStages == 4) LerpColor(Color.blue, Color.magenta, 5);
        if (_colorStages == 5) LerpColor(Color.magenta, Color.red, 0);
    }

    private void Absorb()
    {
        if (_startAbsorbing) _absorbTimer -= Time.deltaTime;

        if (!_endAbsorbing && _absorbTimer <= 0)
        {
            foreach (Transform child in brokenCollectable.transform) child.GetComponent<AbsorbCollectable>().StartDrift();
            Destroy(gameObject, 3f);
            _renderer.enabled = false;
            _boxCollider.enabled = false;
            _endAbsorbing = true;
        }
    }

    private void LerpColor(Color colorA, Color colorB, int nextColorStage)
    {
        colorA.a = 0.5f;
        colorB.a = 0.5f;
        _renderer.material.color = Color.Lerp(colorA, colorB, _colorRatio);

        if (_colorRatio >= 1)
        {
            _colorStages = nextColorStage;
            _colorRatio = 0;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == K.LAYER_PLAYER)
        {
            _startAbsorbing = true;
            _grabbed = true;
            brokenCollectable.SetActive(true);
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            foreach (Transform child in brokenCollectable.transform)
            {
                child.GetComponent<Renderer>().material.color = _renderer.material.color;
                child.GetComponent<Rigidbody>().AddExplosionForce(0.03f, child.transform.position, 0.01f);
            }
        }
    }
}
