using UnityEngine;

public class MainCharacterController : MonoBehaviour {
    public Vector3 MoveVector;
    public float moveSpeed;
    public JoystickController Joystick;
    private Transform cameraTransform;
    
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        // get the original input direction
        MoveVector = Input();

        // convert the input depends on the camera view
        MoveVector = RotateWithView();
        
        if (MoveVector != Vector3.zero)
        {
            // change character looking direciton with joystick input
            transform.rotation = Quaternion.LookRotation(MoveVector);

            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private Vector3 Input()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Joystick.Horizontal();
        direction.z = Joystick.Vertical();

        if(direction.magnitude > 1)
            direction.Normalize();

        return direction;
    }

    private Vector3 RotateWithView()
    {
        if(cameraTransform != null)
        {
            Vector3 dir = cameraTransform.TransformDirection(MoveVector);
            dir.Set(dir.x/* * -1*/, 0, dir.z/* * -1*/);
            return dir.normalized * MoveVector.magnitude;
        }
        else
        {
            cameraTransform = Camera.main.transform;
            return MoveVector;
        }
    }
}
