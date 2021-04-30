using Photon.Pun;
using System.Collections;
using UnityEngine;
 
public class AdvPlayerHandler : MonoBehaviourPun
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
 
    public bool IsAlive = true;
 
    [SerializeField] private GameObject m_model;
    [SerializeField] private Health m_health;
    [SerializeField] private float m_respawnTime = 2.5f;
 
    //private Camera m_camera;
    private Camera m_camera;
 
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
        //  if (!photonView.IsMine) return;
        //  if (this.cameraPrefab != null)
        //   {
        //      if (instanceCam != null) return;

        //  instanceCam = Instantiate(cameraPrefab, Vector3.zero, transform.rotation, transform).GetComponentInChildren<Camera>();
        //  instanceCam.transform.localPosition = new Vector3(0f, 8.5f, 0f);

        //  GetComponent<PlayerMovement>().playerCamera = instanceCam;
        //   }

        GameObject camera = new GameObject();
        camera.AddComponent<Camera>();
        m_camera = Instantiate(camera, Vector3.zero, transform.rotation, transform).GetComponent<Camera>();
 
        m_camera.transform.localPosition = new Vector3(0f, 0.6f, 0f);
        GetComponent<PlayerMovement>().playerCamera = m_camera;


        // Camera c = transform.Find("Camera").gameObject.GetComponent<Camera>();
        // GetComponent<PlayerMovement>().playerCamera = c;

        // GameObject camera = new GameObject();
        // camera.AddComponent<Camera>();
        // m_camera = Instantiate(camera, Vector3.zero, transform.rotation, transform).GetComponent<Camera>();
        // m_camera.transform.localPosition = new Vector3(0f, 0.6f, 0f);
        // GetComponent<PlayerMovement>().playerCamera = m_camera;
        

        // if (this.m_cameraPrefab != null)
        // {
        //     if (m_camera != null) return;

        //     this.m_camera = Instantiate(this.m_cameraPrefab, transform);
        //     //m_camera.GetComponent<WowCamera>().Target = PlayerObject.transform;
        //     GetComponent<PlayerMovement>().playerCamera = m_camera.GetComponentInChildren<Camera>();
        // }
        //m_camera = PlayerObject.GetComponent<Camera>();
        
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
 