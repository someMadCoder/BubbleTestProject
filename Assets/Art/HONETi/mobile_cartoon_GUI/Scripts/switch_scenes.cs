using UnityEngine;
using UnityEngine.UI;

namespace Art.HONETi.mobile_cartoon_GUI.Scripts
{
	public class switch_scenes : MonoBehaviour {
	
		public string sceneName = "";

		// Use this for initialization
		void Start () {
			Button b = GetComponent<Button> ();
			if (b != null && sceneName != "")
			{
				b.onClick.AddListener(() => {Application.LoadLevel(sceneName);});
			}
		}
	
		// Update is called once per frame
		void Update () {
	
		}
	}
}
