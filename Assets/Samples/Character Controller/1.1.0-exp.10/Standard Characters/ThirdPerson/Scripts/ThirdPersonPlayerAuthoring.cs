using UnityEngine;
using Unity.Entities;
using Unity.Entities.Content;
using Unity.Transforms;

[DisallowMultipleComponent]
public class ThirdPersonPlayerAuthoring : MonoBehaviour
{
    // Weak ref test
    public WeakObjectReference<Mesh> mesh;
    public WeakObjectReference<Material> material;

    public GameObject ControlledCharacter;
    public GameObject ControlledCamera;

    public class Baker : Baker<ThirdPersonPlayerAuthoring>
    {
        public override void Bake(ThirdPersonPlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new ThirdPersonPlayer
            {
                ControlledCharacter = GetEntity(authoring.ControlledCharacter, TransformUsageFlags.Dynamic),
                ControlledCamera = GetEntity(authoring.ControlledCamera, TransformUsageFlags.Dynamic),
            });
            AddComponent(entity, new MeshComponentData{mesh = authoring.mesh, material = authoring.material});
            AddComponent<ThirdPersonPlayerInputs>(entity);
        }
    }
}

public struct MeshComponentData : IComponentData
{
    public bool startedLoad;
    public WeakObjectReference<Mesh> mesh;
    public WeakObjectReference<Material> material;
}

[WorldSystemFilter(WorldSystemFilterFlags.Default)]
[UpdateInGroup(typeof(PresentationSystemGroup))]

public partial struct RenderFromWeakObjectReferenceSystem : ISystem
{
    public void OnCreate(ref SystemState state) { }
    public void OnDestroy(ref SystemState state) { }
    public void OnUpdate(ref SystemState state)
    {
        foreach (var dec in SystemAPI.Query<RefRW<MeshComponentData>>())
        {
            Debug.Log($"mesh loading status: {dec.ValueRW.mesh.LoadingStatus}");
            Debug.Log($"material loading status: {dec.ValueRW.material.LoadingStatus}");
            
            if (!dec.ValueRW.startedLoad)
            {
                dec.ValueRW.mesh.LoadAsync();
                dec.ValueRW.material.LoadAsync();
                dec.ValueRW.startedLoad = true;
            }
            if (dec.ValueRW.mesh.LoadingStatus == ObjectLoadingStatus.Completed &&
                dec.ValueRW.material.LoadingStatus == ObjectLoadingStatus.Completed)
            {
                //Graphics.DrawMesh(dec.ValueRO.mesh.Result,
                    //transform.ValueRO.Value, dec.ValueRO.material.Result, 0);
            }
        }
    }
}