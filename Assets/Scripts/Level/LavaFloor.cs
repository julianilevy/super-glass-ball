using UnityEngine;
using System.Collections;

public class LavaFloor : MonoBehaviour
{
    public GameObject fire;
    public Material[] allMaterials = new Material[2];

    private GameObject[] _allFires = new GameObject[8];
    private Renderer _renderer;
    private float _fireTimer;
    private float _materialDuration = 5;
    private float _materialRatio;
    private bool _fireRepeating;
    private int _materialStages;
    private int _fireIndex;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = allMaterials[K.LAVAARRAY_YELLOW];
    }

	private void Update ()
    {
        ChangeMaterial();
        SpawnFire();
        ControlAllFires();
	}

    private void ChangeMaterial()
    {
        if (_materialRatio < 1) _materialRatio += Time.deltaTime / _materialDuration;

        if (_materialStages == 0) LerpMaterial(allMaterials[K.LAVAARRAY_YELLOW], allMaterials[K.LAVAARRAY_RED], 1);
        if (_materialStages == 1) LerpMaterial(allMaterials[K.LAVAARRAY_RED], allMaterials[K.LAVAARRAY_YELLOW], 0);
    }

    private void LerpMaterial(Material materialA, Material materialB, int nextMaterialStage)
    {
        _renderer.material.Lerp(materialA, materialB, _materialRatio);

        if (_materialRatio >= 1)
        {
            _materialStages = nextMaterialStage;
            _materialRatio = 0;
        }
    }

    private void SpawnFire()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer >= 0.8f)
        {
            var newFire = Instantiate(fire, transform.position, fire.transform.rotation) as GameObject;
            newFire.transform.parent = gameObject.transform;
            newFire.transform.position = transform.position + new Vector3(Random.Range(-13, 13), 5.3f, Random.Range(-13, 13));
            AddToArray(_allFires, newFire, _fireIndex);
            _fireIndex++;
            _fireTimer = 0;
        }
    }

    private void ControlAllFires()
    {
        if (_fireIndex == _allFires.Length)
        {
            _fireRepeating = true;
            _fireIndex = 0;
        }

        if (_fireRepeating)
        {
            if(_allFires[_fireIndex] != null)
            {
                _allFires[_fireIndex].GetComponent<ParticleSystem>().enableEmission = false;
                Destroy(_allFires[_fireIndex].gameObject, 3.5f);
            }
        }
    }

    public void AddToArray(GameObject[] array, GameObject go, int index)
    {
        array[index] = go;
    }
}