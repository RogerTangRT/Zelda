using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    #region Constants
    /// <summary>
    /// Delay entre iniciar a animação e sumir o objeto da cena
    /// </summary>
    const float ANIMATION_DELAY = 0.5f;
    #endregion

    #region Coroutine
    /// <summary>
    /// Coroutine.
    /// Espera ANIMATION_DELAY segundos para que a animação ocorra
    /// </summary>
    /// <returns></returns>
    IEnumerator SmashPot_Coroutine()
    {
        // Espera 0.5 segundos antes de sumir com o objeto. 
        // O tempo da animação é de 0.15s
        yield return new WaitForSeconds(ANIMATION_DELAY);
        // Remove objeto da cena
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Public Entry Points
    /// <summary>
    /// Smash. Esmaga o pot. Inicia a animação para esmagar o objeto.
    /// </summary>
    public void Smash()
    {
        Animator Animator = GetComponent<Animator>();
        Animator.SetBool("smash", true);
        StartCoroutine(SmashPot_Coroutine());
    }
    #endregion
}
