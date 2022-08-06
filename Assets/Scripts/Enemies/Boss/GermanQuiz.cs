using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GermanQuiz : MonoBehaviour
{
    public German german;
    public GermanMoveTexts presentationBox;
    public GermanMoveTexts questionsBox;
    public GermanMoveTexts questionTimerBox;
    public GermanMoveTexts answerABox;
    public GermanMoveTexts answerBBox;
    public GermanMoveTexts answerCBox;
    public GermanMoveTexts answerDBox;
    public Text presentationTextField;
    public Text questionsTextField;
    public Text questionTimerTextField;
    public Text answerATextField;
    public Text answerBTextField;
    public Text answerCTextField;
    public Text answerDTextField;
    public GameObject blackCurtain;

    private Color _answersOriginalColor;
    private Color _timerOriginalColor;
    private string _presentationText;
    private string _question01Text;
    private string _question02Text;
    private string _question03Text;
    private string _question04Text;
    private string _question05Text;
    private string _question06Text;
    private string _removeQuizText01;
    private string _removeQuizText02;
    private float _writingSpeed = 3.5f;
    private float _timerMaxTime = 10f;
    private float _timerTime;
    private bool _question01Active;
    private bool _question02Active;
    private bool _question03Active;
    private bool _question04Active;
    private bool _question05Active;
    private bool _question06Active;
    private bool _timerActivated;
    private bool _blackCurtainON;
    private int _correctAnswers;

    public void Start()
    {
        _timerOriginalColor = questionTimerTextField.color;
        _answersOriginalColor = questionsBox.GetComponent<Image>().color;

        _presentationText = "Soy Ascaroth, jefe supremo de este videojuego.\n¡Para poder vencerme tendrás que responder correctamente las siguientes preguntas!";
        _question01Text = "¿En qué año apareció el primer Zelda?";
        _question02Text = "¿Cuántos títulos sacó Rare para la Nintendo 64?";
        _question03Text = "¿Quién desarrolló Super Meat Boy?";
        _question04Text = "¿Qué saga se sitúa en un mundo post apocaliptico?";
        _question05Text = "¿Cuál de estos juegos no fue desarrollado por Blizzard?";
        _question06Text = "¿Cuál es el mejor juego de la historia?";
        _removeQuizText01 = "¡Acertaste a un total de " + _correctAnswers + " preguntas! Me gustaría felicitarte o insultarte por eso, pero no fui programado para dar diferentes respuestas acordes al resultado obtenido.";
        _removeQuizText02 = "De hecho, este quiz es totalmente irrelevante al juego... Como sea, te espero al final del nivel, si es que logras llegar a él MUAJAJAJA *risa muy malvada*.";
    }

    public void Update()
    {
        QuestionTimer();
        MoveBlackCurtain();
    }

    public void QuestionTimer()
    {
        if (_timerActivated)
        {
            if (_timerTime >= 0.99f)
            {
                if (_timerTime <= 4) questionTimerTextField.color = Color.red;

                _timerTime -= Time.deltaTime;
                questionTimerTextField.text = "" + (int)_timerTime;
            }

            if (_timerTime <= 0.99f)
            {
                _timerActivated = false;
                ShowAnswersResults();

                if (_question01Active)
                {
                    _question01Active = false;
                    StartCoroutine(RemoveAnswers("AskQuestion02"));
                }
                if (_question02Active)
                {
                    _question02Active = false;
                    StartCoroutine(RemoveAnswers("AskQuestion03"));
                }
                if (_question03Active)
                {
                    _question03Active = false;
                    StartCoroutine(RemoveAnswers("AskQuestion04"));
                }
                if (_question04Active)
                {
                    _question04Active = false;
                    StartCoroutine(RemoveAnswers("AskQuestion05"));
                }
                if (_question05Active)
                {
                    _question05Active = false;
                    StartCoroutine(RemoveAnswers("AskQuestion06"));
                }
                if (_question06Active)
                {
                    _question06Active = false;
                    StartCoroutine(RemoveAnswers("StopQuestions01"));
                }
            }
        }
    }

    public void AnswerTheQuestion(string answerBox)
    {
        if (_question01Active)
        {
            if (answerBox == "A") ShowAnswersResults();
            if (answerBox == "B") ShowAnswersResults();
            if (answerBox == "C") ShowAnswersResults();
            if (answerBox == "D")
            {
                answerDBox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }

            _question01Active = false;
            _timerActivated = false;
            StartCoroutine(RemoveAnswers("AskQuestion02"));
        }

        if (_question02Active)
        {
            if (answerBox == "A")
            {
                answerABox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }
            if (answerBox == "B") ShowAnswersResults();
            if (answerBox == "C") ShowAnswersResults();
            if (answerBox == "D") ShowAnswersResults();

            _question02Active = false;
            _timerActivated = false;
            StartCoroutine(RemoveAnswers("AskQuestion03"));
        }

        if (_question03Active)
        {
            if (answerBox == "A") ShowAnswersResults();
            if (answerBox == "B") ShowAnswersResults();
            if (answerBox == "C")
            {
                answerCBox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }
            if (answerBox == "D") ShowAnswersResults();

            _question03Active = false;
            _timerActivated = false;
            StartCoroutine(RemoveAnswers("AskQuestion04"));
        }

        if (_question04Active)
        {
            if (answerBox == "A") ShowAnswersResults();
            if (answerBox == "B")
            {
                answerBBox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }
            if (answerBox == "C") ShowAnswersResults();
            if (answerBox == "D") ShowAnswersResults();

            _question04Active = false;
            _timerActivated = false;
            StartCoroutine(RemoveAnswers("AskQuestion05"));
        }

        if (_question05Active)
        {
            if (answerBox == "A") ShowAnswersResults();
            if (answerBox == "B") ShowAnswersResults();
            if (answerBox == "C")
            {
                answerCBox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }
            if (answerBox == "D") ShowAnswersResults();

            _question05Active = false;
            _timerActivated = false;
            StartCoroutine(RemoveAnswers("AskQuestion06"));
        }

        if (_question06Active)
        {
            if (answerBox == "A")
            {
                answerABox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }
            if (answerBox == "B")
            {
                answerBBox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }
            if (answerBox == "C")
            {
                answerCBox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }
            if (answerBox == "D")
            {
                answerDBox.GetComponent<Image>().color = Color.green;
                _correctAnswers++;
            }

            _question06Active = false;
            _timerActivated = false;
            StartCoroutine(RemoveAnswers("StopQuestions01"));
        }
    }

    public void ShowAnswersResults()
    {
        if (_question01Active)
        {
            answerABox.GetComponent<Image>().color = Color.red;
            answerBBox.GetComponent<Image>().color = Color.red;
            answerCBox.GetComponent<Image>().color = Color.red;
            answerDBox.GetComponent<Image>().color = Color.green;

        }
        if (_question02Active)
        {
            answerABox.GetComponent<Image>().color = Color.green;
            answerBBox.GetComponent<Image>().color = Color.red;
            answerCBox.GetComponent<Image>().color = Color.red;
            answerDBox.GetComponent<Image>().color = Color.red;
        }
        if (_question03Active)
        {
            answerABox.GetComponent<Image>().color = Color.red;
            answerBBox.GetComponent<Image>().color = Color.red;
            answerCBox.GetComponent<Image>().color = Color.green;
            answerDBox.GetComponent<Image>().color = Color.red;
        }
        if (_question04Active)
        {
            answerABox.GetComponent<Image>().color = Color.red;
            answerBBox.GetComponent<Image>().color = Color.green;
            answerCBox.GetComponent<Image>().color = Color.red;
            answerDBox.GetComponent<Image>().color = Color.red;
        }
        if (_question05Active)
        {
            answerABox.GetComponent<Image>().color = Color.red;
            answerBBox.GetComponent<Image>().color = Color.red;
            answerCBox.GetComponent<Image>().color = Color.green;
            answerDBox.GetComponent<Image>().color = Color.red;
        }
        if (_question06Active)
        {
            answerABox.GetComponent<Image>().color = Color.green;
            answerBBox.GetComponent<Image>().color = Color.green;
            answerCBox.GetComponent<Image>().color = Color.green;
            answerDBox.GetComponent<Image>().color = Color.green;
        }
    }

    public void MoveBlackCurtain()
    {
        if (_blackCurtainON) blackCurtain.transform.position += Vector3.up;
    }

    public IEnumerator ActivatePresentationText()
    {
        german.Arrived = true;
        yield return new WaitForSeconds(2f);
        presentationBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_presentationText, presentationTextField, "AskQuestion01"));
        StopCoroutine(ActivatePresentationText());
    }

    public IEnumerator AskQuestion01()
    {
        yield return new WaitForSeconds(1.5f);
        presentationBox.RemoveText();
        yield return new WaitForSeconds(1.5f);
        questionsBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_question01Text, questionsTextField, "ShowAnswers01"));
        StopCoroutine(AskQuestion01());
    }

    public IEnumerator ShowAnswers01()
    {
        yield return new WaitForSeconds(1f);
        answerABox.active = true;
        answerATextField.text = "1983";
        yield return new WaitForSeconds(0.4f);
        answerBBox.active = true;
        answerBTextField.text = "1980";
        yield return new WaitForSeconds(0.4f);
        answerCBox.active = true;
        answerCTextField.text = "1989";
        yield return new WaitForSeconds(0.4f);
        answerDBox.active = true;
        answerDTextField.text = "1986";
        yield return new WaitForSeconds(0.4f);
        questionTimerBox.active = true;
        _timerTime = _timerMaxTime;
        questionTimerTextField.text = "" + (int)_timerTime;
        yield return new WaitForSeconds(1.5f);
        _timerActivated = true;
        _question01Active = true;
        StopCoroutine(ShowAnswers01());
    }

    public IEnumerator AskQuestion02()
    {
        questionsBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_question02Text, questionsTextField, "ShowAnswers02"));
        StopCoroutine(AskQuestion02());
    }

    public IEnumerator ShowAnswers02()
    {
        yield return new WaitForSeconds(1f);
        answerABox.active = true;
        answerATextField.text = "11";
        yield return new WaitForSeconds(0.4f);
        answerBBox.active = true;
        answerBTextField.text = "5";
        yield return new WaitForSeconds(0.4f);
        answerCBox.active = true;
        answerCTextField.text = "8";
        yield return new WaitForSeconds(0.4f);
        answerDBox.active = true;
        answerDTextField.text = "3";
        yield return new WaitForSeconds(0.4f);
        questionTimerBox.active = true;
        _timerTime = _timerMaxTime;
        questionTimerTextField.text = "" + (int)_timerTime;
        yield return new WaitForSeconds(1.5f);
        _timerActivated = true;
        _question02Active = true;
        StopCoroutine(ShowAnswers02());
    }

    public IEnumerator AskQuestion03()
    {
        questionsBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_question03Text, questionsTextField, "ShowAnswers03"));
        StopCoroutine(AskQuestion03());
    }

    public IEnumerator ShowAnswers03()
    {
        yield return new WaitForSeconds(1f);
        answerABox.active = true;
        answerATextField.text = "Phil Fish";
        yield return new WaitForSeconds(0.4f);
        answerBBox.active = true;
        answerBTextField.text = "Shigeru Miyamoto";
        yield return new WaitForSeconds(0.4f);
        answerCBox.active = true;
        answerCTextField.text = "Edmund McMillen";
        yield return new WaitForSeconds(0.4f);
        answerDBox.active = true;
        answerDTextField.text = "Markus Persson";
        yield return new WaitForSeconds(0.4f);
        questionTimerBox.active = true;
        _timerTime = _timerMaxTime;
        questionTimerTextField.text = "" + (int)_timerTime;
        yield return new WaitForSeconds(1.5f);
        _timerActivated = true;
        _question03Active = true;
        StopCoroutine(ShowAnswers03());
    }

    public IEnumerator AskQuestion04()
    {
        questionsBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_question04Text, questionsTextField, "ShowAnswers04"));
        StopCoroutine(AskQuestion04());
    }

    public IEnumerator ShowAnswers04()
    {
        yield return new WaitForSeconds(1f);
        answerABox.active = true;
        answerATextField.text = "The Witcher";
        yield return new WaitForSeconds(0.4f);
        answerBBox.active = true;
        answerBTextField.text = "Fallout";
        yield return new WaitForSeconds(0.4f);
        answerCBox.active = true;
        answerCTextField.text = "Sleeping Dogs";
        yield return new WaitForSeconds(0.4f);
        answerDBox.active = true;
        answerDTextField.text = "The Elder Scrolls";
        yield return new WaitForSeconds(0.4f);
        questionTimerBox.active = true;
        _timerTime = _timerMaxTime;
        questionTimerTextField.text = "" + (int)_timerTime;
        yield return new WaitForSeconds(1.5f);
        _timerActivated = true;
        _question04Active = true;
        StopCoroutine(ShowAnswers04());
    }

    public IEnumerator AskQuestion05()
    {
        questionsBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_question05Text, questionsTextField, "ShowAnswers05"));
        StopCoroutine(AskQuestion05());
    }

    public IEnumerator ShowAnswers05()
    {
        yield return new WaitForSeconds(1f);
        answerABox.active = true;
        answerATextField.text = "Warcraft";
        yield return new WaitForSeconds(0.4f);
        answerBBox.active = true;
        answerBTextField.text = "Diablo";
        yield return new WaitForSeconds(0.4f);
        answerCBox.active = true;
        answerCTextField.text = "Terraria";
        yield return new WaitForSeconds(0.4f);
        answerDBox.active = true;
        answerDTextField.text = "Starcraft";
        yield return new WaitForSeconds(0.4f);
        questionTimerBox.active = true;
        _timerTime = _timerMaxTime;
        questionTimerTextField.text = "" + (int)_timerTime;
        yield return new WaitForSeconds(1.5f);
        _timerActivated = true;
        _question05Active = true;
        StopCoroutine(ShowAnswers05());
    }

    public IEnumerator AskQuestion06()
    {
        questionsBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_question06Text, questionsTextField, "ShowAnswers06"));
        StopCoroutine(AskQuestion06());
    }

    public IEnumerator ShowAnswers06()
    {
        yield return new WaitForSeconds(1f);
        answerABox.active = true;
        answerATextField.text = "Dark Souls";
        yield return new WaitForSeconds(0.4f);
        answerBBox.active = true;
        answerBTextField.text = "Dark Souls";
        yield return new WaitForSeconds(0.4f);
        answerCBox.active = true;
        answerCTextField.text = "Dark Souls";
        yield return new WaitForSeconds(0.4f);
        answerDBox.active = true;
        answerDTextField.text = "Dark Souls";
        yield return new WaitForSeconds(0.4f);
        questionTimerBox.active = true;
        _timerTime = _timerMaxTime;
        questionTimerTextField.text = "" + (int)_timerTime;
        yield return new WaitForSeconds(1.5f);
        _timerActivated = true;
        _question06Active = true;
        StopCoroutine(ShowAnswers06());
    }

    public IEnumerator StopQuestions01()
    {
        yield return new WaitForSeconds(1f);
        _removeQuizText01 = "¡Acertaste a un total de " + _correctAnswers + " preguntas! Me gustaría felicitarte o insultarte por eso, pero no fui programado para dar diferentes respuestas acordes al resultado obtenido.";
        presentationTextField.text = "";
        presentationBox.active = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(AnimateText(_removeQuizText01, presentationTextField, "StopQuestions02"));
        StopCoroutine(StopQuestions01());
    }

    public IEnumerator StopQuestions02()
    {
        yield return new WaitForSeconds(1.5f);
        presentationTextField.text = "";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AnimateText(_removeQuizText02, presentationTextField, "StopQuestions03"));
        StopCoroutine(StopQuestions02());
    }

    public IEnumerator StopQuestions03()
    {
        yield return new WaitForSeconds(1.5f);
        presentationBox.RemoveText();
        yield return new WaitForSeconds(1.5f);
        presentationTextField.text = "";
        german.QuizDone = true;
        german.Arrived = false;
        _blackCurtainON = true;
        yield return new WaitForSeconds(2f);
        german.playerStats.locked = false;
        yield return new WaitForSeconds(6f);
        Destroy(blackCurtain);
        _blackCurtainON = false;
        german.Arrived = true;
        german.gameObject.SetActive(false);
        StopCoroutine(StopQuestions03());
    }

    public IEnumerator RemoveAnswers(string nextCoroutine)
    {
        yield return new WaitForSeconds(2f);
        questionTimerBox.RemoveText();
        yield return new WaitForSeconds(0.4f);
        answerABox.RemoveText();
        yield return new WaitForSeconds(0.4f);
        answerBBox.RemoveText();
        yield return new WaitForSeconds(0.4f);
        answerCBox.RemoveText();
        yield return new WaitForSeconds(0.4f);
        answerDBox.RemoveText();
        yield return new WaitForSeconds(0.4f);
        questionsBox.RemoveText();
        yield return new WaitForSeconds(1.5f);
        questionsTextField.text = "";
        _timerTime = _timerMaxTime;
        questionTimerTextField.color = _timerOriginalColor;
        questionTimerTextField.text = "" + (int)_timerTime;
        answerABox.GetComponent<Image>().color = _answersOriginalColor;
        answerBBox.GetComponent<Image>().color = _answersOriginalColor;
        answerCBox.GetComponent<Image>().color = _answersOriginalColor;
        answerDBox.GetComponent<Image>().color = _answersOriginalColor;
        StartCoroutine(nextCoroutine);
        StopCoroutine("RemoveAnswers");
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