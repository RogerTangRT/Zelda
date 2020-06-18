using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public BoolValue m_StoredValue;
    public Sprite m_ActiveSprite;
    public Door m_Door;
    private SpriteRenderer m_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if(m_StoredValue.m_RuntimeValue)
            ActivateSwitch();
    }
    public void ActivateSwitch()
    {
        m_StoredValue.m_RuntimeValue = true;
        m_Door.Open();
        m_SpriteRenderer.sprite = m_ActiveSprite;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBody"))
            ActivateSwitch();
    }
}
