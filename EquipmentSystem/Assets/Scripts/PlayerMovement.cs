using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundcheck;
    [SerializeField] private float heading = 0;
    [SerializeField] private Transform cam;
    [SerializeField] private CharacterController character;

    private Vector3 _camF;
    private Vector3 _camR;

    private Vector2 _input;

    private Vector3 _intent;
    private Vector3 _velocity;
    private Vector3 _velocityXZ;

    [SerializeField] private float speed = 7;
    [SerializeField] private float accel = 15;
    private float _turnSpeed;
    private float _turnSpeedLow = 5;
    private float _turnSpeedHigh = 20;

    [SerializeField] private float jumpHeight;
    [SerializeField] private int maxjumpcount = 3;
    [SerializeField] private int jumpcount;
    private float _inputTimer;

    [SerializeField] private float grav = 10f;
    [SerializeField] private bool grounded = false;
    [SerializeField] private bool isJumping = false;
    private void Start()
    {
        _inputTimer = 0;
        character = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _inputTimer += Time.deltaTime;
        DoInput();
        CalculateCamera();
        CalculateGround();
        DoMove();
        DoGravity();
        DoJump();

        character.Move(_velocity * Time.deltaTime);

        if (_inputTimer >= 2)
        {
            jumpcount = 0;
        }
    }

    private void DoInput()
    {
        heading += Input.GetAxis("Mouse X") * Time.deltaTime * 180;

        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _input = Vector2.ClampMagnitude(_input, 1);
    }
    private void DoMove()
    {
        _intent = _camF * _input.y + _camR * _input.x;
        float ts = _velocity.magnitude / 5;
        _turnSpeed = Mathf.Lerp(_turnSpeedHigh, _turnSpeedLow, ts);
        if (_input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(_intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }

        _velocityXZ = _velocity;
        _velocityXZ.y = 0;
        _velocityXZ = Vector3.Lerp(_velocityXZ, transform.forward * _input.magnitude * speed, accel * Time.deltaTime);
        _velocity = new Vector3(_velocityXZ.x, _velocity.y, _velocityXZ.z);


    }
    private void CalculateCamera()
    {
        _camF = cam.forward;
        _camR = cam.right;

        _camF.y = 0;
        _camR.y = 0;
        _camF = _camF.normalized;
        _camR = _camR.normalized;
    }
    private void CalculateGround()
    {
        grounded = Physics.Raycast(groundcheck.transform.position, Vector3.down, out _, 0.1f);
    }
    private void DoGravity()
    {
        if (grounded)
        {
            if (!isJumping)
            {
                _velocity.y = -0.5f;
            }
        }
        else
        {
            _velocity.y -= grav * Time.deltaTime;
        }
    }
    private void DoJump()
    {
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _inputTimer = 0;
                jumpcount++;
                StartCoroutine(JumpCooldown());
            }

        }
        if (!isJumping) return;
        switch (jumpcount)
        {
            case 1:
                _velocity.y = jumpHeight;
                break;
            case 2:
                _velocity.y = jumpHeight + 2;
                break;
            default:
            {
                if (jumpcount == maxjumpcount)
                {
                    _velocity.y = jumpHeight + 5;
                    jumpcount = 0;
                }
                break;
            }
        }
    }
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }
}