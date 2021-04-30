using Photon.Pun;
using System.Collections;
using UnityEngine;
 
public class HandleCharacter : MonoBehaviourPun
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
 
    public bool IsAlive = true;
 
    [SerializeField] private GameObject m_model;
    [SerializeField] private Health m_health;
    [SerializeField] private float m_respawnTime = 2.5f;
 
    private Camera m_camera;
 
    public GameObject gunPrefab;
    
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
        m_camera = Instantiate(camera, Vector3.zero, transform.rotation, transform).GetComponent<Camera>();
 
        m_camera.transform.localPosition = new Vector3(0f, 0.6f, 0f);
        GetComponent<PlayerMovement>().playerCamera = m_camera;
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