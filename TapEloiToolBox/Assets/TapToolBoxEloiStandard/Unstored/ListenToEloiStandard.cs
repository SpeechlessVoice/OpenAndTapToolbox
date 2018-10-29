﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenToEloiStandard : MonoBehaviour, ITapListener
{

    private TapInputType m_context = TapInputType.EloiStandard;
    public bool IsListening()
    {
        return m_listenToEloiStandard;
    }

    public void SetListenerTo(bool on)
    {
        m_listenToEloiStandard = on;
    }

    public void Switch()
    {
        SetListenerTo(!IsListening());
    }


    public bool m_listenToEloiStandard = true;
    public OnHandValueDetected m_onHandComboDetected;

  

    void Update()
    {

        string input = Input.inputString;
        if (input.Length > 0)
        {
            foreach (char c in input.ToCharArray())
            {
                if (m_listenToEloiStandard)
                {

                    HandTapValue eloiValue = TapUtility.GetTapBasedOnEloiStandard(c);
                    m_onHandComboDetected.Invoke(eloiValue);
                    if(toDoOnhandsvalueDetected!=null)
                    toDoOnhandsvalueDetected(this, TapUtility.ConvertToHandsValue(eloiValue));
                }
            }
        }
    }

    public string GetName()
    {
        return m_context.ToString();
    }

    public TapInputType GetListenerType()
    {
        return m_context;
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    
    public ToDoOnHandsTapValueDetected toDoOnhandsvalueDetected;

    

    public void AddListener(ToDoOnHandsTapValueDetected listener)
    {
        RemoveListener(listener);
        toDoOnhandsvalueDetected += listener;
    }

    public void RemoveListener(ToDoOnHandsTapValueDetected listener)
    {
        toDoOnhandsvalueDetected -= listener;
    }

    public void AddListener(ToDoOnTapValueDetected listener)
    {
    }

    public void RemoveListener(ToDoOnTapValueDetected listener)
    {
    }
}

public interface ITapListener {

    void SetListenerTo(bool setOn);
    bool IsListening();
    void Switch();
    string GetName();
    TapInputType GetListenerType();
    GameObject GetGameObject();

    void AddListener(ToDoOnTapValueDetected listener);
    void RemoveListener(ToDoOnTapValueDetected listener);

    void AddListener(ToDoOnHandsTapValueDetected listener);
    void RemoveListener(ToDoOnHandsTapValueDetected listener);


}

public delegate void ToDoOnTapValueDetected(ITapListener listener, TapValue value);
public delegate void ToDoOnHandsTapValueDetected(ITapListener listener, HandsTapValue value);
