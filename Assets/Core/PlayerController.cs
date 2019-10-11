using UnityEngine;
using System.Collections;
using System.IO;
using Network.Packet.Clientbound.Play;
using Network.Packet.Serverbound.Play;
using Clientbound = Network.Packet.Clientbound;
using Serverbound = Network.Packet.Serverbound;
using PosAndLookFlags = Network.Packet.Clientbound.Play.PlayerPositionAndLookPacket.EnumFlags;
using Network.Packet;
using Network;
using World;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ServerConnection))]
public class PlayerController : MonoBehaviour {
	public WorldManager CurrentWorld;
	public float WalkSpeed = 4.317f;
	public float SprintingSpeed = 5.612f;
	public float JumpPower = 9.6f;

    public bool OnGround;
    public float Health = 20;
    public int Food = 20;
    public float FoodSaturation = 5;

    private Rigidbody _rigidbody;
	private MouseLook _mouseLook;
	private Vector3 _prevVelocity;
	private bool _sprinting;
	private bool _forward;
	private float _jumpCD = 0;
	private float _standardFov;
	private Vector3 _prevSentPos = new Vector3();
	private Vector2 _prevSentRot = new Vector2();
	private bool _prevSentOnGround = false;
	private bool _jump;
	private Vector3 _movement;
	private bool[] _mouseClicked = new bool[3];
	private ServerConnection _connection;
	
	IEnumerator Start () {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => {
            SceneManager.SetActiveScene(scene);
        };
        
		_rigidbody = transform.parent.gameObject.GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(0, 0.9f, 0);
        _mouseLook = Camera.main.transform.GetComponent<MouseLook>();
		_connection = gameObject.GetComponent<ServerConnection>();
		_prevVelocity = _rigidbody.velocity;
		_standardFov = Camera.main.fieldOfView;
        _connection.PacketHandler.PlayPacketReceivedEvent += OnChatMessagePacket;
        _connection.PacketHandler.PlayPacketReceivedEvent += OnChunkDataPacket;
		_connection.PacketHandler.PlayPacketReceivedEvent += OnPlayerPositionAndLookPacket;
		_connection.PacketHandler.PlayPacketReceivedEvent += OnUnloadChunkPacket;
		_connection.PacketHandler.PlayPacketReceivedEvent += OnBlockChangePacket;
        _connection.PacketHandler.PlayPacketReceivedEvent += OnUpdateHealthPacket;

        while (true) {
			int cx = (int) transform.position.x >> 4;
			int cy = (int) transform.position.y >> 4;
			int cz = (int) transform.position.z >> 4;
			CurrentWorld.TryRendFromQueue(cx, cy, cz);

            yield return new WaitForSeconds(0.1f);
            yield return new PriorityYieldInstruction(PriorityYieldInstruction.Priority.Low);
		}
	}

	void Update () {
        if (SceneManager.GetActiveScene() != gameObject.scene) {
            return;
        }

        _jumpCD -= Time.deltaTime;
		if (Input.GetMouseButtonUp(0)) {
			_mouseClicked[0] = false;
		}
		if (Input.GetMouseButtonUp(1)) {
			_mouseClicked[1] = false;
		}
		if (Input.GetMouseButtonUp(2)) {
			_mouseClicked[2] = false;
		}
		if (!_mouseClicked[0]) {
            _mouseClicked[0] = Input.GetMouseButtonDown(0);
		}
		if (!_mouseClicked[1]) {
            _mouseClicked[1] = Input.GetMouseButtonDown(1);
		}
		if (!_mouseClicked[2]) {
            _mouseClicked[2] = Input.GetMouseButtonDown(2);
		}

		if (Input.GetButtonUp("Jump")) {
			_jump = false;
		}
		if (!_jump) {
			_jump = Input.GetButtonDown("Jump");
		}

		float moveHorizontal = Input.GetAxisRaw("Horizontal");
      	float moveVertical = Input.GetAxisRaw("Vertical");
      	_movement = transform.forward * moveVertical + transform.right * moveHorizontal;
		_movement.Normalize();

		_forward = moveVertical > 0;

		if (Input.GetButtonUp("Sprint")) {
			_sprinting = false;
		}
		if (!_sprinting) {
			_sprinting = Input.GetButtonDown("Sprint");
		}
	}

	void FixedUpdate() {
        if (SceneManager.GetActiveScene().name != "GameScene") {
            return;
        }

        float moveSpeed;
		if (_sprinting && _forward) {
			moveSpeed = SprintingSpeed;
			if (Camera.main.fieldOfView < _standardFov + 10) {
				Camera.main.fieldOfView += 2;
			}
		} else {
			moveSpeed = WalkSpeed;
			if (Camera.main.fieldOfView > _standardFov) {
				Camera.main.fieldOfView -= 2;
			}
		}
		
		_rigidbody.velocity = _movement * moveSpeed + new Vector3(0, _rigidbody.velocity.y);
		if (_jump && _jumpCD <= 0 && OnGround) {
			_rigidbody.AddForce(0, JumpPower, 0, ForceMode.Impulse);
			_jumpCD = 0.4f;
		}

        bool flag2 = Vector3.Distance(transform.position, _prevSentPos) > 0.03;
		bool flag3 = _mouseLook.Rotation.x != _prevSentRot.x || _mouseLook.Rotation.y != _prevSentRot.y;
		if (flag2 && flag3) {
			new Serverbound.Play.PlayerPositionAndLookPacket(transform.position,
				_mouseLook.Rotation, OnGround).Send(_connection);
		} else if (flag2) {
			new PlayerPositionPacket(transform.position, OnGround).Send(_connection);
		} else if (flag3) {
			new PlayerLookPacket(_mouseLook.Rotation, OnGround).Send(_connection);
		} else if (_prevSentOnGround != OnGround) {
			new PlayerPacket(OnGround).Send(_connection);
		}
		if (flag2) {
			_prevSentPos = transform.position;
		}
		if (flag3) {
			_prevSentRot = _mouseLook.Rotation;
		}
		_prevSentOnGround = OnGround;

		_prevVelocity = _rigidbody.velocity;
		/*
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 10)) {
			ChunkManager chunk = hit.collider.gameObject.GetComponent<ChunkManager>();
			if (chunk != null) {
				Vector3 hitPoint = hit.point.Fix();
				Vector3 pos = hitPoint.Floor();

				int cx = (int) pos.x >> 4;
				int cz = (int) pos.z >> 4;

				Vector3 placePos = pos;
				Vector3 breakPos = pos;
				bool solid = false;// chunk.worldManager.IsSolid(pos.ToInt());
				if (solid) {
					if (Mathf.Abs(hitPoint.y % 1f) < 0.001) {
						placePos -= new Vector3(0, 1, 0);
					} else if (Mathf.Abs(hitPoint.x % 1f) < 0.001) {
						placePos -= new Vector3(1, 0, 0);
					} else if (Mathf.Abs(hitPoint.z % 1f) < 0.001) {
						placePos -= new Vector3(0, 0, 1);
					} 
				} else {
					if (Mathf.Abs(hitPoint.y % 1f) < 0.001) {
						breakPos -= new Vector3(0, 1, 0);
					} else if (Mathf.Abs(hitPoint.x % 1f) < 0.001) {
						breakPos -= new Vector3(1, 0, 0);
					} else if (Mathf.Abs(hitPoint.z % 1f) < 0.001) {
						breakPos -= new Vector3(0, 0, 1);
					} 
				}
				if (mouseClicked[1]) {
					mouseClicked[1] = false;
					SetBlock(chunk.worldManager, placePos, 4);
				}
				if (mouseClicked[0]) {
					mouseClicked[0] = false;
					SetBlock(chunk.worldManager, breakPos, 0);
				}
			}
		}
		*/
	}

	public void SetBlock(WorldManager world, Vector3 pos, int block) {
		int cx = (int) pos.x >> 4;
		int cz = (int) pos.z >> 4;

		//ChunkManager chunk = world.GetChunk(cx, cz);

		int bx = (int) pos.x & 0x000F;
		int bz = (int) pos.z & 0x000F;
		int by = (int) pos.y & 0x000F;

		//chunk.sections[by].Data[bx, by, bz] = block;
		//chunk.GenMesh();
	}

    public void OnUpdateHealthPacket(IPacket packet) {
        var uhp = packet as UpdateHealthPacket;
        if (uhp != null) {
            if (Health > 0 && uhp.Health <= 0)
            {
                SceneManager.LoadSceneAsync("DeathScene", LoadSceneMode.Additive);
            }
            Health = uhp.Health;
            Food = uhp.Food;
            FoodSaturation = uhp.FoodSaturation;
            
        }
    }

    public void OnBlockChangePacket(IPacket packet) {
		var bcp = packet as BlockChangePacket;
		if (bcp != null) {
			int x = bcp.Location.x;
			int y = bcp.Location.y;
			int z = bcp.Location.z;
			if (y < 0 || y >= ChunkManager.H) {
				return;
			}
			int cx = x >> 4;
			int cz = z >> 4;
			
			ChunkManager chunk = CurrentWorld.GetChunk(cx, cz, false);
			if ((object) chunk == null) {
				return;
			}

			int cy = y >> 4;
			ChunkSection section = chunk.Sections[cy];

			if ((object) section == null) {
				return;
			}

			int bx = x & 0x000F;
			int by = y & 0x000F;
			int bz = z & 0x000F;

			section.DecodeLongs();			
			section.Data[bx, by, bz] = (short) bcp.BlockId;
			section.Rend();
		}
	}

	public void OnChunkDataPacket(IPacket packet) {
		var cdp = packet as ChunkDataPacket;
		if (cdp != null) {
			using (MemoryStream ms = new MemoryStream(cdp.Data)) {
				ChunkManager chunk = CurrentWorld.GetChunk(cdp.ChunkX, cdp.ChunkZ);
				chunk.FillChunk(ms, cdp.PrimaryBitMask, cdp.GroundUpContinuous);
			}
		}
	}

	public void OnUnloadChunkPacket(IPacket packet) {
		var ucp = packet as UnloadChunkPacket;
		if (ucp != null) {
			CurrentWorld.UnloadChunk(ucp.ChunkX, ucp.ChunkZ);
		}
	}

    public void OnChatMessagePacket(IPacket packet) {
        var cmp = packet as Clientbound.Play.ChatMessagePacket;
        if (cmp != null) {
            Debug.Log(cmp.GetSimpleText());
        }
    }

    public void OnPlayerPositionAndLookPacket(IPacket packet) {
		var ppalp = packet as Clientbound.Play.PlayerPositionAndLookPacket;
		if (ppalp != null) {
			Vector3 newPos = _rigidbody.position;
			Vector3 newVelocity = _rigidbody.velocity;
			if (ppalp.Flags.HasFlag(PosAndLookFlags.X)) {
				newPos.x += (float) ppalp.X;
			} else {
				newVelocity.x = 0;
				newPos.x = (float) ppalp.X;
			}

			if (ppalp.Flags.HasFlag(PosAndLookFlags.Y)) {
				newPos.y += (float) ppalp.Y;
			} else {
				newVelocity.y = 0;
				newPos.y = (float) ppalp.Y;
			}

			if (ppalp.Flags.HasFlag(PosAndLookFlags.Z)) {
				newPos.z += (float) ppalp.Z;
			} else {
				newVelocity.z = 0;
				newPos.z = (float) ppalp.Z;
			}
			_rigidbody.position = newPos;
			_rigidbody.velocity = newVelocity;

			SetRotation(ppalp.Yaw, ppalp.Pitch);

			new TeleportConfirmPacket(ppalp.TeleportId).Send(_connection);
			new Serverbound.Play.PlayerPositionAndLookPacket(transform.position,
				_mouseLook.Rotation, false).Send(_connection);
		}
	}

	public void SetRotation(float yaw, float pitch) {	
		_mouseLook.Rotation = new Vector2(yaw, pitch);
	}
}