using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [Header("Score Bar")]
    [SerializeField] private Text _scoreLeft;
    [SerializeField] private Text _scoreRight;
    private GameObject _ball;
    private bool _isMovingWithBall = false;

    private void Start()
    {
        _scoreLeft = GameObject.Find("ScoreLeft").GetComponent<Text>();
        _scoreRight = GameObject.Find("ScoreRight").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        Kick();
    }

    private void Re()
    {
        DestroyWithTag("Player");
        DestroyWithTag("Object");
        Destroy(GameObject.FindGameObjectWithTag("Ball"));

        SpawnElements spawnElements = GameObject.FindGameObjectWithTag("Trigger").GetComponent<SpawnElements>();
        spawnElements.SpawnNewPlayers();
        spawnElements.SpawnNewBall();
        spawnElements.SpawnNewObjects();
        Countdown.Timer += 4;
    }

    private void DestroyWithTag(string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }

    private void Kick()
    {
        if (Input.GetMouseButton(0) && _isMovingWithBall)
        {
            _ball.GetComponent<Rigidbody>().isKinematic = false;
            _ball.transform.parent = null;
            _ball.GetComponent<Rigidbody>().AddForce(transform.forward * 25f, ForceMode.Impulse);
            _ball = null;
            _isMovingWithBall = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = collision.transform.GetChild(1);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            _ball = this.gameObject;
            _isMovingWithBall = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RightGates")
        {
            Re();
            _scoreLeft.text = Convert.ToString(int.Parse(_scoreLeft.text) + 1);
        }
        else if (other.gameObject.tag == "LeftGates")
        {
            Re();
            _scoreRight.text = Convert.ToString(int.Parse(_scoreRight.text) + 1);
        }
    }
}