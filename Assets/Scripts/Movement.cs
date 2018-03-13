void MouseMovement(){
	float spd = 100f;
	float speedInitialX = speedX;
	float speedInitialY = speedY;
	if(Input.GetAxis("MouseRight") != 0){
		Vector3 mouseCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 start = new Vector2(transform.position.x,transform.position.y);
		Vector2 end = new Vector2(mouseCoordinates.x,mouseCoordinates.y);
		float d = Vector2.Distance(start,end);
		

		if(d > 0.1){
			float angle = Mathf.Asin((end.y-start.y)/d);
			if (start.y <= end.y && start.x <= end.x){
				//nothing
			}else if (start.y <= end.y && start.x > end.x){
				angle = Mathf.PI - angle; 
			}else if (start.y > end.y && start.x > end.x){
				angle = Mathf.PI - angle; 
			}else if (start.y > end.y && start.x <= end.x){
				angle = 2*Mathf.PI + angle; 
			}

			speedX = spd*Mathf.Cos(angle);
			speedY = spd*Mathf.Sin(angle);
			float damping = 1/((Mathf.Abs(speedY)/spd)+1); //vertical movement is twice as expensive
			speedX = speedX * damping * Time.deltaTime;
			speedY = speedY * damping * Time.deltaTime;
		}
	}else{
		speedX -=  Mathf.Sign(speedX) * frictionForceX * Time.deltaTime;
		if(Mathf.Sign(speedX) != Mathf.Sign(speedInitialX)){
			speedX = 0;
		}
		speedY -=  Mathf.Sign(speedY) * frictionForceY * Time.deltaTime;
		if(Mathf.Sign(speedY) != Mathf.Sign(speedInitialY)){
			speedY = 0;
		}
	}
	if(speedX!=0){
		Vector3 theScale = transform.localScale;
		theScale.x = Mathf.Sign(speedX);
		transform.localScale = theScale;
	}
	GetComponent<Rigidbody2D>().velocity = new Vector2(speedX,speedY);
}

void KeyboardMovement(){
	float spd = 1f;
	float speedInitialX = speedX;
	float speedInitialY = speedY;
	if(Input.GetAxis("Horizontal") != 0){
		speedX = spd*Mathf.Sign(Input.GetAxis("Horizontal") * Time.deltaTime);
	}else{
		speedX -=  Mathf.Sign(speedX) * frictionForceX * Time.deltaTime;
		if(Mathf.Sign(speedX) != Mathf.Sign(speedInitialX)){
			speedX = 0;
		}
	}
	if(Input.GetAxis("Vertical") != 0){
		speedY = spd*Mathf.Sign(Input.GetAxis("Vertical") * Time.deltaTime);
	}else{
		speedY -=  Mathf.Sign(speedY) * frictionForceY * Time.deltaTime;
		if(Mathf.Sign(speedY) != Mathf.Sign(speedInitialY)){
			speedY = 0;
		}
	}
	Vector3 theScale = transform.localScale;
	if(speedX!=0){
		theScale.x = Mathf.Sign(speedX);
		transform.localScale = theScale;
	}
	if(speedY!=0){
		theScale.y = Mathf.Sign(speedY);
		transform.localScale = theScale;
	}
	GetComponent<Rigidbody2D>().velocity = new Vector2(speedX,speedY);
}
