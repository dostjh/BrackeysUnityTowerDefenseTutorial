using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color insufficientFundsColor;
	public Color hoverColor;

	[Tooltip("Y-axis offset so object placement isn't embedded in node")]
	public Vector3 positionOffset;

	[Header("Optional")]
	public GameObject turret;

	Renderer rend;
	Color startColor;

	BuildManager buildManager;

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;

		buildManager = BuildManager.instance;
	}

	void OnMouseDown()
	{
		// Don't allow animation or clicking when hovering over an existing UI element.
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (!buildManager.CanBuild)
		{
			return;
		}

		if (turret != null)
		{
			// TODO Display on screen
			Debug.Log("Node occupied! Cannot place object");
			return;
		}

		buildManager.BuildTurretOn(this);
	}

	// Hover animation
	void OnMouseEnter()
	{
		// Don't allow animation or clicking when hovering over an existing UI element.
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (!buildManager.CanBuild)
		{
			return;
		}

		if (buildManager.CanAfford)
		{
			rend.material.color = hoverColor;
		}
		else
		{
			rend.material.color = insufficientFundsColor;
		}
	}

	// Undo hover animation
	void OnMouseExit()
	{
		rend.material.color = startColor;
	}
}
