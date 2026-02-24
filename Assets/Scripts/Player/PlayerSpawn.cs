using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    private PlayerScript player;
    private InputAction respawnAction;
    private bool down = false;
    private Vector3 position;
    public GameObject playerBlueprint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        position = transform.position;
        respawnAction = InputSystem.actions.FindAction("Respawn");
        Spawn();
    }

    public void Spawn()
    {
        if (player == null)
        {
            player = Instantiate(playerBlueprint, position, Quaternion.identity).GetComponent<PlayerScript>();
            player.spawn = this;
        }
    }

    public void Respawn()
    {
        player.setPosition(position);
        player.spawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (down)
        {
            if (respawnAction.ReadValue<float>() == 0.0f)
            {
                down = false;
            }
        }
        else if (respawnAction.ReadValue<float>() != 0.0f || !player.spawned) {
            down = true;
            Respawn();
        }
    }
}
