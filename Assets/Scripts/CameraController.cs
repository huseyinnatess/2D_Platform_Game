using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public Transform[] CameraSinirlari;

    private void Update()
    {
        if (Player != null)
        {
            transform.position = new Vector3
                (Mathf.Clamp(Player.position.x, CameraSinirlari[0].position.x + Camera.main.orthographicSize * Camera.main.aspect, CameraSinirlari[1].position.x - Camera.main.orthographicSize * Camera.main.aspect),
               Mathf.Clamp(Player.position.y, CameraSinirlari[2].position.y + 6, CameraSinirlari[3].position.y - 6),
                transform.position.z);
        }
            
      
    }


}
