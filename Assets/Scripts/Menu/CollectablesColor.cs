using UnityEngine;
using System.Collections;

public class CollectablesColor : MonoBehaviour
{
    private Renderer _renderer;
    private float _colorDuration = 0.5f;
    private float _colorRatio;
    private int _colorStages;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        ChangeColor();
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
}
