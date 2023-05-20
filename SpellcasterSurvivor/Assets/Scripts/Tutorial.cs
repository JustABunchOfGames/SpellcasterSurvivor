using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Text _tutorialText;

    private int _steps = 1;

    [Header("MoveText - Step1")]
    [TextArea]
    [SerializeField] private string _moveText;
    private bool _hasMoved;
    private bool _hasJumped;

    [Header("Attack1 - Step2")]
    [TextArea]
    [SerializeField] private string _attack1Text;

    [Header("Attack2 - Step3")]
    [TextArea]
    [SerializeField] private string _attack2Text;

    [Header("Attack3 - Step4")]
    [TextArea]
    [SerializeField] private string _attack3Text;

    [Header("Attack4 - Step5")]
    [TextArea]
    [SerializeField] private string _attack4Text;

    [Header("GoalText - Step6")]
    [TextArea]
    [SerializeField] private string _goalText;


    private void Update()
    {
        switch (_steps)
        {
            case 1:
                Step1();
                break;
            case 2:
                Step2();
                break;
            case 3:
                Step3();
                break;
            case 4:
                Step4();
                break;
            case 5:
                Step5();
                break;
            case 6:
                Step6();
                break;
            default:
                break;
        }
    }

    private void Step1()
    {
        _tutorialText.text = _moveText;

        if (Input.GetButtonDown("Jump"))
            _hasJumped = true;
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            _hasMoved = true;

        if (_hasMoved && _hasJumped)
        {
            _steps = 2;
        }
    }

    private void Step2()
    {
        _tutorialText.text = _attack1Text;

        if (Input.GetButton("Attack1"))
            _steps = 3;
    }

    private void Step3()
    {
        _tutorialText.text = _attack2Text;

        if (Input.GetButton("Attack2"))
            _steps = 4;
    }

    private void Step4()
    {
        _tutorialText.text = _attack3Text;

        if (Input.GetButton("Attack3"))
            _steps = 5;
    }

    private void Step5()
    {
        _tutorialText.text = _attack4Text;

        if (Input.GetButton("Attack4"))
            _steps = 6;
    }

    private void Step6()
    {
        _tutorialText.text = _goalText;
    }
}
