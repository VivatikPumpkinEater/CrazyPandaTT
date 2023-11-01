using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecelerationZone : MonoBehaviour
{
    private const float SlowdownFactor = 0.1f;
    private const float ZoneRadius = 3f;

    private PhysicsScene2D _physicsScene;
    private Scene _physicalScene;

    private List<Rigidbody2D> _rigidbodiesInPhyScene = new();
    
    private Scene CurrentScene => SceneManager.GetActiveScene();
    private PhysicsScene2D CurrentPhysicalScene => CurrentScene.GetPhysicsScene2D();

    private void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;
        var csp = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        _physicalScene = SceneManager.CreateScene("PhysicalScene", csp);
        _physicsScene = _physicalScene.GetPhysicsScene2D();
    }

    private void Update()
    {
        CurrentPhysicalScene.Simulate(Time.deltaTime);
        _physicsScene.Simulate(Time.deltaTime * SlowdownFactor);

        for (var i = _rigidbodiesInPhyScene.Count - 1; i > 0; i--)
        {
            var rb = _rigidbodiesInPhyScene[i];
            
            if (Vector2.Distance(rb.position, transform.position) < ZoneRadius)
                continue;
            
            MoveRigidbodyToPhyScene(rb, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var rb = other.attachedRigidbody;
        if (!rb || _rigidbodiesInPhyScene.Contains(rb))
            return;
        
        MoveRigidbodyToPhyScene(rb, true);
    }

    private void MoveRigidbodyToPhyScene(Rigidbody2D rb, bool value)
    {
        var scene = value ? _physicalScene : CurrentScene;
        
        var velocity = rb.velocity;
        SceneManager.MoveGameObjectToScene(rb.gameObject, scene);
        rb.velocity = velocity;

        if (value)
            _rigidbodiesInPhyScene.Add(rb);
        else
            _rigidbodiesInPhyScene.Remove(rb);
    }
}