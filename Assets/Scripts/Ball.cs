using System;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private Text _score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gates") 
            _score.text = Convert.ToString(int.Parse(_score.text) + 1); 
    }
}