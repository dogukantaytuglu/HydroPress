using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ad : MonoBehaviour
{
    [SerializeField] 
    protected Button _showAdButton;
    [SerializeField] 
    protected string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] 
    protected string _iOsAdUnitId = "Rewarded_iOS";
    protected string _adUnitId;
}
