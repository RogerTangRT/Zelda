using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Treasure : Interactable
{
    #region Enumerado. Máquina de estado Treasure
    enum TreasureState
    {
        Close,
        Opening,
        Open
    }
    #endregion

    #region variaveis
    /// <summary>
    /// Estado da Abertura do Terouso
    /// </summary>
    private TreasureState m_TreasureState;

    public Item m_Contents;
    public Inventory m_PlayerInventory;
    public SignalList m_RaiseItem;
    public GameObject m_DialogBox;
    public Text m_DialogText;
    #endregion

    public override void Start()
    {
        base.Start();
        m_TreasureState = TreasureState.Close;
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void KeyPressed()
    {
        switch (m_TreasureState)
        {
            case TreasureState.Close:
                {
                    OpenTreasure();
                }
                break;
            case TreasureState.Opening:
                {
                    TreasureAlreadyOpen();
                }
                break;
        }
    }
    private IEnumerator OpenTreasureAnimation()
    {
        m_Animator.SetBool("open", true);
        yield return new WaitForSeconds(2f);

        if (m_DialogBox != null)
        {
            // Dialog windows on
            m_DialogBox.SetActive(true);
            // Dialog Text = contensts text
            m_DialogText.text = m_Contents.m_ItemDescription;

            // Add contents to inventory
            m_PlayerInventory.AddItem(m_Contents);
            m_PlayerInventory.m_currentItem = m_Contents;
            // Raise the signal to the player to animate set the treasure to open

            // Raise the signal to the player to stop animating
            // RAISE
            // Item:            Signal_ReceiveItem
            // Local:           ScriptableObjects/Player
            // Capturado por:   Player->Receive Item
            // Método:          Scripts/Player Scripts/PlayerMovment->RaiseItem()
            m_RaiseItem.Raise();
        }
        else
            m_TreasureState = TreasureState.Open;

        // RAISE
        // Signal:          Signal_ContextClueOff
        // Local Signal:    ScriptableObjects/Context Clue
        // Capturado por:   Player
        // Método:          Scripts/Player Scrips/ContextClue->Disable()
        m_ContextOff.Raise();
        m_UseContext = false;
    }
    public void OpenTreasure()
    {
        m_TreasureState = TreasureState.Opening;
        StartCoroutine(OpenTreasureAnimation());
    }
    public void TreasureAlreadyOpen()
    {
        if (m_TreasureState == TreasureState.Opening)
        {
            // Dialog windows off
            m_DialogBox.SetActive(false);
            // Set the current item to empty
            m_PlayerInventory.m_currentItem = null;

            // Raise the signal to the player to stop animating
            // RAISE
            // Item:            Signal_ReceiveItem
            // Local:           ScriptableObjects/Player
            // Capturado por:   Player->Receive Item
            // Método:          Scripts/Player Scripts/PlayerMovment->RaiseItem()
            m_RaiseItem.Raise();
            // Set the treasure opened
            m_TreasureState = TreasureState.Open;
        }
    }
}
