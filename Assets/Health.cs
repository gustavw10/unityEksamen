using Photon.Pun;
using System;
using UnityEngine;
 
public class Health : MonoBehaviourPunCallbacks, IPunObservable, IAttackable
{
    [SerializeField] private int m_currentHealth;
    [SerializeField] private int m_maxHealth = 100;
 
    public event Action OnDeath; 
 
    private void Awake()
    {
        if (!photonView.IsMine) return;
 
        m_currentHealth = m_maxHealth;
    }
 
    public void OnAttack(GameObject attacker, int damage)
    {
        if (attacker == gameObject) return; // can't hit self. 
 
        m_currentHealth = Mathf.Max(m_currentHealth - damage, 0);
 
        if (m_currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
 
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(this.m_currentHealth);
        }
        else
        {
            // Network player, receive data
            this.m_currentHealth = (int)stream.ReceiveNext();
        }
    }
 
    public void ResetHealth()
    {
        m_currentHealth = m_maxHealth;
    }
}
 