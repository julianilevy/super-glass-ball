using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalText : MonoBehaviour
{
    public PlayerStats playerStats;
    public RealGerman realGerman;
    public GameObject finalBridge;
    public Image nicoChan;
    public GermanMoveTexts presentationBox;
    public Text presentationTextField;

    private string _trueFinalText;
    private string _trueFinalVictoryText;
    private float _writingSpeed = 3.5f;
    private bool _nicoChanAppears;
    private bool _nicoChanDisappears;
    private bool _finalBridgeON;

    public void Start()
    {
        nicoChan.color = new Color(255, 255, 255, 0);

        _trueFinalText = "¡¿Cómo has logrado llegar hasta aquí?! Es increíble que aún te funcione el botón de restart (que no existe)...           \nComo sea, tu suerte se termina acá, ¡Prepárate para morir!";
        _trueFinalVictoryText = "No puedo creerlo... Me has vencido... Arghh *Grito de dolor*";
    }

    public void Update()
    {
        NicoChan();
        MoveFinalBridge();
    }

    public void NicoChan()
    {
        if(_nicoChanAppears)
        {
            nicoChan.color += new Color(0, 0, 0, 0.01f);

            if (nicoChan.color.a >= 1)
            {
                nicoChan.color = new Color(255, 255, 255, 1);
                _nicoChanAppears = false;
                StartCoroutine(NicoChanTimer());
            }
        }

        if (_nicoChanDisappears)
        {
            nicoChan.color -= new Color(0, 0, 0, 0.01f);

            if (nicoChan.color.a <= 0)
            {
                nicoChan.color = new Color(255, 255, 255, 0);
                _nicoChanDisappears = false;
                nicoChan.gameObject.SetActive(false);
            }
        }
    }

    public void MoveFinalBridge()
    {
        if (_finalBridgeON)
        {
            finalBridge.transform.position -= Vector3.forward * 0.1f;
        }
    }

    public IEnumerator ActivateTrueFinalText01()
    {
        yield return new WaitForSeconds(2f);
        presentationTextField.text = "";
        presentationBox.active = true;
        yield return new WaitForSeconds(1f);
        _finalBridgeON = true;
        StartCoroutine(AnimateText(_trueFinalText, presentationTextField, "ActivateTrueFinalText02"));
        StopCoroutine(ActivateTrueFinalText01());
    }

    public IEnumerator ActivateTrueFinalText02()
    {
        yield return new WaitForSeconds(1.5f);
        presentationBox.RemoveText();
        yield return new WaitForSeconds(2f);
        presentationTextField.text = "";
        playerStats.locked = false;
        realGerman.ReadyToFight = true;
        StopCoroutine(ActivateTrueFinalText02());
    }

    public IEnumerator ActivateTrueFinalVictoryText()
    {
        yield return new WaitForSeconds(3f);
        presentationTextField.text = "";
        presentationBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_trueFinalVictoryText, presentationTextField, "DeactivateTrueFinalVictoryText"));
        StopCoroutine(ActivateTrueFinalVictoryText());
    }

    public IEnumerator DeactivateTrueFinalVictoryText()
    {
        yield return new WaitForSeconds(2.5f);
        presentationBox.RemoveText();
        yield return new WaitForSeconds(3f);
        presentationTextField.text = "";
        realGerman.ReadyToDisappear = true;
        realGerman.particleFire.enableEmission = false;
        realGerman.particleSteam.enableEmission = false;
        StopCoroutine(DeactivateTrueFinalVictoryText());
    }

    public IEnumerator ActivateFinalText03()
    {
        yield return new WaitForSeconds(0.5f);
        _nicoChanAppears = true;
        nicoChan.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        presentationBox.RemoveText();
        yield return new WaitForSeconds(1.5f);
        presentationTextField.text = "";
        StopCoroutine(ActivateFinalText03());
    }

    public IEnumerator NicoChanTimer()
    {
        yield return new WaitForSeconds(2.5f);
        _nicoChanDisappears = true;
        StopCoroutine(NicoChanTimer());
    }

    IEnumerator AnimateText(string completeText, Text textField, string nextCoroutine)
    {
        int i;
        textField.text = "";

        for (i = 0; i < completeText.Length; i++)
        {
            textField.text += completeText[i];
            yield return new WaitForSeconds(_writingSpeed / 100f);
        }
        if (i >= completeText.Length)
        {
            StartCoroutine(nextCoroutine);
            StopCoroutine("AnimateText");
        }
    }
}
