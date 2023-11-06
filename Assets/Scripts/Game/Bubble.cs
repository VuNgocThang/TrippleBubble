using System;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int ID;
    public Vector3 originalPos;
    public Vector3 originalScale;
    public bool hasChildren;
    public bool isChild;
    public float radius;
    public Transform children;
    public Transform connectPoint;
    public Material mat;
    public Texture2DArray bubblTexture2d;
    public List<GameObject> objs = new List<GameObject>();
    public List<Material> mats = new List<Material>();
    public bool canMoveHT;
    public MeshCollider meshCollider;
    public Rigidbody rb;
    public bool IsMoving;
    public bool IsDone;
    public Tweener tweenerMove;
    public bool CanMoving => !IsMoving && !IsDone;

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
    }
    
    
    private void FixedUpdate()
    {
        MoveHT();
    }

    public void Init(int id)
    {
        ID = id;
        SetColor(id);
    }
    public void SetColor(int index)
    {
        int i = SetIndexObjs(index);
        objs[i].SetActive(true);
        if (hasChildren)
        {
            SelectMaterial(1);
        }
        else
        {
            SelectMaterial(0);
        }
        objs[i].GetComponent<MeshRenderer>().material.SetFloat("_Index", index + 1);
    }
    public int SetIndexObjs(int i)
    {
        int result;

        switch (i / 9)
        {
            case 0:
                result = 0;
                break;
            case 1:
                result = 1;
                break;
            case 2:
                result = 2;
                break;
            default:
                result = 0;
                break;
        }
        return result;
    }
    public void Move(Transform parent, float time = -1, Action checkEat = null)
    {
        IsMoving = true;
        transform.SetParent(parent);
        tweenerMove = transform.DOLocalMove(Vector3.zero, time == -1 ? 0.3f : time);
        transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f);

        SelectMaterial(0);
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].activeSelf)
            {
                objs[i].GetComponent<MeshRenderer>().material.SetFloat("_Index", ID + 1);
            }
        }

        tweenerMove.OnStart(() =>
        {
            LogicGame.instance.count += 1;
        });
        tweenerMove.OnComplete(() =>
        {
            LogicGame.instance.count -= 1;
            IsMoving = false;
            checkEat?.Invoke();
        });
    }
    public void MoveHT()
    {
        if (canMoveHT)
        {
            rb.velocity += (LogicGame.instance.targetHT.position - transform.position) * 0.01f;
        }
    }
    public void SelectMaterial(int index)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].activeSelf)
            {
                objs[i].GetComponent<MeshRenderer>().sharedMaterial = mats[index];
            }
        }
    }
    public void CheckHasChild()
    {
        if (children.childCount > 0)
        {
            hasChildren = true;
        }

        if (hasChildren)
        {
            for (int i = 0; i < children.childCount; i++)
            {
                Bubble child = children.GetChild(i).GetComponent<Bubble>();
                SetStateIfIsChild(child);
            }
        }
    }

    public void SetStateIfIsChild(Bubble bb)
    {
        bb.isChild = true;
        bb.canMoveHT = false;
        bb.meshCollider.enabled = false;
        bb.rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void ResetState(Bubble bb)
    {
        bb.isChild = false;
        bb.canMoveHT = true;
        bb.meshCollider.enabled = true;
        bb.rb.constraints &= ~RigidbodyConstraints.FreezePosition; // bỏ freeze pos
        bb.rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
    }

    public void StateAfterMove()
    {
        meshCollider.enabled = false;
        canMoveHT = false;
    }

    public void ResetStateIfUndo()
    {
        meshCollider.enabled = true;
        canMoveHT = true;
    }
}
