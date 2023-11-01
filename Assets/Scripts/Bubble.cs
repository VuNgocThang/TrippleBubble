using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int ID;
    public Vector3 originalPos;
    public bool hasChildren;
    public bool isChild;
    public float radius;
    public Transform children;
    public Transform connectPoint;
    public Material mat;
    public Texture2DArray bubblTexture2d;
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
    }
    public void SetColor(int index)
    {
        Material tempMat = new Material(Shader.Find("Shader Graphs/BubbleTextureArray"));
        tempMat.SetTexture("_Texture2D_Array", bubblTexture2d);
        tempMat.SetFloat("Value", 0f);
        tempMat.SetFloat("_Index", index);
        MeshRenderer.sharedMaterial = tempMat;
        mat = MeshRenderer.sharedMaterial;
    }
    public void Move(Transform parent, float time = -1, Action checkEat = null)
    {
        IsMoving = true;
        transform.SetParent(parent);
        tweenerMove = transform.DOLocalMove(Vector3.zero, time == -1 ? 0.3f : time);
        transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f);
        SetOpacity(1f);
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

            SetOpacity(0.6f);
        }
        else
        {
            SetOpacity(1f);
        }

    }
    public void SetOpacity(float o)
    {
        Material mat = GetComponent<MeshRenderer>().material;   
        Color currentColor = mat.color;
        currentColor.a = o;
        mat.color = currentColor;

    }

}
