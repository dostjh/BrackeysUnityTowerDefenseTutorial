using UnityEngine;

// TODO: Follow forum for robust camera controls http://bit.ly/2b3SHDY
// TODO: Clamp panning (WASD) controls

public class CameraController : MonoBehaviour
{
	bool doMovement = false;

	public float panSpeed = 30f;
	public float panBoarderThickness = 10f;

	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			doMovement = !doMovement;
		}

		if (!doMovement)
		{
			return;
		}

		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBoarderThickness)
		{
			transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panBoarderThickness)
		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panBoarderThickness)
		{
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBoarderThickness)
		{
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}

		// Zoom
		var scroll = Input.GetAxis("Mouse ScrollWheel");
		var position = transform.position;
		position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
		position.y = Mathf.Clamp(position.y, minY, maxY);

		transform.position = position;
	}
}
