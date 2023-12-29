using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour, IWeapon
{
    GameObject weaponObject;
    [SerializeField]
    GameObject rayVisual;

    VisualEffect _effect;
    [SerializeField]
    Transform gunBarrel;

    private string secondPos = "pos2";
    private string thirdPos = "pos3";
    private string targetPos = "pos4";

    private void Start()
    {
        //weaponObject.SetActive(true);

        _effect = rayVisual.GetComponent<VisualEffect>();
        _effect.enabled = false;
    }

    public void ActivateRay(Vector3 target)
    {
        UpdateRay(target);
        _effect.enabled = true;
    }

    public void UpdateRay(Vector3 target)
    {
        _effect.SetVector3("pos1", gunBarrel.position);
        _effect.SetVector3(targetPos, target);
        Vector3 secondPosition = Vector3.Lerp(gunBarrel.position, target, 0.33f);
        _effect.SetVector3(secondPos, secondPosition);
        Vector3 thirdPosition = Vector3.Lerp(gunBarrel.position, target, 0.66f);
        _effect.SetVector3(thirdPos, thirdPosition);
    }

    public void DeactivateRay()
    {
        _effect.enabled = false;
    }
}
