using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour {

    public UIFader weaponsFader1;
    public UIFader weaponsFader2;
    public ApproachTarget weaponsApproacher;

    public Text[] textFields;
    
    private string weaponName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var weapon = GameController.GetInstance().currentWeapon;
        if (weapon == null && weaponName != null)
        {
            weaponsFader1.reverse = true;
            weaponsFader2.reverse = true;
            weaponsApproacher.reverse = true;
            weaponsFader1.enabled = true;
            weaponsFader2.enabled = true;
            weaponsApproacher.enabled = true;
            weaponName = null;
        }
        else if (weapon != null && weapon.displayName != weaponName)
        {
            weaponName = weapon.displayName;
            foreach (var text in textFields)
            {
                text.text = weaponName.ToUpper();
            }
            weaponsFader1.reverse = false;
            weaponsFader2.reverse = false;
            weaponsApproacher.reverse = false;
            weaponsFader1.enabled = true;
            weaponsFader2.enabled = true;
            weaponsApproacher.enabled = true;
        }
	}
}
