using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MovingPlatformAuthoring : MonoBehaviour
{
    public MovingPlatform MovingPlatform;
    
    public class MovingPlatformBaker : Baker<MovingPlatformAuthoring> 
    {
        public override void Bake(MovingPlatformAuthoring authoring) 
        {
            AddComponent(authoring.MovingPlatform);
        }
    }
}
