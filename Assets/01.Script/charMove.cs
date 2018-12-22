using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMove : Character
{
    Dictionary<KeyCode, Action> _charMoveDic = new Dictionary<KeyCode, Action>();
    void Start ()
    {
        _charMoveDic.Add(KeyCode.LeftArrow, debug1);
        _charMoveDic.Add(KeyCode.RightArrow, debug2);
        _charMoveDic.Add(KeyCode.UpArrow, debug3);
        _charMoveDic.Add(KeyCode.DownArrow, debug4);
    }

    private void MovePos()
    {
        throw new NotImplementedException();
    }

    void Update ()
    {
    }

    void debug1()
    {
        Debug.Log("LEFT");
    }

    void debug2()
    {
        Debug.Log("RIGHT");
    }

    void debug3()
    {
        Debug.Log("UP");
    }

    void debug4()
    {
        Debug.Log("DOWN");
    }

    public void InputKeyMove()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in _charMoveDic)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }
}
