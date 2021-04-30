using Photon.Pun;
using UnityEngine;
 
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] public Transform m_muzzle;
    [SerializeField] private int m_weaponDamage = 35;
    private PlayerHandler m_playerHandler;
    
    private PhotonView m_photonView;
 
    void Update()
    {
        if (m_photonView == null)
            m_photonView = GetComponent<PhotonView>();
 
        if (!m_photonView.IsMine) return;
 
        if (m_playerHandler == null)
            m_playerHandler = GetComponent<PlayerHandler>();
 
        if (!m_playerHandler.IsAlive) return;
 
        if (Input.GetMouseButtonDown(0))
        {
            m_photonView.RPC("Shoot", RpcTarget.All);
        }
    }
 
    [PunRPC]
    private void Shoot()
    {
        // Effects ?
        // Sound
        // Muzzle flash
        // inside the if statement below, you could add some hit effect, e.g. bloodsplatter, sparks, dust depending on whatever it might have hit. 
 
        Ray ray = new Ray(m_muzzle.position, m_muzzle.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            var victim = hit.transform.GetComponentInParent<IAttackable>();
 
            if (victim == null) return;
 
            victim.OnAttack(gameObject, m_weaponDamage);
        }
    }
}