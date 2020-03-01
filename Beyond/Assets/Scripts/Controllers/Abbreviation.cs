using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abbreviation : MonoBehaviour {

	public static void setAbbreviation(int a, Text t) {
        if (a >= 1000 && a <= 999999) //Add mine abbreviations
            t.text = (a / 1000f).ToString("#.00") + "k";
        else if (a > 999999 && a <= 999999999)
            t.text = (a / 1000000f).ToString("#.00") + "M";
        else if (a > 999999999 && System.Convert.ToInt64(a) <= 999999999999)
            t.text = (a / 1000000000f).ToString("#.00") + "B";
        else if (System.Convert.ToInt64(a) > 999999999999)
            t.text = (System.Convert.ToInt64(a) / 1000000000f).ToString("#.00") + "Q";
        else 
            t.text = a.ToString();
    }

	public static void setLongAbbreviation(long a, Text t) {
		if (a >= 1000 && a <= 999999) //Add mine abbreviations
            t.text = (a / 1000f).ToString("#.00") + "k";
        else if (a > 999999 && a <= 999999999)
            t.text = (a / 1000000f).ToString("#.00") + "M";
        else if (a > 999999999 && System.Convert.ToInt64(a) <= 999999999999)
            t.text = (a / 1000000000f).ToString("#.00") + "B";
        else if (System.Convert.ToInt64(a) > 999999999999)
            t.text = (System.Convert.ToInt64(a) / 1000000000f).ToString("#.00") + "Q";
        else 
            t.text = Mathf.RoundToInt(a).ToString();
	}
}
