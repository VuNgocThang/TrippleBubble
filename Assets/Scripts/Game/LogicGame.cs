using DG.Tweening;
using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicGame : MonoBehaviour
{
    public static LogicGame instance;
    public LogicGameUI logicUI;
    public UIGameManager uiGame;
    public List<Bubble> listBB;
    public List<int> listIndex;
    public Timer timer;
    public List<Bubble> listGOStored = new List<Bubble>();
    public List<Bubble> listBBShuffle;
    public List<Transform> listPoint = new List<Transform>();
    public LineController lineController;
    public List<Bubble> listBubbleUndo = new List<Bubble>();
    public LayerMask layerMask;
    public List<LevelSetMap> listLevel;
    public List<LevelSetMap> listLevelDaily;
    public int indexLevel;
    public LevelSetMap level;
    public PathCreator pathCreater;
    public int count;
    public bool isDrag;
    public bool canShuffle;
    public bool checkLose;
    public bool checkWin;
    public bool canEat;
    int winStreak;
    public TextMeshProUGUI txtCombo;
    public Transform targetHT;
    public bool canClick;
    public Transform pathCreaterGift;
    int currentTotalBB;
    public GameObject particleTest;
    public CameraResize cameraResize;
    [SerializeField] TutorialManager tutorialManager;
    public Transform pathHandRotate;
    public List<int> listIndexParent = new List<int>()
    {
        0,1,2,3,4,5,6,7,8
    };
    public bool isInTut;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {

        if (!DataUseInGame.gameData.isDaily)
        {
            indexLevel = DataUseInGame.gameData.indexLevel;
            level = Instantiate(listLevel[indexLevel], transform);
        }
        else
        {
            indexLevel = DataUseInGame.gameData.indexDailyLV;
            level = Instantiate(listLevelDaily[indexLevel], transform);
        }
        InitSomething();
        InitBubbles();
        lineController.CreateLine(listBBShuffle);
        cameraResize.InitSizeObject(level.gameObject);
        currentTotalBB = listBB.Count;
        canShuffle = true;

        //số bóng *3 + 30 giây
        //timer.timeLeft = currentTotalBB * 3 + 30f;
        timer.timeLeft = 1111f;

        timer.stopTimer = true;
        UseBooster();
        StartCoroutine(timer.InitTimerSetting());
        if (DataUseInGame.gameData.indexLevel == 1 || DataUseInGame.gameData.indexLevel == 2)
        {
            timer.stopTimer = true;
        }

        if (DataUseInGame.gameData.indexLevel == 2)
        {
            int a = UnityEngine.Random.Range(0, listBB.Count);
            listBB[a].originalPos = listBB[a].transform.position;
            Move(listBB[a]);
        }
    }
    private void InitSomething()
    {
        if (DataUseInGame.gameData.isDaily)
        {
            foreach (int item in DataUseInGame.gameData.listIndexDaily)
            {
                if (!listIndex.Contains(item))
                {
                    listIndex.Add(item);
                }
            }
        }
        else
        {
            foreach (int item in DataUseInGame.gameData.listIndex)
            {
                if (!listIndex.Contains(item))
                {
                    listIndex.Add(item);
                }
            }
        }

        uiGame.InitAnim();
        Application.targetFrameRate = 60;
        if (DataUseInGame.gameData.indexLevel == 1 || DataUseInGame.gameData.indexLevel == 2)
        {
            canClick = false;
        }
        else
        {
            canClick = true;
        }
        if (!PlayerPrefs.HasKey("WinStreak"))
        {
            winStreak = 0;
            PlayerPrefs.SetInt("WinStreak", winStreak);
            PlayerPrefs.Save();
        }
        else
        {
            winStreak = PlayerPrefs.GetInt("WinStreak");
        }

        count = 0;

        if (DataUseInGame.gameData.indexLevel > 0 || DataUseInGame.gameData.isDaily)
        {
            Debug.Log(DataUseInGame.gameData.isDaily + " 12323123");
            GameManager.Instance.canRotate = true;
        }
    }
    void InitBubbles()
    {
        int count = listIndex.Count;
        int countAll = level.bubbles.Count;
        int max = level.maxEach;
        int countParent = 0;
        int numCountParent;

        for (int i = 0; i < level.bubbles.Count; i++)
        {
            if (level.bubbles[i].children.childCount > 0)
            {
                level.bubbles[i].hasChildren = true;
                countParent++;
            }
        }
        numCountParent = countParent;

        List<int> listRandom = new List<int>();
        int[] arr = new int[count];
        while (countAll > 0)
        {
            int i;
            int index;

            if (countParent > 0)
            {
                i = UnityEngine.Random.Range(0, 8);
                countParent--;

                index = listIndex[i];

                if (arr[i] < max)
                {
                    arr[i] += 3;
                    for (int y = 0; y < 3; y++)
                    {
                        listRandom.Add(index);
                        countAll--;
                    }
                }
            }
            else
            {
                i = UnityEngine.Random.Range(0, count);
                index = listIndex[i];

                if (arr[i] < max)
                {
                    arr[i] += 3;
                    for (int y = 0; y < 3; y++)
                    {
                        listRandom.Add(index);
                        countAll--;
                    }
                }
            }


        }

        List<int> listRandomParent = new List<int>();
        for (int i = 0; i < numCountParent; i++)
        {
            int j = UnityEngine.Random.Range(0, numCountParent);
            listRandomParent.Add(listRandom[j]);
            listRandom.RemoveAt(j);
        }

        List<int> list = new List<int>();
        int r;
        while (listRandom.Count > 0)
        {
            r = UnityEngine.Random.Range(0, listRandom.Count);
            list.Add(listRandom[r]);
            listRandom.RemoveAt(r);
        }
        listRandom.AddRange(list);

        List<Bubble> listBBInit = new List<Bubble>();

        for (int i = 0; i < level.bubbles.Count; i++)
        {
            listBBInit.Add(level.bubbles[i]);
        }
        int countBB = level.bubbles.Count;
        List<Bubble> tempB = new List<Bubble>();
        List<Bubble> bubbleParent = new List<Bubble>();

        for (int i = 0; i < countBB; i++)
        {
            tempB.Add(listBBInit[i]);
        }

        for (int i = 0; i < countBB; i++)
        {
            if (tempB[i].hasChildren)
            {
                //Debug.Log(i + " --- " + tempB[i].name);
                bubbleParent.Add(tempB[i]);
                listBBInit.Remove(tempB[i]);
            }
        }

        //Tutorial
        if (DataUseInGame.gameData.indexLevel == 0 && !DataUseInGame.gameData.isDaily)
        {
            for (int i = 0; i < tutorialManager.listIndex.Count; i++)
            {
                listBBInit[i].Init(tutorialManager.listIndex[i]);
                listBBInit[i].originalScale = listBBInit[i].transform.localScale;

                listBB.Add(listBBInit[i]);
            }
            tutorialManager.ShowTutorial();
            tutorialManager.handClick.gameObject.SetActive(true);
            tutorialManager.AnimHand();
            tutorialManager.AnimHandRotate();
        }
        else
        {

            for (int i = 0; i < listRandomParent.Count; i++)
            {
                bubbleParent[i].CheckHasChild();
                bubbleParent[i].Init(listRandomParent[i]);
                bubbleParent[i].originalScale = bubbleParent[i].transform.localScale;

                listBB.Add(bubbleParent[i]);
            }


            for (int i = 0; i < listRandom.Count; i++)
            {
                listBBInit[i].Init(listRandom[i]);
                listBBInit[i].originalScale = listBBInit[i].transform.localScale;

                listBB.Add(listBBInit[i]);
            }
        }


        foreach (Bubble bubble in listBB)
        {
            if (!bubble.isChild)
            {
                listBBShuffle.Add(bubble);
            }
        }


    }
    void UseBooster()
    {
        if (PlayerPrefs.GetInt("BoosterHint") == 1)
        {
            Debug.Log("USE HINT");
            UseBoosterHint();
        }

        if (PlayerPrefs.GetInt("BoosterTimer") == 1)
        {
            Debug.Log("USE TIMER");
            UseBoosterTimer();
        }

        if (PlayerPrefs.GetInt("BoosterLightning") == 1)
        {
            Debug.Log("USE LIGHTNING");
            UseBoosterLightning();
        }
    }
    int indexHint = -1;
    void UseBoosterHint()
    {
        indexHint = UnityEngine.Random.Range(0, listBBShuffle.Count);
        for (int i = 0; i < listBBShuffle.Count; i++)
        {
            for (int j = i + 1; j < listBBShuffle.Count; j++)
            {
                if (listBBShuffle[i].ID == listBBShuffle[indexHint].ID && listBBShuffle[j].ID == listBBShuffle[indexHint].ID
                    && i != indexHint && j != indexHint)
                {
                    var g1 = listBBShuffle[i];
                    var g2 = listBBShuffle[j];
                    var g3 = listBBShuffle[indexHint];


                    g1.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f);
                    g2.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1.25f);
                    g3.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1.5f);
                    return;
                }
            }
        }
    }
    void UseBoosterTimer()
    {
        timer.timeLeft += 30f;
    }
    void UseBoosterLightning()
    {
        int index = UnityEngine.Random.Range(0, listBBShuffle.Count);
        if (indexHint != -1)
        {
            if (listBBShuffle[index].ID == listBBShuffle[indexHint].ID)
            {
                index = UnityEngine.Random.Range(0, listBBShuffle.Count);
            }
        }

        for (int i = 0; i < listBBShuffle.Count; i++)
        {
            for (int j = i + 1; j < listBBShuffle.Count; j++)
            {
                if (listBBShuffle[i].ID == listBBShuffle[index].ID && listBBShuffle[j].ID == listBBShuffle[index].ID
                    && i != index && j != index)
                {
                    var g1 = listBBShuffle[i];
                    var g2 = listBBShuffle[j];
                    var g3 = listBBShuffle[index];


                    g1.transform.DOScale(new Vector3(1f, 1f, 1f), 1f)
                        .OnStart(() =>
                        {
                            g1.particleBoom.SetActive(true);
                            g1.particlePP.SetActive(true);
                        })
                        .OnComplete(() =>
                        {
                            SolveChildOfBB(g1);
                            g1.gameObject.SetActive(false);
                            listBBShuffle.Remove(g1);
                            listBB.Remove(g1);
                        });

                    g2.transform.DOScale(new Vector3(1f, 1f, 1f), 1f)
                        .OnStart(() =>
                        {
                            g2.particleBoom.SetActive(true);
                            g2.particlePP.SetActive(true);
                        })
                        .OnComplete(() =>
                        {

                            SolveChildOfBB(g2);
                            g2.gameObject.SetActive(false);
                            listBBShuffle.Remove(g2);
                            listBB.Remove(g2);
                        });

                    g3.transform.DOScale(new Vector3(1f, 1f, 1f), 1f)
                        .OnStart(() =>
                        {
                            g3.particleBoom.SetActive(true);
                            g3.particlePP.SetActive(true);
                        })
                        .OnComplete(() =>
                        {

                            SolveChildOfBB(g3);
                            g3.gameObject.SetActive(false);
                            listBBShuffle.Remove(g3);
                            listBB.Remove(g3);
                        });
                    return;
                }
            }
        }
    }
    void Update()
    {
        OnClick();
    }

    public void UpdateLine()
    {
        lineController.CreateLine(listBBShuffle);
    }

    float timeCount;
    void OnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            timeCount = Time.time;
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (Time.time - timeCount > 0.2f && isDrag) return;
                if (checkLose) return;
                if (timer.stopTimer) return;
                if (!canClick) return;
                if (listGOStored.Count > 6) return;


                RaycastHit raycastHit;
                bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 1000f, layerMask);
                if (isHit)
                {
                    Bubble bubble = raycastHit.collider.GetComponent<Bubble>();

                    if (!bubble.click && indexLevel == 0) return;

                    bubble.originalPos = bubble.transform.position;
                    bubble.particleEatt.Play();
                    AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.click);
                    if (indexLevel == 0)
                    {
                        tutorialManager.OnClick(bubble);

                    }
                    Move(bubble);

                }
            }
        }

    }
    void Move(Bubble bubble)
    {
        if (listGOStored.Count > 6 && !isHint) return;

        SolveChildOfBB(bubble);

        //bubble.StateAfterMove();
        listBB.Remove(bubble);
        listBBShuffle.Remove(bubble);
        listBubbleUndo.Add(bubble);

        if (listGOStored.Contains(bubble)) return;
        listGOStored.Add(bubble);

        List<Bubble> tempB = new List<Bubble>();
        int c = listGOStored.Count;
        while (tempB.Count < c)
        {
            tempB.Add(listGOStored[0]);
            listGOStored.RemoveAt(0);

            for (int i = 0; i < listGOStored.Count; ++i)
            {
                if (tempB[tempB.Count - 1].ID == listGOStored[i].ID)
                {
                    tempB.Add(listGOStored[i]);
                    listGOStored.RemoveAt(i);
                    --i;
                }
            }
        }
        listGOStored = tempB;

        for (int i = 0; i < listGOStored.Count; ++i)
        {
            if (!listGOStored[i].IsDone)
            {
                listGOStored[i].Move(listPoint[i], -1, CheckEat);
            }
        }


    }
    private void SolveChildOfBB(Bubble bubble)
    {
        if (bubble.hasChildren)
        {
            for (int i = bubble.children.childCount - 1; i >= 0; i--)
            {
                Transform child = bubble.children.GetChild(i);
                child.SetParent(level.transform);
                child.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);

                Bubble childBB = child.GetComponent<Bubble>();
                childBB.ResetState(childBB);
                listBBShuffle.Add(childBB);
            }

            bubble.hasChildren = false;
        }
    }
    void CheckEat()
    {
        Tweener tweener = null;
        for (int i = 0; i < listGOStored.Count - 2; ++i)
        {
            if (listGOStored[i].CanMoving && listGOStored[i + 1].CanMoving && listGOStored[i + 2].CanMoving
                && listGOStored[i].ID == listGOStored[i + 1].ID && listGOStored[i + 1].ID == listGOStored[i + 2].ID)
            {
                canEat = true;
                isHint = false;

                count -= 1;
                var g1 = listGOStored[i];
                var g2 = listGOStored[i + 1];
                var g3 = listGOStored[i + 2];
                listGOStored.Remove(g1);
                listGOStored.Remove(g2);
                listGOStored.Remove(g3);


                Instantiate(particleTest);
                particleTest.transform.position = Camera.main.WorldToScreenPoint(g2.transform.position);

                AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.eat);

                g1.particleBoom.SetActive(true);
                g2.particleBoom.SetActive(true);
                g3.particleBoom.SetActive(true);

                g1.Move(g2.transform.parent, 0.3f, () =>
                {
                    g1.gameObject.SetActive(false);
                    g2.gameObject.SetActive(false);
                    g3.gameObject.SetActive(false);

                    listBubbleUndo.Remove(g1);
                    listBubbleUndo.Remove(g2);
                    listBubbleUndo.Remove(g3);

                    canClick = true;
                });
                g2.Move(g2.transform.parent, 0.31f);
                g3.Move(g2.transform.parent, 0.31f);
                g1.IsDone = g2.IsDone = g3.IsDone = true;

                tweener = g3.tweenerMove;
                i += 2;
            }
        }
        if (tweener != null)
        {
            tweener.OnComplete(() =>
            {
                CheckDone();
            });
        }

        if (count == 0)
        {
            CheckLose();
        }
    }
    void CheckDone()
    {
        canEat = false;
        for (int i = 0; i < listGOStored.Count; ++i)
        {
            if (listGOStored[i].CanMoving)
            {
                listGOStored[i].Move(listPoint[i], 0.2f, CheckEat);
            }
        }
        hinting = false;
        CheckWin();

    }
    public IEnumerator AnimBoomBB(string str)
    {
        for (int i = 0; i < listBB.Count; ++i)
        {
            StartCoroutine(AnimBB(listBB[i]));
            SolveChildOfBB(listBB[i]);
            AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.pop);
            listBBShuffle.Remove(listBB[i]);
        }

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(str);
        DOTween.KillAll();
    }
    IEnumerator AnimBB(Bubble bb)
    {
        bb.particleBoom.SetActive(true);
        yield return new WaitForSeconds(0.005f);
        foreach (var obj in bb.objs)
        {
            obj.SetActive(false);
        }
        bb.particlePP.SetActive(true);
        yield return new WaitForSeconds(4.0f);

        bb.gameObject.SetActive(false);
    }
    public void CheckLose()
    {
        if (checkLose || listGOStored.Count <= 6 || isHint || canEat) return;

        Debug.Log("you lose");
        checkLose = true;
        timer.stopTimer = true;

        logicUI.OpenLoseUI();
        logicUI.loseUI.OpenPanelOutOfMove();

    }
    public void Lose()
    {
        winStreak = 0;
        PlayerPrefs.SetInt("WinStreak", winStreak);
        PlayerPrefs.Save();
    }
    public void CheckWin()
    {
        if (!checkLose && !checkWin && listBB.Count <= 0)
        {
            timer.stopTimer = true;
            checkWin = true;
            winStreak++;
            PlayerPrefs.SetInt("WinStreak", winStreak);
            PlayerPrefs.Save();
            AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.win);
            pathCreaterGift.DOMove(new Vector3(pathCreaterGift.position.x, -1.5f, pathCreaterGift.transform.position.z), 2f)
                .OnComplete(() =>
                {
                    logicUI.OpenWinUI();
                });
        }
    }

    public bool useByBtn;
    bool isShuffleing = false;
    List<Vector3> listNewPosShuffle = new List<Vector3>();
    public void Shuffle()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numShuffle = DataUseInGame.gameData.numShuffleItem;
        if (numShuffle <= 0) return;
        if (isShuffleing) return;
        if (useByBtn)
        {
            numShuffle--;
            if (DataUseInGame.gameData.indexLevel == 2 && isInTut)
            {
                Debug.Log("ưtf");
                logicUI.tutBtn.TutFreeze();
            }
        }

        DataUseInGame.gameData.numShuffleItem = numShuffle;
        DataUseInGame.instance.SaveData();

        listNewPosShuffle.Clear();

        List<Bubble> list = new List<Bubble>();
        int r;
        while (listBBShuffle.Count > 0)
        {
            r = UnityEngine.Random.Range(0, listBBShuffle.Count);
            list.Add(listBBShuffle[r]);
            listBBShuffle.RemoveAt(r);
        }

        listBBShuffle.AddRange(list);

        while (list.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, list.Count);
            listNewPosShuffle.Add(list[i].transform.position);
            list.RemoveAt(i);
        }

        for (int i = 0; i < listBBShuffle.Count; i++)
        {
            int currentIndex = i;
            if (canShuffle)
            {
                Physics.autoSimulation = false;
                listBBShuffle[currentIndex].transform.DOMove(listNewPosShuffle[currentIndex], 1f)
                    .OnStart(() =>
                    {
                        canShuffle = false;
                        canClick = false;
                        isShuffleing = true;
                    })

                    .OnComplete(() =>
                    {
                        Physics.autoSimulation = true;
                        canShuffle = true;
                        canClick = true;
                        isShuffleing = false;
                        useByBtn = false;
                    });
            }
        }
    }

    public bool isHint;
    public bool hinting = false;
    public void Hint()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numHint = DataUseInGame.gameData.numHintItem;
        if (numHint <= 0) return;

        isHint = true;
        if (checkLose || canEat || listGOStored.Count > 6) return;
        if (hinting) return;

        if (DataUseInGame.gameData.indexLevel == 1)
        {
            timer.stopTimer = false;
        }

        numHint--;
        logicUI.tutBtn.ImageHint.SetActive(false);

        canClick = false;
        DataUseInGame.gameData.numHintItem = numHint;
        DataUseInGame.instance.SaveData();

        if (listGOStored.Count > 0)
        {
            if (listGOStored.Count > 1)
            {
                if (listGOStored[0].ID == listGOStored[1].ID)
                {
                    for (int i = 0; i < listBB.Count; i++)
                    {
                        if (listGOStored[0].ID == listBB[i].ID)
                        {
                            //isHint = false;
                            Move(listBB[i]);
                            hinting = true;
                            return;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < listBB.Count; i++)
                    {
                        for (int j = i + 1; j < listBB.Count; j++)
                        {
                            if (listGOStored[0].ID == listBB[i].ID && listGOStored[0].ID == listBB[j].ID)
                            {
                                // isHint = false;
                                Move(listBB[i]);
                                Move(listBB[j - 1]);
                                hinting = true;

                                return;
                            }
                        }
                    }
                }
            }


            for (int i = 0; i < listBB.Count; i++)
            {
                for (int j = i + 1; j < listBB.Count; j++)
                {
                    if (listGOStored[0].ID == listBB[i].ID && listGOStored[0].ID == listBB[j].ID)
                    {
                        // isHint = false;
                        Move(listBB[i]);
                        Move(listBB[j - 1]);
                        hinting = true;

                        return;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < listBB.Count; i++)
            {
                for (int j = i + 1; j < listBB.Count; j++)
                {
                    for (int k = j + 1; k < listBB.Count; k++)
                    {
                        if (listBB[i].ID == listBB[j].ID && listBB[j].ID == listBB[k].ID)
                        {
                            // isHint = false;
                            Move(listBB[i]);
                            Move(listBB[j - 1]);
                            Move(listBB[k - 2]);
                            hinting = true;

                            return;
                        }
                    }
                }
            }

        }
    }
    public void Undo()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numUndo = DataUseInGame.gameData.numUndoItem;

        if (numUndo <= 0) return;
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;

        if (DataUseInGame.gameData.indexLevel == 2)
        {
            logicUI.tutBtn.TutShuffle();
        }

        numUndo--;
        DataUseInGame.gameData.numUndoItem = numUndo;
        DataUseInGame.instance.SaveData();

        int index = listBubbleUndo.Count - 1;
        Bubble bubble = listBubbleUndo[index];
        bubble.ResetStateIfUndo();
        //bubble.particleEat.SetActive(false);
        bubble.transform.DOMove(bubble.originalPos, 0.3f).
            OnComplete(() =>
            {
                bubble.meshCollider.enabled = true;
            });
        bubble.transform.SetParent(level.transform);
        bubble.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);

        listBB.Add(bubble);
        listBBShuffle.Add(bubble);
        listGOStored.Remove(bubble);
        listBubbleUndo.RemoveAt(index);
        for (int j = 0; j < listGOStored.Count; ++j)
        {
            listGOStored[j].Move(listPoint[j], -1, CheckDone);
        }
        return;
    }

    bool isUndoing = false;
    public void UndoTripple()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numTrippleUndo = DataUseInGame.gameData.numTrippleUndoItem;

        if (numTrippleUndo <= 0) return;
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;
        if (isUndoing) return;

        numTrippleUndo--;
        DataUseInGame.gameData.numTrippleUndoItem = numTrippleUndo;
        DataUseInGame.instance.SaveData();
        StartCoroutine(UndoTrippleCoroutine());
    }
    private IEnumerator UndoTrippleCoroutine()
    {
        isUndoing = true;
        int count = 0;

        for (int i = listBubbleUndo.Count - 1; i >= 0; --i)
        {
            if (count >= 3) break;
            count++;

            int index = listBubbleUndo.Count - 1;
            Bubble bubble = listBubbleUndo[index];
            bubble.ResetStateIfUndo();
            //bubble.particleEat.SetActive(false);
            bubble.transform.DOMove(bubble.originalPos, 0.3f)
                .OnComplete(() =>
                {
                    bubble.meshCollider.enabled = true;
                });
            bubble.transform.SetParent(level.transform);
            bubble.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
            listBB.Add(bubble);
            listBBShuffle.Add(bubble);
            listGOStored.Remove(bubble);
            listBubbleUndo.RemoveAt(index);
            yield return new WaitForSeconds(0.2f);

            for (int j = 0; j < listGOStored.Count; ++j)
            {
                listGOStored[j].Move(listPoint[j], -1, CheckDone);
            }
        }

        isUndoing = false;
    }
    public void UndoAll()
    {
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;
        if (isUndoing) return;

        Debug.Log(canClick);

        StartCoroutine(UndoAllCoroutine());
    }
    private IEnumerator UndoAllCoroutine()
    {
        isUndoing = true;
        for (int i = listBubbleUndo.Count - 1; i >= 0; --i)
        {
            int index = listBubbleUndo.Count - 1;
            Bubble bubble = listBubbleUndo[index];
            bubble.ResetStateIfUndo();
            //bubble.particleEat.SetActive(false);
            bubble.transform.DOMove(bubble.originalPos, 0.3f)
                .OnComplete(() =>
                {
                    bubble.meshCollider.enabled = true;
                });
            bubble.transform.SetParent(level.transform);
            bubble.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
            listBB.Add(bubble);
            listBBShuffle.Add(bubble);
            listGOStored.Remove(bubble);
            listBubbleUndo.RemoveAt(index);
            yield return new WaitForSeconds(0.2f);
        }

        Shuffle();
        canClick = true;
        Debug.Log(canClick);
        isUndoing = false;
        timer.stopTimer = false;
    }

    bool isFreezeing = false;
    public void Freeze()
    {
        canClick = true;
        logicUI.tutBtn.ImageHint.SetActive(false);
        isInTut = false;

        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numFreeze = DataUseInGame.gameData.numFreezeTimeItem;
        if (numFreeze <= 0) return;
        if (isFreezeing) return;

        timer.stopTimer = false;

        numFreeze--;
        DataUseInGame.gameData.numFreezeTimeItem = numFreeze;
        DataUseInGame.instance.SaveData();

        timer.isFreeze = true;
        StartCoroutine(StopFreeze());
    }
    IEnumerator StopFreeze()
    {
        isFreezeing = true;
        yield return new WaitForSeconds(5f);
        timer.isFreeze = false;
        isFreezeing = false;
    }
    public IEnumerator CanClickAgain()
    {
        yield return new WaitForSeconds(0.4f);
        if (listGOStored.Count > 6)
        {
            UndoAll();
        }
        timer.stopTimer = false;
        canClick = true;
    }
}
