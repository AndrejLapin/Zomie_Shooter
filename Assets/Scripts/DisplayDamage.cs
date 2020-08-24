using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas damageCanvas;
    [SerializeField] float timeForDamageCanvas = 0.25f;

    void Start()
    {
        damageCanvas.enabled = false;
    }

    public void ShowDamageImpact()
    {
        StartCoroutine(ShowImpact());
    }

    IEnumerator ShowImpact()
    {
        damageCanvas.enabled = true;
        yield return new WaitForSeconds(timeForDamageCanvas);
        damageCanvas.enabled = false;
    }
}
