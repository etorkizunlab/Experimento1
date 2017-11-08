using UnityEngine;
using System.Collections;
public class Diana : MonoBehaviour {
	const int multiplierX = 10;
	const int multiplierY = 10;

	float posX;
	float posY;
	bool shooted;

	Vector2 scale;
	System.Random r = new System.Random();
	// Use this for initialization
	void Start () {
		this.Reposition();
		scale = new Vector2(GetComponent<SpriteRenderer>().bounds.size.x, GetComponent<SpriteRenderer>().bounds.size.y);
		gameObject.GetComponent<Renderer>().enabled = false;
		//UiContext.Hub.Connect<ShotPosition>(UiContext.MessageId.Shot, ShotAction);
	}

	void ShotAction (ShotPosition position) {
		Shoot(position.x, position.y);
	}
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(posX, posY, 1);
		gameObject.GetComponent<Renderer>().enabled = !shooted;

	}

	public void Shoot(float X, float Y) {
		float diffX = Mathf.Abs(posX - X);
		float diffY = Mathf.Abs(posY - Y);
		if (!shooted && diffX < scale.x/2 && diffY < scale.y/2) {
			Debug.Log("ACIERTO!!!!");
			shooted = true;
			UnityMainThreadDispatcher.Instance().Enqueue(DoTheRep());
		}
	}

	private void Reposition () {
		posX = ((float)(r.NextDouble()) - 0.5f) * multiplierX;
		posY = ((float)(r.NextDouble()) - 0.5f) * multiplierY;
		shooted = false;
	}
	public void Reposition(float x, float y) {
		posX = x;
		posY = y;
		shooted = false;
	}
	public IEnumerator DoTheRep() {
		yield return new WaitForSeconds(0.5f);
		this.Reposition();
		yield return null;
	}
}

public class ShotPosition {
	public float x {
		get;
		set;
	}
	public float y {
		get;
		set;
	}
	public ShotPosition(float x, float y) {
		this.x = x;
		this.y = y;
	}
}
