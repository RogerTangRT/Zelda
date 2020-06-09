﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string m_SceneToLoad;
    public Vector2 m_PlayerPosition;
    public VectorValue m_PlayerStorege;
    public GameObject m_FadeInPanel;
    public GameObject m_FadeOutPanel;
    public float m_FadeWait;

    public void Awake()
    {
        if (m_FadeInPanel != null)
        {
            GameObject panel = Instantiate(m_FadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            // Destroi painel depois de 1 segundo
            Destroy(panel, 1);
        }
    }
    public IEnumerator FadeCoroutine()
    {

        if (m_FadeOutPanel != null)
        {
            Instantiate(m_FadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(m_FadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_SceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody") && collision.isTrigger)
        {
            m_PlayerStorege.m_InitialValue = m_PlayerPosition;
            // SceneManager.LoadScene(m_SceneToLoad);
            StartCoroutine(FadeCoroutine());
        }
    }
}