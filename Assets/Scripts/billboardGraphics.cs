using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardGraphics : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void LateUpdate()
    {
        //the purpose of the billboardGraphics script is to help give the illusion that a 2D sprite is 3D by having it rotate to look at the player, this works especially well for graphics that don't really need
        //a 3D view (like a tree, most trees just kind of sit there as nonfunctional ambience, so as long as they have a point in 3D space, one need not see all sides of it) or sprites that would normally default
        //to looking at the player (maybe an NPC that would normally desire to speak to or move towards the player). However, do be mindful that other sprites will not benefit from billboard graphics, such as a door
        //(doors do not normally rotate to look at a player and are stuck in a wall) or a chair in the middle of a room (one would potentially walk around it and therefore need to see the back of it)
        
        Vector3 pointToLookAt = Camera.main.transform.position;//grab the overall position of the camera, which is a child of the player/character
        pointToLookAt.y = transform.position.y; // avoids leaning by updating the pointToLookAt.y before updating the overall position
        transform.LookAt(pointToLookAt);// tells the sprite to look at the updated pointToLookAt
    }
}



