using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoPanel : MonoBehaviour {

    public Text[] textFields;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var weapon = GameController.GetInstance().currentWeapon;
        if (weapon == null)
        {
            SetAmmo(0);
        }
        else
        {
            if (weapon.infiniteAmmo)
            {
                SetAmmo(-1);
            }
            else
            {
                SetAmmo((int)weapon.ammo);
            }
        }
    }

    private void SetAmmo(int ammo)
    {
        string ammoText;
        if (ammo == -1)
        {
            ammoText = "--";
        }
        else
        {
            ammoText = ammo.ToString();
        }
        foreach (var text in textFields)
        {
            text.text = ammoText;
        }
    }
}
