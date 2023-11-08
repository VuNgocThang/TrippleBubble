﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation;
using TMPro;
using UnityEngine;

public class LogicGame : MonoBehaviour
{
    public static LogicGame instance;
    public LogicGameUI logicUI;
    public List<Bubble> listBB;
    [SerializeField] List<Bubble> listBBShuffle;
    [SerializeField] List<GameObject> listObject;
    [SerializeField] List<Bubble> listGOStored = new List<Bubble>();
    [SerializeField] LevelSetMap prefabLevel;
    [SerializeField] List<Transform> listPoint = new List<Transform>();
    [SerializeField] LineController lineController;
    [SerializeField] List<Bubble> listBubbleUndo = new List<Bubble>();
    [SerializeField] Timer timer;
    [SerializeField] LayerMask layerMask;
    public LevelSetMap level;
    public PathCreator pathCreater;
    public int count;
    public bool an = false;
    public bool isDrag;
    public bool canShuffle;
    public bool checkLose;
    public bool checkWin;
    bool canEat;
    public int currentIndex;
    public int nextIndex;
    int winStreak;
    public TextMeshProUGUI txtCombo;
    public Transform targetHT;
    public bool canClick;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("BoosterHint"));
        Debug.Log(PlayerPrefs.GetInt("BoosterTimer"));
        Debug.Log(PlayerPrefs.GetInt("BoosterLightning"));
      
        Application.targetFrameRate = 60;
        canClick = true;
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
        Debug.Log(winStreak + " WinStreak");

        Init();
        canShuffle = true;
    }
    void Init()
    {
        level = Instantiate(prefabLevel, transform);

        int count = listObject.Count;
        int countAll = level.bubbles.Count;
        int max = level.maxEach;

        List<int> listRandom = new List<int>();
        int[] arr = new int[count];
        while (countAll > 0)
        {
            int index = UnityEngine.Random.Range(0, count);
            if (arr[index] < max)
            {
                arr[index] += 3;
                for (int y = 0; y < 3; y++)
                {
                    listRandom.Add(index);
                    countAll--;
                }
            }
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

        for (int i = 0; i < level.bubbles.Count; i++)
        {
            listBB.Add(level.bubbles[i]);
        }

        for (int i = 0; i < listRandom.Count; i++)
        {
            listBB[i].CheckHasChild();
            listBB[i].Init(listRandom[i]);
            listBB[i].originalPos = listBB[i].transform.position;
            listBB[i].originalScale = listBB[i].transform.localScale;
        }

        foreach (Bubble bubble in listBB)
        {
            if (!bubble.isChild)
            {
                listBBShuffle.Add(bubble);
            }
        }

    }
    void Update()
    {
        lineController.CreateLine(listBBShuffle);
        OnClick();

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

                RaycastHit raycastHit;
                bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 1000f, layerMask);
                if (isHit)
                {
                    Bubble bubble = raycastHit.collider.GetComponent<Bubble>();
                    Move(bubble);
                }
            }
        }

    }
    void Move(Bubble bubble)
    {
        if (listGOStored.Count > 6 && !isHint) return;

        SolveChildOfBB(bubble);

        bubble.StateAfterMove();

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
                count -= 1;
                var g1 = listGOStored[i];
                var g2 = listGOStored[i + 1];
                var g3 = listGOStored[i + 2];
                listGOStored.Remove(g1);
                listGOStored.Remove(g2);
                listGOStored.Remove(g3);
                g1.Move(g2.transform.parent, 0.3f, () =>
                {
                    g1.gameObject.SetActive(false);
                    g2.gameObject.SetActive(false);
                    g3.gameObject.SetActive(false);

                    listBubbleUndo.Remove(g1);
                    listBubbleUndo.Remove(g2);
                    listBubbleUndo.Remove(g3);
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
    public void CheckLose()
    {
        if (!checkLose && listGOStored.Count > 6 && !isHint && !canEat)
        {
            Debug.Log("you lose");
            checkLose = true;
            timer.stopTimer = true;
            logicUI.OpenLoseUI();
            logicUI.loseUI.OpenPanelOutOfMove();
        }

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
            Debug.Log("you win");
            checkWin = true;
            winStreak++;
            PlayerPrefs.SetInt("WinStreak", winStreak);
            PlayerPrefs.Save();
            logicUI.OnWinUI();

        }
    }
    public List<Vector3> listNewPosShuffle = new List<Vector3>();
    public void Shuffle()
    {
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
                    })

                    .OnComplete(() =>
                    {
                        canShuffle = true;
                        Physics.autoSimulation = true;
                        canClick = true;
                    });
            }
        }
    }

    bool isHint;
    bool hinting = false;
    public void Hint()
    {
        isHint = true;
        if (checkLose || canEat || listGOStored.Count > 6) return;
        if (hinting) return;

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
                            Move(listBB[i]);
                            hinting = true;
                            isHint = false;
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
                                Move(listBB[i]);
                                Move(listBB[j - 1]);
                                hinting = true;
                                isHint = false;

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
                        Move(listBB[i]);
                        Move(listBB[j - 1]);
                        hinting = true;
                        isHint = false;

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
                            Move(listBB[i]);
                            Move(listBB[j - 1]);
                            Move(listBB[k - 2]);
                            hinting = true;
                            isHint = false;

                            return;
                        }
                    }
                }
            }

        }
    }
    public void Undo()
    {
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;

        int index = listBubbleUndo.Count - 1;
        Bubble bubble = listBubbleUndo[index];
        bubble.ResetStateIfUndo();
        bubble.transform.DOMove(bubble.originalPos, 0.3f);
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
    public void UndoTripple()
    {
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;

        StartCoroutine(UndoTrippleCoroutine());
    }
    private IEnumerator UndoTrippleCoroutine()
    {
        int count = 0;
        for (int i = listBubbleUndo.Count - 1; i >= 0; --i)
        {
            if (count >= 3) break;
            count++;

            int index = listBubbleUndo.Count - 1;
            Bubble bubble = listBubbleUndo[index];
            bubble.ResetStateIfUndo();
            bubble.transform.DOMove(bubble.originalPos, 0.3f);
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
    }
    public void UndoAll()
    {
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;

        StartCoroutine(UndoAllCoroutine());
    }
    private IEnumerator UndoAllCoroutine()
    {
        for (int i = listBubbleUndo.Count - 1; i >= 0; --i)
        {
            int index = listBubbleUndo.Count - 1;
            Bubble bubble = listBubbleUndo[index];
            bubble.ResetStateIfUndo();
            bubble.transform.DOMove(bubble.originalPos, 0.3f);
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
    }
    public void Freeze()
    {
        timer.isFreeze = true;
        StartCoroutine(StopFreeze());
    }
    IEnumerator StopFreeze()
    {
        yield return new WaitForSeconds(5f);
        timer.isFreeze = false;
    }
    public void SubHeart()
    {
        int heart = PlayerPrefs.GetInt("NumHeart");

        if (heart >= 5)
        {
            PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        heart--;
        PlayerPrefs.SetInt("NumHeart", heart);
        PlayerPrefs.Save();
    }
}
