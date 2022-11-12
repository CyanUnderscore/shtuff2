using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
	public GameObject player;
	public GameObject wallPrefab;
	public GameObject floorPrefab;

	private const int WIDTH = 5;
	private const int HEIGHT = 5;

    // Start is called before the first frame update
    void Start()
    {
		for(int j = 0; j < HEIGHT; j++) {
			for(int i = 0; i < WIDTH; i++) {
				var position = new Vector3(i * 10f, j * 6f, 0f);

				var floor = Instantiate(floorPrefab, position, Quaternion.identity);
				var wall = Instantiate(wallPrefab, position + new Vector3(-4.5f, 3f), Quaternion.identity);

				if(i == 0) { // if on the edge of the map, make walls unbreakable
					wall.GetComponent<Wall>().breakable = false;
				}
				if(i == WIDTH - 1) {
					var right_wall = Instantiate(wallPrefab, position + new Vector3(10f - 4.5f, 3f), Quaternion.identity);
					right_wall.GetComponent<Wall>().breakable = false;
					right_wall.transform.parent = transform;
				}

				if(j == 0) { // if on the bottom, make ground unbreakable
					floor.GetComponent<Wall>().breakable = false;
				}
				if(j == HEIGHT-1) { // if on the top, make unbreakable ceiling
					var ceiling = Instantiate(floorPrefab, position + new Vector3(0f, 6f), Quaternion.identity);
					ceiling.GetComponent<Wall>().breakable = false;
					ceiling.transform.parent = transform;
				}

				floor.transform.parent = transform;
				wall.transform.parent = transform;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
