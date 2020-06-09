using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Apresenta o texto no Pergaminho
public class Sign : Interactable
{
    public string m_Message;
    public Text m_DialogText;
    public GameObject m_DialogBox;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void KeyPressed()
    {
        if (m_DialogBox != null)
        {
            // Quando Ativa o controle ele fica na Hierarquia
            if (m_DialogBox.activeInHierarchy)
                m_DialogBox.SetActive(false);
            else
            {
                m_DialogBox.SetActive(true);
                m_DialogText.text = m_Message;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.CompareTag("PlayerBody"))
            if (m_DialogBox != null)
                m_DialogBox.SetActive(false);
    }
}
