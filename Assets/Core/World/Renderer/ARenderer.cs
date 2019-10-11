using World;

namespace World.Renderer {
    public abstract class ARenderer {
        public int X {
            get;
            private set;
        }
        public int Y {
            get;
            private set;
        }
        public int Z {
            get;
            private set;
        }
        public int WorldX {
            get;
            private set;
        }
        public int WorldY {
            get;
            private set;
        }
        public int WorldZ {
            get;
            private set;
        }
        public readonly SectionRenderer Renderer;
        public readonly LightingHelper LightingHelper;
        public int BlockId;
        public Block Block;
        public bool Visible = false;

        public ARenderer(SectionRenderer renderer) {
            Renderer = renderer;
            LightingHelper = renderer.LightingHelper;
        }

        public void SetPosition(int x, int y, int z) {
            X = x;
            Y = y;
            Z = z;

            WorldX = Renderer.Section.Chunk.WorldPos.x + x;
            WorldY = Renderer.Section.WorldY + y;
            WorldZ = Renderer.Section.Chunk.WorldPos.z + z;
        }

        public int GetNeighbor(int modX, int modY, int modZ) {
            return Renderer.Section.Chunk.World.GetBlock(WorldX + modX,
                WorldY + modY, WorldZ + modZ);
        }

        public virtual void Initialize() {
        }

        public virtual void RenderWestSide() {
        }

        public virtual void RenderEastSide() {
        }

        public virtual void RenderUpSide() {
        }

        public virtual void RenderDownSide() {
        }

        public virtual void RenderSouthSide() {
        }

        public virtual void RenderNorthSide() {
        }
    }
}