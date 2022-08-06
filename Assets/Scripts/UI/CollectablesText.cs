using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectablesText : MonoBehaviour
{
    public PlayerStats playerStats;
    public int totalCollectables;

    private MoveTexts _parent;
    private Text _text;
    private float _colorDuration = 0.5f;
    private float _colorRatio;
    private float _updateCollectablesTimer;
    private int _colorStages;
    private int _currentCollectables;

    private void Start()
    {
        _parent = gameObject.GetComponentInParent<MoveTexts>();
        _text = gameObject.GetComponent<Text>();
        if (totalCollectables >= 10) _text.text = "00 / " + totalCollectables;
        else _text.text = "00 / 0" + totalCollectables;
    }

    private void Update()
    {
        ShowCollectables();
        ChangeColor();
    }

    private void ShowCollectables()
    {
        if (_currentCollectables != playerStats.collectables) _parent.active = true;

        if (_parent.active)
        {
            _updateCollectablesTimer += Time.deltaTime;

            if (_currentCollectables != playerStats.collectables)
            {
                if (_parent.StayTime >= 3) _parent.ReadyToBack = true;
                else _parent.StayTime = 0;
            }

            if (_parent.ReadyToBack)
            {
                _parent.TimeToDisappear -= Time.deltaTime;

                if (_parent.TimeToDisappear <= 0)
                {
                    _parent.ReadyToBack = false;
                    _parent.StayTime = 0;
                }
            }

            if (_updateCollectablesTimer >= 1.4f) _currentCollectables = playerStats.collectables;
        }
        else _updateCollectablesTimer = 0;

        if (totalCollectables >= 10)
        {
            if (_currentCollectables < 10) _text.text = "0" + _currentCollectables + " / " + totalCollectables;
            else _text.text = _currentCollectables + " / " + totalCollectables;
        }
        else
        {
            if (_currentCollectables < 10) _text.text = "0" + _currentCollectables + " / 0" + totalCollectables;
            else _text.text = _currentCollectables + " / 0" + totalCollectables;
        }
    }

    private void ChangeColor()
    {
        if (_colorRatio < 1) _colorRatio += Time.deltaTime / _colorDuration;

        if (_colorStages == 0) LerpColor(Color.red, Color.yellow, 1);
        if (_colorStages == 1) LerpColor(Color.yellow, Color.green, 2);
        if (_colorStages == 2) LerpColor(Color.green, Color.cyan, 3);
        if (_colorStages == 3) LerpColor(Color.cyan, Color.blue, 4);
        if (_colorStages == 4) LerpColor(Color.blue, Color.magenta, 5);
        if (_colorStages == 5) LerpColor(Color.magenta, Color.red, 0);
    }

    private void LerpColor(Color colorA, Color colorB, int nextColorStage)
    {
        colorA.a = 0.5f;
        colorB.a = 0.5f;
        _text.material.color = Color.Lerp(colorA, colorB, _colorRatio);

        if (_colorRatio >= 1)
        {
            _colorStages = nextColorStage;
            _colorRatio = 0;
        }
    }
}