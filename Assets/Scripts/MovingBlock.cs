using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Left,
    Right,
    Up,
    Down
}

public class MovingBlock : MonoBehaviour {

    public Direction direction = Direction.Left;

    public float distance = 0f;

    public float speed = 0f;

    public float startDelay = 0f;

    public float delay = 0f;

    private float sourcePos;
    private float destPos;
    private float timer;

    private bool directionSwitch = true;
    private bool isDelay = false;
    private bool isStartDelay = true;

    private Rigidbody2D rigidbody;

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();

        switch (direction) {
            case Direction.Left:
                sourcePos = transform.position.x;
                destPos = transform.position.x - distance;
                break;
            case Direction.Right:
                sourcePos = transform.position.x;
                destPos = transform.position.x + distance;
                break;
            case Direction.Up:
                sourcePos = transform.position.y;
                destPos = transform.position.y + distance;
                break;
            case Direction.Down:
                sourcePos = transform.position.y;
                destPos = transform.position.y - distance;
                break;
        }
    }

	// Update is called once per frame
	void Update () {

        if (isStartDelay && timer < startDelay) {
            timer += Time.deltaTime;
            return;
        } else if (isStartDelay && timer >= startDelay) {
            timer = 0f;
            isStartDelay = false;
        }

        if (isDelay && timer < delay) {
            timer += Time.deltaTime;
            return;
        } else if(isDelay && timer >= delay) {
            timer = 0f;
            isDelay = false;
        }

		switch (direction) {
            case Direction.Left:
                if(directionSwitch && transform.position.x > destPos) {
                    //transform.Translate(new Vector3(-(speed * Time.deltaTime), 0, 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y));
                    //rigidbody.position = new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y);
                } else if(!directionSwitch && transform.position.x < sourcePos) {
                    //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y));
                    //rigidbody.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
                } else {
                    directionSwitch = !directionSwitch;
                    isDelay = true;
                }
                break;
            case Direction.Right:
                if (directionSwitch && transform.position.x < destPos) {
                    //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y));
                } else if (!directionSwitch && transform.position.x > sourcePos) {
                    //transform.Translate(new Vector3(-(speed * Time.deltaTime), 0, 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x - (speed * Time.deltaTime), transform.position.y));
                } else {
                    directionSwitch = !directionSwitch;
                    isDelay = true;
                }
                break;
            case Direction.Up:
                if (directionSwitch && transform.position.y < destPos) {
                    //transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime)));
                } else if (!directionSwitch && transform.position.y > sourcePos) {
                    //transform.Translate(new Vector3(0, -(speed * Time.deltaTime), 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime)));
                } else {
                    directionSwitch = !directionSwitch;
                    isDelay = true;
                }
                break;
            case Direction.Down:
                if (directionSwitch && transform.position.y > destPos) {
                    //transform.Translate(new Vector3(0, -(speed * Time.deltaTime), 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime)));
                } else if (!directionSwitch && transform.position.y < sourcePos) {
                    //transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
                    rigidbody.MovePosition(new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime)));
                } else {
                    directionSwitch = !directionSwitch;
                    isDelay = true;
                }
                break;
        }
	}
}
