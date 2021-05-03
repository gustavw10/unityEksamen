using Photon.Pun;
using System.Collections;
using UnityEngine;
 
public class PlayerHandler : MonoBehaviourPun
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
 
    public bool IsAlive = true;
 
    [SerializeField] private GameObject m_model;
    [SerializeField] private Health m_health;
    [SerializeField] private float m_respawnTime = 2.5f;
 
    private GameObject m_camera;

    public GameObject gunPrefab;
    private GameObject m_gun;
 
    private void OnEnable()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
 
            if (m_camera == null)
                CreateCamera();
 
            m_health.OnDeath += PlayerOnDeath;
        }
 
        DontDestroyOnLoad(gameObject);
    }
 
    private void OnDestroy() => m_health.OnDeath -= PlayerOnDeath;
 
    private void CreateCamera()
    {
        GameObject camera = new GameObject();
        camera.AddComponent<Camera>();
        m_camera = Instantiate(camera, Vector3.zero, transform.rotation, transform);

        m_gun = Instantiate(gunPrefab, Vector3.zero, transform.rotation, transform);
        //m_gun.transform.parent = camera.GetComponent<Camera>().gameObject.transform;
        m_gun.transform.SetParent(m_camera.gameObject.transform);
        m_gun.transform.localPosition = new Vector3(0.4f, -0.3f, 0f);

        //camera.transform.SetParent(LocalPlayerInstance.transform);
 
        m_camera.transform.localPosition = new Vector3(0f, 0.6f, 0f);

        GetComponent<PlayerShooting>().m_muzzle = m_gun.transform.Find("Muzzle");
    }
 
    private void PlayerOnDeath()
    {
        if (!photonView.IsMine) return;
 
        IsAlive = false;
 
        photonView.RPC("SetModelActive", RpcTarget.All, false);
 
        StartCoroutine(SpawnDelay());
    }
 
    [PunRPC]
    private void SetModelActive(bool b) => m_model.SetActive(b);
 
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(m_respawnTime);
 
        m_health.ResetHealth();
        
        IsAlive = true;
 
        transform.position = new Vector3(
            UnityEngine.Random.Range(-25, 26),
            5,
            UnityEngine.Random.Range(-25, 26)
        );
        
        photonView.RPC("SetModelActive", RpcTarget.All, true);
    }
}
 