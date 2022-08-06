using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public float menuLevelPositionX;
    public float menuLevelPositionY;
    public float menuLevelPositionZ;
    public bool recordTimeLevel01;
    public bool recordTimeLevel02;
    public bool recordTimeLevel03;
    public bool recordTimeLevelBossBronze;
    public bool recordTimeLevelBossGold;
    public bool collectablesGrabbedLevel01;
    public bool collectablesGrabbedLevel02;
    public bool collectablesGrabbedLevel03;
    public bool allLivesRemainLevel01;
    public bool allLivesRemainLevel02;
    public bool allLivesRemainLevel03;
    public bool completedLevel01;
    public bool completedLevel02;
    public bool completedLevel03;
    public bool completedLevelBossBronze;
    public bool completedLevelBossGold;
    public bool quizDisabled;
    public bool cheatedLevels;

    private List<bool> _goldRequeriments = new List<bool>();
    private float _timeLevel01 = 0;
    private float _timeLevel02 = 0;
    private float _timeLevel03 = 0;
    private float _timeLevelBoss = 0;
    private bool _tempRecordTimeLevel01 = false;
    private bool _tempRecordTimeLevel02 = false;
    private bool _tempRecordTimeLevel03 = false;
    private bool _tempRecordTimeLevelBossBronze = false;
    private bool _tempRecordTimeLevelBossGold = false;
    private bool _tempAllLivesRemainLevel01 = false;
    private bool _tempAllLivesRemainLevel02 = false;
    private bool _tempAllLivesRemainLevel03 = false;
    private bool _levelBossGoldActive = false;

    public bool LevelBossGoldActive
    {
        get { return _levelBossGoldActive; }
    }

    public void Awake ()
    {
        Load();
        GoldRequeriments();
    }

    public void Update()
    {
        if (playerStats != null)
        {
            CheckCompletedLevels();
            CheckTime();
            CheckCollectables();
            CheckLives();
        }
    }

    public void CheckCompletedLevels()
    {
        if (!completedLevel01) if (playerStats.levelCompleted) completedLevel01 = true;
        if (!completedLevel02) if (playerStats.levelCompleted) completedLevel02 = true;
        if (!completedLevel03) if (playerStats.levelCompleted) completedLevel03 = true;

        if (!completedLevelBossBronze && !_levelBossGoldActive) if (playerStats.levelCompleted) completedLevelBossBronze = true;
        if (!completedLevelBossGold && _levelBossGoldActive) if (playerStats.levelCompleted) completedLevelBossGold = true;
    }

    public void CheckTime()
    {
        if (!recordTimeLevel01)
        {
            _timeLevel01 += Time.deltaTime;

            if (_timeLevel01 <= 140) _tempRecordTimeLevel01 = true;
            else _tempRecordTimeLevel01 = false;
        }
        if (!recordTimeLevel02)
        {
            _timeLevel02 += Time.deltaTime;

            if (_timeLevel02 <= 150) _tempRecordTimeLevel02 = true;
            else _tempRecordTimeLevel02 = false;
        }
        if (!recordTimeLevel03)
        {
            _timeLevel03 += Time.deltaTime;

            if (_timeLevel03 <= 180) _tempRecordTimeLevel03 = true;
            else _tempRecordTimeLevel03 = false;
        }
        if (!recordTimeLevelBossBronze)
        {
            if (!_levelBossGoldActive)
            {
                _timeLevelBoss += Time.deltaTime;

                if (quizDisabled)
                {
                    if (_timeLevelBoss <= 390) _tempRecordTimeLevelBossBronze = true;
                    else _tempRecordTimeLevelBossBronze = false;
                }
                else
                {
                    if (_timeLevelBoss <= 530) _tempRecordTimeLevelBossBronze = true;
                    else _tempRecordTimeLevelBossBronze = false;
                }

            }
        }
        if (!recordTimeLevelBossGold)
        {
            if (_levelBossGoldActive)
            {
                _timeLevelBoss += Time.deltaTime;

                if (quizDisabled)
                {
                    if (_timeLevelBoss <= 390) _tempRecordTimeLevelBossGold = true;
                    else _tempRecordTimeLevelBossGold = false;
                }
                else
                {
                    if (_timeLevelBoss <= 530) _tempRecordTimeLevelBossGold = true;
                    else _tempRecordTimeLevelBossGold = false;
                }
            }
        }
    }

    public void CheckCollectables()
    {
        if (!collectablesGrabbedLevel01) if (playerStats.collectables >= 20) collectablesGrabbedLevel01 = true;
        if (!collectablesGrabbedLevel02) if (playerStats.collectables >= 20) collectablesGrabbedLevel02 = true;
        if (!collectablesGrabbedLevel03) if (playerStats.collectables >= 20) collectablesGrabbedLevel03 = true;
    }

    public void CheckLives()
    {
        if (!allLivesRemainLevel01)
        {
            if (playerStats.lives >= 3) _tempAllLivesRemainLevel01 = true;
            else _tempAllLivesRemainLevel01 = false;
        }
        if (!allLivesRemainLevel02)
        {
            if (playerStats.lives >= 3) _tempAllLivesRemainLevel02 = true;
            else _tempAllLivesRemainLevel02 = false;
        }
        if (!allLivesRemainLevel03)
        {
            if (playerStats.lives >= 3) _tempAllLivesRemainLevel03 = true;
            else _tempAllLivesRemainLevel03 = false;
        }
    }

    public void GoldRequeriments()
    {
        if (Application.loadedLevelName == "Level Boss")
        {
            _goldRequeriments.Add(recordTimeLevel01);
            _goldRequeriments.Add(recordTimeLevel02);
            _goldRequeriments.Add(recordTimeLevel03);
            _goldRequeriments.Add(collectablesGrabbedLevel01);
            _goldRequeriments.Add(collectablesGrabbedLevel02);
            _goldRequeriments.Add(collectablesGrabbedLevel03);
            _goldRequeriments.Add(allLivesRemainLevel01);
            _goldRequeriments.Add(allLivesRemainLevel02);
            _goldRequeriments.Add(allLivesRemainLevel03);
            _goldRequeriments.Add(completedLevel01);
            _goldRequeriments.Add(completedLevel02);
            _goldRequeriments.Add(completedLevel03);

            foreach (var requeriment in _goldRequeriments)
            {
                if (!requeriment) return;
            }

            _levelBossGoldActive = true;
        }
    }

    public void SetTimeRecord(int level)
    {
        if (level == 1) if (!recordTimeLevel01) recordTimeLevel01 = _tempRecordTimeLevel01;
        if (level == 2) if (!recordTimeLevel02) recordTimeLevel02 = _tempRecordTimeLevel02;
        if (level == 3) if (!recordTimeLevel03) recordTimeLevel03 = _tempRecordTimeLevel03;
        if (level == 4)
        {
            if (!_levelBossGoldActive) if (!recordTimeLevelBossBronze) recordTimeLevelBossBronze = _tempRecordTimeLevelBossBronze;
            if (_levelBossGoldActive) if (!recordTimeLevelBossGold) recordTimeLevelBossGold = _tempRecordTimeLevelBossGold;
        }
    }

    public void SetLivesRemain(int level)
    {
        if (level == 1) if (!allLivesRemainLevel01) allLivesRemainLevel01 = _tempAllLivesRemainLevel01;
        if (level == 2) if (!allLivesRemainLevel02) allLivesRemainLevel02 = _tempAllLivesRemainLevel02;
        if (level == 3) if (!allLivesRemainLevel03) allLivesRemainLevel03 = _tempAllLivesRemainLevel03;
    }

    public void SetQuizButton(bool quiz)
    {
        quizDisabled = quiz;

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (Application.loadedLevelName == "Menu")
        {
            FileStream fileMenu = File.Create(Application.persistentDataPath + "/Menu.sav");
            Data dataMenu = new Data();
            if (cheatedLevels)
            {
                dataMenu.menuLevelPositionX = 254.9524f;
                dataMenu.menuLevelPositionY = 54.75435f;
                dataMenu.menuLevelPositionZ = 130.3648f;
            }
            else
            {
                dataMenu.menuLevelPositionX = menuLevelPositionX;
                dataMenu.menuLevelPositionY = menuLevelPositionY;
                dataMenu.menuLevelPositionZ = menuLevelPositionZ;
            }
            dataMenu.quizDisabled = quizDisabled;
            binaryFormatter.Serialize(fileMenu, dataMenu);
            fileMenu.Close();
        }
    }

    public void UnlockAll()
    {
        recordTimeLevel01 = true;
        recordTimeLevel02 = true;
        recordTimeLevel03 = true;
        recordTimeLevelBossBronze = true;
        recordTimeLevelBossGold = true;
        collectablesGrabbedLevel01 = true;
        collectablesGrabbedLevel02 = true;
        collectablesGrabbedLevel03 = true;
        allLivesRemainLevel01 = true;
        allLivesRemainLevel02 = true;
        allLivesRemainLevel03 = true;
        completedLevel01 = true;
        completedLevel02 = true;
        completedLevel03 = true;
        completedLevelBossBronze = true;
        completedLevelBossGold = true;
        quizDisabled = false;
        cheatedLevels = true;
    }

    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (Application.loadedLevelName == "Menu")
        {
            FileStream fileMenu = File.Create(Application.persistentDataPath + "/Menu.sav");
            Data dataMenu = new Data();
            dataMenu.menuLevelPositionX = menuLevelPositionX;
            dataMenu.menuLevelPositionY = menuLevelPositionY;
            dataMenu.menuLevelPositionZ = menuLevelPositionZ;
            dataMenu.quizDisabled = quizDisabled;
            binaryFormatter.Serialize(fileMenu, dataMenu);
            fileMenu.Close();
        }
        if (Application.loadedLevelName == "Level 01")
        {
            FileStream fileLevel01 = File.Create(Application.persistentDataPath + "/Level01.sav");
            Data dataLevel01 = new Data();
            SetTimeRecord(1);
            SetLivesRemain(1);
            dataLevel01.completedLevel01 = completedLevel01;
            dataLevel01.recordTimeLevel01 = recordTimeLevel01;
            dataLevel01.collectablesGrabbedLevel01 = collectablesGrabbedLevel01;
            dataLevel01.allLivesRemainLevel01 = allLivesRemainLevel01;
            binaryFormatter.Serialize(fileLevel01, dataLevel01);
            fileLevel01.Close();
        }
        if (Application.loadedLevelName == "Level 02")
        {
            FileStream fileLevel02 = File.Create(Application.persistentDataPath + "/Level02.sav");
            Data dataLevel02 = new Data();
            SetTimeRecord(2);
            SetLivesRemain(2);
            dataLevel02.completedLevel02 = completedLevel02;
            dataLevel02.recordTimeLevel02 = recordTimeLevel02;
            dataLevel02.collectablesGrabbedLevel02 = collectablesGrabbedLevel02;
            dataLevel02.allLivesRemainLevel02 = allLivesRemainLevel02;
            binaryFormatter.Serialize(fileLevel02, dataLevel02);
            fileLevel02.Close();
        }
        if (Application.loadedLevelName == "Level 03")
        {
            FileStream fileLevel03 = File.Create(Application.persistentDataPath + "/Level03.sav");
            Data dataLevel03 = new Data();
            SetTimeRecord(3);
            SetLivesRemain(3);
            dataLevel03.completedLevel03 = completedLevel03;
            dataLevel03.recordTimeLevel03 = recordTimeLevel03;
            dataLevel03.collectablesGrabbedLevel03 = collectablesGrabbedLevel03;
            dataLevel03.allLivesRemainLevel03 = allLivesRemainLevel03;
            binaryFormatter.Serialize(fileLevel03, dataLevel03);
            fileLevel03.Close();
        }
        if (Application.loadedLevelName == "Level Boss")
        {
            FileStream fileLevelBoss = File.Create(Application.persistentDataPath + "/LevelBoss.sav");
            Data dataLevelBoss = new Data();
            SetTimeRecord(4);
            dataLevelBoss.recordTimeLevelBossBronze = recordTimeLevelBossBronze;
            dataLevelBoss.recordTimeLevelBossGold = recordTimeLevelBossGold;
            dataLevelBoss.completedLevelBossBronze = completedLevelBossBronze;
            dataLevelBoss.completedLevelBossGold = completedLevelBossGold;
            binaryFormatter.Serialize(fileLevelBoss, dataLevelBoss);
            fileLevelBoss.Close();
        }
    }

    public void Load()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/Menu.sav"))
        {
            FileStream fileMenu = File.Open(Application.persistentDataPath + "/Menu.sav", FileMode.Open);
            Data dataMenu = (Data)binaryFormatter.Deserialize(fileMenu);
            fileMenu.Close();
            menuLevelPositionX = dataMenu.menuLevelPositionX;
            menuLevelPositionY = dataMenu.menuLevelPositionY;
            menuLevelPositionZ = dataMenu.menuLevelPositionZ;
            quizDisabled = dataMenu.quizDisabled;
        }
        if (File.Exists(Application.persistentDataPath + "/Level01.sav"))
        {
            FileStream fileLevel01 = File.Open(Application.persistentDataPath + "/Level01.sav", FileMode.Open);
            Data dataLevel01 = (Data)binaryFormatter.Deserialize(fileLevel01);
            fileLevel01.Close();
            completedLevel01 = dataLevel01.completedLevel01;
            recordTimeLevel01 = dataLevel01.recordTimeLevel01;
            collectablesGrabbedLevel01 = dataLevel01.collectablesGrabbedLevel01;
            allLivesRemainLevel01 = dataLevel01.allLivesRemainLevel01;
        }
        if (File.Exists(Application.persistentDataPath + "/Level02.sav"))
        {
            FileStream fileLevel02 = File.Open(Application.persistentDataPath + "/Level02.sav", FileMode.Open);
            Data dataLevel02 = (Data)binaryFormatter.Deserialize(fileLevel02);
            fileLevel02.Close();
            completedLevel02 = dataLevel02.completedLevel02;
            recordTimeLevel02 = dataLevel02.recordTimeLevel02;
            collectablesGrabbedLevel02 = dataLevel02.collectablesGrabbedLevel02;
            allLivesRemainLevel02 = dataLevel02.allLivesRemainLevel02;
        }
        if (File.Exists(Application.persistentDataPath + "/Level03.sav"))
        {
            FileStream fileLevel03 = File.Open(Application.persistentDataPath + "/Level03.sav", FileMode.Open);
            Data dataLevel03 = (Data)binaryFormatter.Deserialize(fileLevel03);
            fileLevel03.Close();
            completedLevel03 = dataLevel03.completedLevel03;
            recordTimeLevel03 = dataLevel03.recordTimeLevel03;
            collectablesGrabbedLevel03 = dataLevel03.collectablesGrabbedLevel03;
            allLivesRemainLevel03 = dataLevel03.allLivesRemainLevel03;
        }
        if (File.Exists(Application.persistentDataPath + "/LevelBoss.sav"))
        {
            FileStream fileLevelBoss = File.Open(Application.persistentDataPath + "/LevelBoss.sav", FileMode.Open);
            Data dataLevelBoss = (Data)binaryFormatter.Deserialize(fileLevelBoss);
            fileLevelBoss.Close();
            recordTimeLevelBossBronze = dataLevelBoss.recordTimeLevelBossBronze;
            recordTimeLevelBossGold = dataLevelBoss.recordTimeLevelBossGold;
            completedLevelBossBronze = dataLevelBoss.completedLevelBossBronze;
            completedLevelBossGold = dataLevelBoss.completedLevelBossGold;
        }
    }
}