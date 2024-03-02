using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("Score Bar")]
    [SerializeField] private Text _scoreLeft;
    [SerializeField] private Text _scoreRight;
    private GameObject _ball;
    private bool _isMovingWithBall = false;
    private string _whoIs;

    [Header("Kick")]
    [SerializeField] private Slider _kickForceSlider;
    private float _kickForceValue = 5f;

    private void Start()
    {
        _scoreLeft = GameObject.Find("ScoreLeft").GetComponent<Text>();
        _scoreRight = GameObject.Find("ScoreRight").GetComponent<Text>();
        _kickForceSlider = GameObject.Find("ForceKick").GetComponent<Slider>();
    }

    private void Update()
    {
        _kickForceSlider.value = _kickForceValue;
        Kick();
    }

    private void FixedUpdate() => PowerKickForce();

    private void PowerKickForce()
    {
        if (_isMovingWithBall && (Input.GetMouseButton(0) || Input.GetMouseButton(1)) && _kickForceValue <= 35f)
            _kickForceValue += 20f * Time.fixedDeltaTime;
    }

    private void Kick()
    {
        if (_isMovingWithBall)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                _ball.GetComponent<Rigidbody>().isKinematic = false;
                _ball.transform.parent = null;
                if (Input.GetMouseButtonUp(0))
                {
                    _ball.GetComponent<Rigidbody>().AddForce(transform.forward * _kickForceValue, ForceMode.Impulse);
                    _ball.GetComponent<Rigidbody>().AddForce(transform.up * (_kickForceValue / 15f), ForceMode.Impulse);
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    _ball.GetComponent<Rigidbody>().AddForce(transform.forward * (_kickForceValue / 1.5f), ForceMode.Impulse);
                    _ball.GetComponent<Rigidbody>().AddForce(transform.up * (_kickForceValue / 4f), ForceMode.Impulse);
                }
                _ball = null;
                _kickForceValue = 5;
                _isMovingWithBall = false;
                StartCoroutine(ClearPlayerName());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.name != _whoIs)
        {
            _whoIs = collision.gameObject.name;

            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.transform.GetChild(1);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            _ball = this.gameObject;
            _isMovingWithBall = true;
        }
    }

    private IEnumerator ClearPlayerName()
    {
        yield return new WaitForSeconds(1);
        _whoIs = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightGates")
        {
            Restart restart = GameObject.FindGameObjectWithTag("Trigger").GetComponent<Restart>();
            restart.Re();
            _scoreLeft.text = Convert.ToString(int.Parse(_scoreLeft.text) + 1);
        }
        else if (other.gameObject.tag == "LeftGates")
        {
            Restart restart = GameObject.FindGameObjectWithTag("Trigger").GetComponent<Restart>();
            restart.Re();
            _scoreRight.text = Convert.ToString(int.Parse(_scoreRight.text) + 1);
        }
    }
}