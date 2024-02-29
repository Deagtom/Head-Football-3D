using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Text _countdown;
    private bool _isTimerStart = false;
    public static int Timer = 4;

    private void Start() => StartCoroutine(TimerCountdown());

    private void Update()
    {
        _countdown.text = Timer.ToString();
        if (Timer <= 0)
        {
            PlayerController.IsPause = false;
            _countdown.enabled = false;
            _isTimerStart = true;
        }
        else
        {
            PlayerController.IsPause = true;
            _countdown.enabled = true;
            if (_isTimerStart)
            {
                StartCoroutine(TimerCountdown());
                _isTimerStart = false;
            }
        }
    }

    private IEnumerator TimerCountdown()
    {
        while (Timer > 0)
        {
            Timer--;
            yield return new WaitForSeconds(1);
        }
    }
}