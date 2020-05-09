using UnityEngine;

public class NodeUI : MonoBehaviour
{
	private Node targetNode;
	public GameObject ui;

	public void SetTargetNode(Node node)
	{
		targetNode = node;

		transform.position = node.GetBuildPosition();

		ui.SetActive(true);
	}

	public void Hide()
	{
		ui.SetActive(false);
	}
}
