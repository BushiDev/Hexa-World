using UnityEngine;

public class PlayerController : MonoBehaviour{

    CharacterController characterController;
    public float speed = 2f;

    void Start(){

        characterController = GetComponent<CharacterController>();

    }

    void Update(){

        Vector3 move = new Vector3(VirtualJoystick.instance.InputData().x, 0f, VirtualJoystick.instance.InputData().y);
        
        move *= speed;
        move *= Time.deltaTime;
        characterController.Move(move);

    }

}