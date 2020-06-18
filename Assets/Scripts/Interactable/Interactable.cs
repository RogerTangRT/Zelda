using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private ActionButton m_ActionButton;

    protected Animator m_Animator;
    protected bool m_UseContext = true;
    protected PlayerMovment m_PlayerMovment;

    public bool m_PlayerInRange;
    public SignalList m_ContextOn;
    public SignalList m_ContextOff;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_Animator = GetComponent<Animator>();
        GameObject Player = GameObject.Find("Player");
        m_PlayerMovment = Player.GetComponent<PlayerMovment>();

        m_ActionButton = FindObjectOfType<ActionButton>();
        if (m_ActionButton != null)
        {
            m_ActionButton.clickDown += Button_clickDown;
            m_ActionButton.clickUp += Button_clickUp;
        }
    }
    private bool ActionKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public virtual void Update()
    {
        if (m_PlayerInRange)
            if (ActionKeyPressed())
                KeyPressed();
    }
    public virtual void KeyPressed()
    {

    }
    public void Button_clickDown(object sender, System.EventArgs e)
    {
        if (m_PlayerInRange)
            KeyPressed();
    }
    public virtual void Button_clickUp(object sender, System.EventArgs e)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (m_ContextOn != null && m_UseContext)
                // RAISE
                // Signal:          Signal_ContextClueOn
                // Local Signal:    ScriptableObjects/Context Clue
                // Capturado por:   Player
                // Método:          Scripts/Player Scrips/ContextClue.cs->Enable()
                m_ContextOn.Raise();
            m_PlayerMovment.Interacting(true);
            m_PlayerInRange = true;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (m_ContextOff != null && m_UseContext)
                // RAISE
                // Signal:          Signal_ContextClueOff
                // Local Signal:    ScriptableObjects/Context Clue
                // Capturado por:   Player
                // Método:          Scripts/Player Scrips/ContextClue.cs->Disable()
                m_ContextOff.Raise();
            m_PlayerMovment.Interacting(false);
            m_PlayerInRange = false;
        }
    }
}
