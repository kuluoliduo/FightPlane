using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple script that lets you localize a UIWidget.
/// </summary>

[RequireComponent(typeof(MaskableGraphic))]
public class UILocalize : MonoBehaviour
{
    public string filename = "ZygUILocal"; //本地化文件名
	public int  key;        //id

	string mLanguage;
	bool mStarted = false;

    public void Refresh(int index)
    {
        Text lbl = GetComponent<Text>();
        string val = Localization.mInstance.GetText(filename, index);
        if (lbl != null)
        {
            lbl.text = val;
        }
        key = index;
    }

	/// <summary>
	/// This function is called by the Localization manager via a broadcast SendMessage.
	/// </summary>
	void OnLocalize (Localization loc)
	{
		if (mLanguage != loc.currentLanguage)
        {
            Text lbl = GetComponent<Text>();
            string val = Localization.mInstance.GetText(filename, key);
            if (lbl != null)
            {
                lbl.text = val;
            }
			mLanguage = loc.currentLanguage;
		}
	}

	/// <summary>
	/// Localize the widget on enable, but only if it has been started already.
	/// </summary>
	void OnEnable ()
	{
        if (mStarted && Localization.mInstance != null) OnLocalize(Localization.mInstance);
	}

	/// <summary>
	/// Localize the widget on start.
	/// </summary>
	void Start ()
	{
		mStarted = true;
        if (Localization.mInstance != null) OnLocalize(Localization.mInstance);
	}
}