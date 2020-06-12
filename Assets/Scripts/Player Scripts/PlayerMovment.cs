using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public enum PlayerState
    {
        idle,
        walk,
        attack,
        interact,
        stagger,
        receiving,
        die
    }

    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_PositionChange;
    private Animator m_Animator;
    private float m_Speed;

    protected Joystick m_Joystick;
    protected ActionButton m_ActionButton;

    public FloatValue m_PlayerSpeed;
    public PlayerState m_currentState;
    public FloatValue m_currentHealth;
    public SignalList m_PlayerHealthSignalList;
    public VectorValue m_StartingPosition;
    public Inventory m_PlayerInventory;
    public SpriteRenderer m_ReceivedItemSprite;
    public Item m_Sword;

    public void Interacting(bool interacting)
    {
        if (interacting)
            m_currentState = PlayerState.interact;
        else
            m_currentState = PlayerState.idle;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Speed = m_PlayerSpeed.m_InitialValue;
        // Posição Inicial. Quando Sai da Casa
        transform.position = m_StartingPosition.m_InitialValue;

        m_currentState = PlayerState.idle;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_Animator = GetComponent<Animator>();
        // Impede que os Hit Box sejam todos disparados ao mesmo tempo quando o botão de ataque é pressionado na posição inicial do jogo.
        // Isto faz com que apenas um GameObject seja disparado dependendo se sua posição.
        // Posição (0,0) dispara todos os Hits. Quando o personagem for movimentado, ele sairá da posição (0,0) e irá para uma das posições possíveis que são:
        // (-1,0),(1,0),(0,-1),(0,1).
        // Seta para posição inicial para do personagem para Down (0,-1). 

        m_Animator.SetFloat("moveX", 0);
        m_Animator.SetFloat("moveY", -1);


        m_Joystick = FindObjectOfType<Joystick>();

        m_ActionButton = FindObjectOfType<ActionButton>();
        if (m_ActionButton != null)
        {
            m_ActionButton.clickDown += Button_clickDown;
            m_ActionButton.clickUp += Button_clickUp;
        }
    }
    private void Button_clickDown(object sender, System.EventArgs e)
    {
        Attack();
    }
    private void Button_clickUp(object sender, System.EventArgs e)
    {
        // m_Animator.SetBool("attaking", false);
        // m_currentState = PlayerState.walk;
    }
    private void GetMovmentControllers()
    {
        m_PositionChange = Vector3.zero;
        // GetAxisRaw Get only 0 or One. GetAxis Get float value.

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0)
            m_PositionChange.x = x;
        else
        {
            if (m_Joystick != null)
                m_PositionChange.x = m_Joystick.Horizontal;
        }

        if (y != 0)
            m_PositionChange.y = y;
        else
        {
            if (m_Joystick != null)
                m_PositionChange.y = m_Joystick.Vertical;
        }

        //m_Animator.SetBool("carrying", Input.GetKey(KeyCode.C));
    }
    public void ReceiveItem()
    {
        if (m_currentState != PlayerState.receiving)
        {
            m_Animator.SetBool("receive item", true);
            m_currentState = PlayerState.receiving;
            if (m_ReceivedItemSprite != null)
                m_ReceivedItemSprite.sprite = m_PlayerInventory.m_currentItem.m_ItemSprite;
        }
        else
        {
            m_Animator.SetBool("receive item", false);
            if (m_ReceivedItemSprite != null)
                m_ReceivedItemSprite.sprite = null;
            m_currentState = PlayerState.interact;
        }
    }
    private IEnumerator Attack_Coroutine()
    {
        if (m_currentState != PlayerState.interact && m_currentState != PlayerState.stagger)
        {
            m_currentState = PlayerState.attack;
            m_Animator.SetBool("attaking", true);
            // 1 frame.
            yield return null;
            m_Animator.SetBool("attaking", false);
            yield return new WaitForSeconds(0.3f);
            m_currentState = PlayerState.idle;
        }
    }
    void Attack()
    {
        if (m_Sword != null && m_PlayerInventory.HasItem(m_Sword))
        {
            if ((m_currentState == PlayerState.walk || m_currentState == PlayerState.idle) && m_currentState != PlayerState.stagger && m_currentState != PlayerState.receiving)
                StartCoroutine(Attack_Coroutine());
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetMovmentControllers();

        //idle,
        //walk,
        //attack,
        //interact,
        //stagger

        // Attack criado em Edit->Project Settings->Input Manager. Valor space
        // m_currentState != PlayerState.attack. Evita múltiplas chamadas

        // (walk=on OR idle=on) AND (stagger=off)
        if (Input.GetButtonDown("Attack"))
            Attack();
        else
        {
            // (walk=on OR idle=on OR interact=on)
            // Quando estiver atacando não se movimenta
            if (m_currentState == PlayerState.walk || m_currentState == PlayerState.idle || m_currentState == PlayerState.interact)
            {
                // (stagger=off)
                if (m_currentState != PlayerState.stagger)
                    UpdateAnimationAndMove();
            }
            else
            {
                // (stagger = off)
                if (m_currentState != PlayerState.stagger)
                    m_Rigidbody2D.velocity = Vector3.zero;
            }
        }
    }

    private void UpdateAnimationAndMove()
    {
        if (m_PositionChange != Vector3.zero)
        {
            MoveCharacter();

            m_Animator.SetFloat("moveX", m_PositionChange.x);
            m_Animator.SetFloat("moveY", m_PositionChange.y);
            m_Animator.SetBool("moving", true);
            if (m_currentState != PlayerState.interact)
                m_currentState = PlayerState.walk;
        }
        else
        {
            m_Animator.SetBool("moving", false);
            if (m_currentState != PlayerState.interact)
                m_currentState = PlayerState.idle;
        }
    }
    private void MoveCharacter()
    {
        // Ajusta o andar nas diagonais que está 2x mais rápido. 
        m_PositionChange.Normalize();

        // mChange deve ser 3D e não 2D para somar com transform.position que é 3D
        // transform.position = Posição do Sprite
        // Time.deltaTime: Tempo transcorrido entre os UPDATES
        // m_Speed: Velocidade

        m_Rigidbody2D.MovePosition(transform.position + m_PositionChange * m_Speed * Time.deltaTime);
        // m_Rigidbody2D.MovePosition(transform.position + m_PositionChange * m_Speed * 0.005f);
    }
    public void Knock(float knockTime, float damage)
    {
        // Evita entrar em estado de stagger 2 vezes
        if (m_currentState != PlayerState.stagger)
        {
            m_currentState = PlayerState.stagger;
            // Evita Null exception se o objeto não for associado
            if (m_currentHealth != null)
            {
                m_currentHealth.m_RuntimeValue -= damage;
                if (m_PlayerHealthSignalList != null)
                    // RAISE
                    // Signal:          Signal_Health
                    // Local Signal:    ScriptableObjects/Player/Health
                    // Capturado por:   Player
                    // Método:          Canvas/HealthHolder/HearthManager->UpdateHearts()
                    m_PlayerHealthSignalList.Raise();

                if (m_currentHealth.m_RuntimeValue > 0)
                    StartCoroutine(Knock_Coroutine(knockTime));
                else
                {
                    // Some com o Player
                    this.gameObject.SetActive(false);
                    m_currentState = PlayerState.die;
                }
            }
        }
    }
    private IEnumerator Knock_Coroutine(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        if (m_Rigidbody2D != null)
            m_Rigidbody2D.velocity = Vector3.zero;

        // Volta para idle   
        m_currentState = PlayerState.idle;
    }
}
