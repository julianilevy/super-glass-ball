using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeScreen : MonoBehaviour
{
    public Scenes nextScene;

    private Image image;
    private bool _firstFade;
    private bool _lastFade;

    public enum Scenes
    {
        Menu,
        Level01,
        Level02,
        Level03,
        LevelBoss,
        Exit
    }

	public void Start ()
    {
        image = gameObject.GetComponent<Image>();
        image.material.color = new Color(0, 0, 0, 1);
        _firstFade = true;
	}
	
	public void Update ()
    {
        FadeIn();
        FadeOut();
	}

    public void FadeIn()
    {
        if (_firstFade)
        {
            image.material.color -= new Color(0, 0, 0, 0.01f);

            if (image.material.color.a <= 0)
            {
                image.material.color = new Color(0, 0, 0, 0);
                _firstFade = false;
            }
        }
    }

    public void FadeOut()
    {
        if (_lastFade)
        {
            image.material.color += new Color(0, 0, 0, 0.01f);

            if (image.material.color.a >= 1)
            {
                image.material.color = new Color(0, 0, 0, 1);
                _lastFade = false;
                StartCoroutine(ChangeScene());
            }
        }
    }

    public void ActivateFade()
    {
        _lastFade = true;
    }

    public void SetNextScene(string scene)
    {
        if (scene == "Menu") nextScene = Scenes.Menu;
        if (scene == "Level 01") nextScene = Scenes.Level01;
        if (scene == "Level 02") nextScene = Scenes.Level02;
        if (scene == "Level 03") nextScene = Scenes.Level03;
        if (scene == "Level Boss") nextScene = Scenes.LevelBoss;
        if (scene == "Exit") nextScene = Scenes.Exit;
        ActivateFade();
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        if (nextScene == Scenes.Menu) Application.LoadLevel("Menu");
        if (nextScene == Scenes.Level01) Application.LoadLevel("Level 01");
        if (nextScene == Scenes.Level02) Application.LoadLevel("Level 02");
        if (nextScene == Scenes.Level03) Application.LoadLevel("Level 03");
        if (nextScene == Scenes.LevelBoss) Application.LoadLevel("Level Boss");
        if (nextScene == Scenes.Exit) Application.Quit();
        StopCoroutine(ChangeScene());
    }
}