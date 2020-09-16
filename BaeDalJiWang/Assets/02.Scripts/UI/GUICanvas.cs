﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICanvas : MonoBehaviour
{
    // ########################################
    // Variables
    // ########################################

    #region Variables

    // Canvas
    public Canvas m_Canvas;

    // GUIAnimFREE object of Dialog
    public GUIAnimFREE m_Dialog;

    public bool end;
    #endregion // Variables

    // ########################################
    // MonoBehaviour Functions
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    // ########################################

    #region MonoBehaviour

    // Awake is called when the script instance is being loaded.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
    void Awake()
    {
        if (enabled)
        {
            // Set GUIAnimSystemFREE.Instance.m_AutoAnimation to false in Awake() will let you control all GUI Animator elements in the scene via scripts.
            GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
        }
    }

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
    public void SetUpStart()
    {
        end = false;
        // MoveIn m_Title1 and m_Title2
        StartCoroutine(MoveInTitleGameObjects());

        // Disable all scene switch buttons
        // http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, false);
    }
    #endregion // MonoBehaviour

    // ########################################
    // MoveIn/MoveOut functions
    // ########################################

    #region MoveIn/MoveOut

    // MoveIn m_Title1 and m_Title2
    IEnumerator MoveInTitleGameObjects()
    {
        yield return new WaitForSeconds(0.5f);

        // MoveIn m_Dialog
        StartCoroutine(ShowDialog());
    }

    // MoveIn m_Dialog
    IEnumerator ShowDialog()
    {
        yield return new WaitForSeconds(0.5f);

        // MoveIn m_Dialog
        m_Dialog.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
        
        // Enable all scene switch buttons
        StartCoroutine(EnableAllDemoButtons());
    }

    // MoveOut m_Dialog
    public void HideAllGUIs()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // MoveOut m_Title1 and m_Title2
        StartCoroutine(HideTitleTextMeshes());
    }

    // MoveOut m_Title1 and m_Title2
    IEnumerator HideTitleTextMeshes()
    {
        yield return new WaitForSeconds(0.5f);
    }

    #endregion // MoveIn/MoveOut

    // ########################################
    // Enable/Disable button functions
    // ########################################

    #region Enable/Disable buttons

    // Enable/Disable all scene switch Coroutine
    IEnumerator EnableAllDemoButtons()
    {
        end = true;
        yield return new WaitForSeconds(0.5f);

        // Enable all scene switch buttons
        // http://docs.unity3d.com/Manual/script-GraphicRaycaster.html
        GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(m_Canvas, true);
    }

    // Disable all buttons for a few seconds
    IEnumerator DisableAllButtonsForSeconds(float DisableTime)
    {
        // Disable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(false);

        yield return new WaitForSeconds(DisableTime);

        // Enable all buttons
        GUIAnimSystemFREE.Instance.EnableAllButtons(true);
    }

    #endregion // Enable/Disable buttons

    // ########################################
    // UI Responder functions
    // ########################################

    #region UI Responder

    public void OnButton_UpperEdge()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // MoveIn m_Dialog from top
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperScreenEdge));
    }

    public void OnButton_LeftEdge()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // MoveIn m_Dialog from left
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.LeftScreenEdge));
    }

    public void OnButton_RightEdge()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Disable all buttons for a few seconds
        StartCoroutine(DisableAllButtonsForSeconds(2.0f));

        // MoveIn m_Dialog from right
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.RightScreenEdge));
    }

    public void OnButton_BottomEdge()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Disable all buttons for a few seconds
        StartCoroutine(DisableAllButtonsForSeconds(2.0f));

        // MoveIn m_Dialog from bottom
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomScreenEdge));
    }

    public void OnButton_UpperLeft()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Disable all buttons for a few seconds
        StartCoroutine(DisableAllButtonsForSeconds(2.0f));

        // MoveIn m_Dialog from upper left
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperLeft));
    }

    public void OnButton_UpperRight()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Disable all buttons for a few seconds
        StartCoroutine(DisableAllButtonsForSeconds(2.0f));

        // MoveIn m_Dialog from upper right
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.UpperRight));
    }

    public void OnButton_BottomLeft()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Disable all buttons for a few seconds
        StartCoroutine(DisableAllButtonsForSeconds(2.0f));

        // MoveIn m_Dialog from bottom left
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomLeft));
    }

    public void OnButton_BottomRight()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Disable all buttons for a few seconds
        StartCoroutine(DisableAllButtonsForSeconds(2.0f));

        // MoveIn m_Dialog from bottom right
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.BottomRight));
    }

    public void OnButton_Center()
    {
        // MoveOut m_Dialog
        m_Dialog.PlayOutAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);

        // Disable all buttons for a few seconds
        StartCoroutine(DisableAllButtonsForSeconds(0.5f));

        // MoveIn m_Dialog from center of screen
        StartCoroutine(DialogMoveIn(GUIAnimFREE.ePosMove.MiddleCenter));
    }

    #endregion // UI Responder

    // ########################################
    // Move dialog functions
    // ########################################

    #region Move Dialog

    // MoveIn m_Dialog by position
    IEnumerator DialogMoveIn(GUIAnimFREE.ePosMove PosMoveIn)
    {
        yield return new WaitForSeconds(0.5f);

        //Debug.Log("PosMoveIn="+PosMoveIn);
        switch (PosMoveIn)
        {
            // Set m_Dialog to move in from upper
            case GUIAnimFREE.ePosMove.UpperScreenEdge:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperScreenEdge;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from left
            case GUIAnimFREE.ePosMove.LeftScreenEdge:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.LeftScreenEdge;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from right
            case GUIAnimFREE.ePosMove.RightScreenEdge:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.RightScreenEdge;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from bottom
            case GUIAnimFREE.ePosMove.BottomScreenEdge:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomScreenEdge;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from upper left
            case GUIAnimFREE.ePosMove.UpperLeft:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperLeft;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from upper right
            case GUIAnimFREE.ePosMove.UpperRight:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.UpperRight;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from bottom left
            case GUIAnimFREE.ePosMove.BottomLeft:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomLeft;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from bottom right
            case GUIAnimFREE.ePosMove.BottomRight:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.BottomRight;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
            // Set m_Dialog to move in from center
            case GUIAnimFREE.ePosMove.MiddleCenter:
            default:
                m_Dialog.m_MoveIn.MoveFrom = GUIAnimFREE.ePosMove.MiddleCenter;
                m_Dialog.m_MoveOut.MoveTo = GUIAnimFREE.ePosMove.MiddleCenter;
                break;
        }

        // Reset m_Dialog
        m_Dialog.Reset();

        // MoveIn m_Dialog by position
        m_Dialog.PlayInAnims(GUIAnimSystemFREE.eGUIMove.SelfAndChildren);
    }

    #endregion //  Move Dialog
}
