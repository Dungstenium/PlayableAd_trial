using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeArrowComponent : MonoBehaviour
{
	private List<Transform> arrowList = new List<Transform>();
	[SerializeField] private Vector3 direction;
	[SerializeField] private float duration;
	private float speed;

	private void Awake()
	{
		arrowList = gameObject.GetComponentsInChildren<Transform>(true).ToList();
		arrowList.Remove(transform);
	}

	private void OnEnable()
	{
		foreach (var arrow in arrowList)
		{
			StartCoroutine(MoveArrow(duration, arrow));
		}
	}

	private IEnumerator MoveArrow(float duration, Transform arrow)
	{
		arrow.gameObject.SetActive(true);

		float counter = 0;
		var initialPosition = arrow.position;
		
		speed = Random.Range(0.8f, 1.2f);
		
		while (counter < duration)
		{
			counter += Time.deltaTime;

			arrow.position += direction * speed * Time.deltaTime;
			
			yield return null;
		}

		arrow.position = initialPosition;
		arrow.gameObject.SetActive(false);
	}
}
