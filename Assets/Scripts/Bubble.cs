using System;
using System.Collections.Generic;
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
        SetColor(id + 1);
        targetObject = LogicGame.instance.target;
    }
    public void SetColor(int index)
    {
        int i = SetIndexObjs(index);
        objs[i].SetActive(true);
        objs[i].GetComponent<MeshRenderer>().material.SetFloat("_Index", index);
        //MeshRenderer.material.SetFloat("_Index", index);
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

    public int SetIndexObjs(int index)
    {
        int result;

        switch (index / 9)
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
        //SetOpacity(1f);
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

            //SetOpacity(0.6f);
        }
        //else
        //{
        //    SetOpacity(1f);
        //}

    }
    //public void SetOpacity(float o)
    //{
    //    Material mat = GetComponent<MeshRenderer>().material;
    //    //Color currentColor = mat.color;
    //    mat.SetColor("MainColor", new Color(1f, 1f, 1f, o));
    //    //currentColor.a = o;
    //    //mat.color = currentColor;

    //}

}
