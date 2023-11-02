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
    public Transform targetObject;
    MeshRenderer meshRenderer;
    MeshRenderer MeshRenderer
    {
        get
        {
            if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
            return meshRenderer;
        }
    }

    public bool IsMoving;
    public bool IsDone;
    public Tweener tweenerMove;
    public bool CanMoving => !IsMoving && !IsDone;

    public void Init(int id)
    {
        ID = id;
        SetColor(id);
        targetObject = LogicGame.instance.target;
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

    public void LookAt()
    {
        //if (targetObject != null)
        //{
        //    Vector3 direction = targetObject.position - connectPoint.position;
        //    Quaternion rotation = Quaternion.LookRotation(direction);
        //    transform.rotation = rotation;
        //}
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
                child.isChild = true;
            }
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

}
