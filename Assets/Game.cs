using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	Vector2[] calibrationPositions;
	private Diana d;
	private int currentcalibrationIndex = 0;
	float t;
	// Use this for initialization
	void Start () {
		Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,-20));
		calibrationPositions = new Vector2[] {
			new Vector2(stageDimensions.x / -2, stageDimensions.y / -2),
			new Vector2(stageDimensions.x / -2, stageDimensions.y / 2),
			new Vector2(stageDimensions.x / 2, stageDimensions.y / -2),
			new Vector2(stageDimensions.x / 2, stageDimensions.y / 2)
		};
		d = (Diana)(GameObject.Find("Diana").GetComponent("Diana"));
	}
	
	// Update is called once per frame
	void Update () {
		t = Time.time;
	}


	public void StartCalibration() {
		UiContext.Hub.Connect<ShotPosition>(UiContext.MessageId.Shot, NextcalibrationStep);
		Debug.Log("calibration started");
		currentcalibrationIndex = 0;
		d.Reposition(calibrationPositions[0].x, calibrationPositions[0].y);
	}

	private void NextcalibrationStep(ShotPosition pos) {
		Debug.Log(t.ToString() +  " - STEP " + currentcalibrationIndex.ToString());

		if (++currentcalibrationIndex < calibrationPositions.Length) {
			d.Reposition(calibrationPositions[currentcalibrationIndex].x, calibrationPositions[currentcalibrationIndex].y);
		} else {
			UiContext.Hub.Disconnect<ShotPosition>(UiContext.MessageId.Shot, NextcalibrationStep);
		}
	}
}
