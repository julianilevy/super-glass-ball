using UnityEngine;
using System.Collections;

public class LockedObjects : MonoBehaviour
{
    public GameDataManager gameDataManager;
    public GameObject Level01NextButton;
    public GameObject Level01NextButtonLocked;
    public GameObject Level02NextButton;
    public GameObject Level02NextButtonLocked;
    public GameObject Level03NextButton;
    public GameObject Level03NextButtonLocked;
    public GameObject Level01Collectable;
    public GameObject Level01CollectableLocked;
    public GameObject Level02Collectable;
    public GameObject Level02CollectableLocked;
    public GameObject Level03Collectable;
    public GameObject Level03CollectableLocked;
    public GameObject Level01Clock;
    public GameObject Level01ClockLocked;
    public GameObject Level02Clock;
    public GameObject Level02ClockLocked;
    public GameObject Level03Clock;
    public GameObject Level03ClockLocked;
    public GameObject Level01Lives;
    public GameObject Level01LivesLocked;
    public GameObject Level02Lives;
    public GameObject Level02LivesLocked;
    public GameObject Level03Lives;
    public GameObject Level03LivesLocked;
    public GameObject LevelBossThropyBronze;
    public GameObject LevelBossThropyGold;
    public GameObject LevelBossThropyLocked;
    public GameObject LevelBossClockBronze;
    public GameObject LevelBossClockGold;
    public GameObject LevelBossClockLocked;
    public GameObject LevelBossQuizButton;
    public GameObject LevelBossQuizButtonsLocked;
    public GameObject LevelMenuCheatButton;
    public GameObject LevelMenuCheatButtonLocked;

    public void Start()
    {
        ManageNextButton();
        ManageThropies();
        ManageCollectable();
        ManageClock();
        ManageLives();
    }

    public void Update()
    {
        ManageQuizButton();
        ManageCheatButton();
    }

    public void ManageNextButton()
    {
        if (gameDataManager.completedLevel01)
        {
            Level01NextButton.SetActive(true);
            Level01NextButtonLocked.SetActive(false);
        }
        else
        {
            Level01NextButton.SetActive(false);
            Level01NextButtonLocked.SetActive(true);
        }
        if (gameDataManager.completedLevel02)
        {
            Level02NextButton.SetActive(true);
            Level02NextButtonLocked.SetActive(false);
        }
        else
        {
            Level02NextButton.SetActive(false);
            Level02NextButtonLocked.SetActive(true);
        }
        if (gameDataManager.completedLevel03)
        {
            Level03NextButton.SetActive(true);
            Level03NextButtonLocked.SetActive(false);
        }
        else
        {
            Level03NextButton.SetActive(false);
            Level03NextButtonLocked.SetActive(true);
        }
    }

    public void ManageThropies()
    {
        if (!gameDataManager.completedLevelBossBronze && !gameDataManager.completedLevelBossGold)
        {
            LevelBossThropyGold.SetActive(false);
            LevelBossThropyBronze.SetActive(false);
            LevelBossThropyLocked.SetActive(true);
        }
        if (gameDataManager.completedLevelBossBronze && !gameDataManager.completedLevelBossGold)
        {
            LevelBossThropyGold.SetActive(false);
            LevelBossThropyBronze.SetActive(true);
            LevelBossThropyLocked.SetActive(false);
        }
        if (gameDataManager.completedLevelBossGold)
        {
            LevelBossThropyGold.SetActive(true);
            LevelBossThropyBronze.SetActive(false);
            LevelBossThropyLocked.SetActive(false);
        }
    }

    public void ManageQuizButton()
    {
        if (gameDataManager.quizDisabled)
        {
            LevelBossQuizButton.SetActive(false);
            LevelBossQuizButtonsLocked.SetActive(true);
        }
        else
        {
            LevelBossQuizButton.SetActive(true);
            LevelBossQuizButtonsLocked.SetActive(false);
        }
    }

    public void ManageCheatButton()
    {
        if (gameDataManager.cheatedLevels)
        {
            LevelMenuCheatButton.SetActive(false);
            LevelMenuCheatButtonLocked.SetActive(true);
        }
        else
        {
            LevelMenuCheatButton.SetActive(true);
            LevelMenuCheatButtonLocked.SetActive(false);
        }
    }

    public void ManageCollectable()
    {
        if (gameDataManager.collectablesGrabbedLevel01)
        {
            Level01Collectable.SetActive(true);
            Level01CollectableLocked.SetActive(false);
        }
        else
        {
            Level01Collectable.SetActive(false);
            Level01CollectableLocked.SetActive(true);
        }
        if (gameDataManager.collectablesGrabbedLevel02)
        {
            Level02Collectable.SetActive(true);
            Level02CollectableLocked.SetActive(false);
        }
        else
        {
            Level02Collectable.SetActive(false);
            Level02CollectableLocked.SetActive(true);
        }
        if (gameDataManager.collectablesGrabbedLevel03)
        {
            Level03Collectable.SetActive(true);
            Level03CollectableLocked.SetActive(false);
        }
        else
        {
            Level03Collectable.SetActive(false);
            Level03CollectableLocked.SetActive(true);
        }
    }

    public void ManageClock()
    {
        if (gameDataManager.recordTimeLevel01)
        {
            Level01Clock.SetActive(true);
            Level01ClockLocked.SetActive(false);
        }
        else
        {
            Level01Clock.SetActive(false);
            Level01ClockLocked.SetActive(true);
        }
        if (gameDataManager.recordTimeLevel02)
        {
            Level02Clock.SetActive(true);
            Level02ClockLocked.SetActive(false);
        }
        else
        {
            Level02Clock.SetActive(false);
            Level02ClockLocked.SetActive(true);
        }
        if (gameDataManager.recordTimeLevel03)
        {
            Level03Clock.SetActive(true);
            Level03ClockLocked.SetActive(false);
        }
        else
        {
            Level03Clock.SetActive(false);
            Level03ClockLocked.SetActive(true);
        }

        if (!gameDataManager.recordTimeLevelBossBronze && !gameDataManager.recordTimeLevelBossGold)
        {
            LevelBossClockGold.SetActive(false);
            LevelBossClockBronze.SetActive(false);
            LevelBossClockLocked.SetActive(true);
        }
        if (gameDataManager.recordTimeLevelBossBronze && !gameDataManager.recordTimeLevelBossGold)
        {
            LevelBossClockGold.SetActive(false);
            LevelBossClockBronze.SetActive(true);
            LevelBossClockLocked.SetActive(false);
        }
        if (gameDataManager.recordTimeLevelBossGold)
        {
            LevelBossClockGold.SetActive(true);
            LevelBossClockBronze.SetActive(false);
            LevelBossClockLocked.SetActive(false);
        }
    }

    public void ManageLives()
    {
        if (gameDataManager.allLivesRemainLevel01)
        {
            Level01Lives.SetActive(true);
            Level01LivesLocked.SetActive(false);
        }
        else
        {
            Level01Lives.SetActive(false);
            Level01LivesLocked.SetActive(true);
        }
        if (gameDataManager.allLivesRemainLevel02)
        {
            Level02Lives.SetActive(true);
            Level02LivesLocked.SetActive(false);
        }
        else
        {
            Level02Lives.SetActive(false);
            Level02LivesLocked.SetActive(true);
        }
        if (gameDataManager.allLivesRemainLevel03)
        {
            Level03Lives.SetActive(true);
            Level03LivesLocked.SetActive(false);
        }
        else
        {
            Level03Lives.SetActive(false);
            Level03LivesLocked.SetActive(true);
        }
    }

    public void ReloadObjects()
    {
        ManageNextButton();
        ManageThropies();
        ManageCollectable();
        ManageClock();
        ManageLives();
    }
}