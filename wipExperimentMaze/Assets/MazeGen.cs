﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGen : MonoBehaviour {

	float[][] angles = new float[][] { new float[] {-45, -89, 45, 89, 30, -50, 50, -15, 20, 30, 30, -10, -25, -20, 20, -15, 30, 60, -50, 10, -15, 20, -45, 30, 40, -20, -60, 50, 35, -15, 20, -45, 30, -20, 30, -20, 10, 45, -70, 20, 15, -60, 20, 30, -20, -15, 35, -60, -40, 20, 20, 25, -25, 30, 30, -45, -90, 10, 10, 10, -15, 60, -50, 20, 20, 20, -15, -15, -15, 80}, new float[] {-30, 20, -50, 30, 20, 15, -50, 20, -60, 20, 30, -20, -50, 30, 60, -25, -25, 80, -60, 30, 30, -15, -25, 40, 10, 10, 10, -35, -15, 15, -20, -20, 15, 40, 15, -60, 25, 25, 30, -90, 25, 45, -70, 20, -60, 20, 20, 15, 10, 10, -45, -45, 10, 20, 15, 30, 40, -60, -20, -15, 10, 10, 20, 15, 10, -30, -20, 15, 15, 15}, new float[] {20}, new float[] {20}, new float[] {20}, new float[] {20} };
	float[][] gateRatios = new float[][] { new float[] {.25f, .5f, .75f, .25f, .5f, .5f, .5f, .5f, .5f, .4f, .3f, 1f, .2f, .35f, .6f, .75f, .8f, .6f, 1f, .9f, .35f, .45f, .2f, .35f, 1f, 1f, .35f, .45f, .1f, .6f, .85f, .2f, .45f, .6f, .5f, .2f, .1f, .8f, .15f, .3f, .6f, .5f, .4f, .6f, .25f, .65f, .45f, .25f, .9f, 1f, 1f, .25f, .15f, .45f, .85f, .75f, .7f, .4f, .5f, .6f, .15f, .85f, .45f, .5f, .4f, .6f, .5f, .8f, .4f, 1f}, new float[] {.5f}, new float[] {.5f}, new float[] {.5f}, new float[] {.5f}, new float[] {.5f} };
	public GameObject gateStart;
	public GameObject gateContPrefabL;
	public GameObject gateContPrefabR;
	public GameObject wallLeftStart;
	public GameObject wallRightStart;
	public GameObject wallLeftEnd;
	public GameObject wallRightEnd;
	public GameObject player;
	public int SceneSelected;

	// Use this for initialization
	void Start () {
		Debug.Log ("Angles Length" + angles[1].Length);
		Debug.Log ("Gate Ratios Length" + gateRatios[0].Length);
		float totalAngle = 0;
		Transform bigWallPrev = gateStart.transform.GetChild (2);
		Transform smallWallPrev = gateStart.transform.GetChild (1);
		//bigWallPrev.localScale = new Vector3 (10f, 1.8f, .4f);
		for (int i = 0; i < angles[1].Length; i++) {
			if (angles [1][i] < 0) {
				//If the turn is left instantiate a left connected prefab
				GameObject gateCont = Instantiate (gateContPrefabL, this.transform, true);
				GameObject wall1 = Instantiate (wallRightStart, this.transform, true);
				wall1.transform.rotation = Quaternion.Euler (0, totalAngle, 0);
				//wall1.transform.localScale = new Vector3 (4 * Mathf.Abs(Mathf.Sin (Mathf.Deg2Rad * angles [i] / 2)), 1.8f, .4f);
				wall1.transform.position = bigWallPrev.GetChild (0).position;
				//rotate the prefab based on the angle given
				totalAngle += angles [1][i];
				gateCont.transform.rotation = Quaternion.Euler (0, totalAngle, 0);
				//change the outside wall
				Transform bigWall = gateCont.transform.GetChild (0).GetChild (1);
				//Get a reference to the connector object, this is the child of one of shorter of the previous walls
				Transform connector = smallWallPrev.transform.GetChild (0);
				gateCont.transform.position = connector.position;
				//create walls (2 out side extended walls)
				GameObject wall2 = Instantiate (wallRightEnd, this.transform, true);
				wall2.transform.rotation = Quaternion.Euler (0, totalAngle, 0);
				wall2.transform.position = bigWall.GetChild (1).position;

				//fix the gates
				gateCont.transform.GetChild(0).GetChild(2).Translate(-gateRatios[0][i] * 3, 0, 0);

				//prepare for next iteration
				if (i != angles[1].Length - 1) {
					if (angles[1][i + 1] < 0) {
						bigWallPrev = bigWall;
						smallWallPrev = gateCont.transform.GetChild (0).GetChild (0);
					} else {
						smallWallPrev = bigWall;
						bigWallPrev = gateCont.transform.GetChild (0).GetChild (0);
					}
				}

			} else {
				//If the turn is left instantiate a left connected prefab
				GameObject gateCont = Instantiate (gateContPrefabR, this.transform, true);
				GameObject wall1 = Instantiate (wallLeftStart, this.transform, true);
				wall1.transform.rotation = Quaternion.Euler (0, totalAngle, 0);
				wall1.transform.position = bigWallPrev.GetChild (0).position;
				//rotate the prefab based on the angle given
				totalAngle += angles [1][i];
				gateCont.transform.rotation = Quaternion.Euler (0, totalAngle, 0);
				//change the outside wall
				Transform bigWall = gateCont.transform.GetChild (0).GetChild (0);
				//Get a reference to the connector object, this is the child of one of shorter of the previous walls
				Transform connector = smallWallPrev.transform.GetChild (0);
				gateCont.transform.position = connector.position;
				//create walls (2 out side extended walls)
				GameObject wall2 = Instantiate (wallLeftEnd, this.transform, true);
				wall2.transform.rotation = Quaternion.Euler (0, totalAngle, 0);
				wall2.transform.position = bigWall.GetChild (1).position;

				//fix the gates
				gateCont.transform.GetChild(0).GetChild(2).Translate(-gateRatios[0][i] * 3, 0, 0);

				//prepare for next iteration
				if (i != angles[1].Length - 1) {
					if (angles [1][i + 1] > 0) {
						bigWallPrev = bigWall;
						smallWallPrev = gateCont.transform.GetChild (0).GetChild (1);
					} else {
						smallWallPrev = bigWall;
						bigWallPrev = gateCont.transform.GetChild (0).GetChild (1);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
