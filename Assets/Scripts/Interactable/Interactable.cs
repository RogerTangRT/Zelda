using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    protected Animator m_Animator;

    private ActionButton m_ActionButton;
    private bool m_PlayerInRange;
    private PlayerMovment m_PlayerMovment;

    public SignalList m_ContextOn;
    public SignalList m_ContextOff;

    // Start is called before the first frame update
    public virtual void Start()
    {
        m_Animator = GetComponent<Animator>();
        GameObject Player = GameObject.Find("Player");
        m_PlayerMovment = Player.GetComponent<PlayerMovment>();

        m_ActionButton = FindObjectOfType<ActionButton>();
        m_ActionButton.clickDown += Button_clickDown;
        m_ActionButton.clickUp += Button_clickUp;
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
            if (m_ContextOn != null)
                // RAISE
                // Signal:          Signal_ContextClueOn
                // Local Signal:    ScriptableObjects/Context Clue
                // Capturado por:   Player
                // Método:          Scripts/Player Scrips/ContextClue->Enable()
                m_ContextOn.Raise();
            m_PlayerMovment.Interacting(true);
            m_PlayerInRange = true;
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if (m_ContextOff != null)
                // RAISE
                // Signal:          Signal_ContextClueOff
                // Local Signal:    ScriptableObjects/Context Clue
                // Capturado por:   Player
                // Método:          Scripts/Player Scrips/ContextClue->Disable()
                m_ContextOff.Raise();
            m_PlayerMovment.Interacting(false);
            m_PlayerInRange = false;
        }
    }
}
