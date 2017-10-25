using UnityEngine;
using System.Collections;
public class Diana : MonoBehaviour {
	const int multiplierX = 10;
	const int multiplierY = 10;

	float posX;
	float posY;
	int shooted = 20;
	Vector2 scale;
	System.Random r = new System.Random();
	// Use this for initialization
	void Start () {
		this.Reposition();
		scale = new Vector2(GetComponent<SpriteRenderer>().bounds.size.x, GetComponent<SpriteRenderer>().bounds.size.y);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(posX, posY, 1);

	}

	public void Shoot(float X, float Y) {
		float diffX = Mathf.Abs(posX - X);
		float diffY = Mathf.Abs(posY - Y);
		if (diffX < scale.x/2 && diffY < scale.y/2) {
			Debug.Log("ACIERTO!!!!");
			UnityMainThreadDispatcher.Instance().Enqueue(DoTheRep());
		}
	}

	private void Reposition () {
		posX = ((float)(r.NextDouble()) - 0.5f) * multiplierX;
		posY = ((float)(r.NextDouble()) - 0.5f) * multiplierY;
	}
	public IEnumerator DoTheRep() {
		yield return new WaitForSeconds(0.5f);
		this.Reposition();
		yield return null;
	}
}
