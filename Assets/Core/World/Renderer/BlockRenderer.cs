namespace World.Renderer {
    public class BlockRenderer : ARenderer {
        public BlockRenderer(SectionRenderer renderer) : base(renderer) {}

        public override void RenderWestSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(1, 0, 0))) {
                Visible = true;
                Renderer.GenFace(X + 1, Y, Z,
                    0, 1, 0,
                    0, 0, 1,
                    2, Block, 0);
                LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed);
            }
        }

        public override void RenderEastSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(-1, 0, 0))) {
                Visible = true;
                Renderer.GenFace(X, Y, Z,
                    0, 0, 1,
                    0, 1, 0,
                    0, Block, 1);
                LightingHelper.SetLightEast(WorldX - 1, WorldY, WorldZ, Block.Biomed);
            }
        }

        public override void RenderUpSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, 1, 0))) {
                Visible = true;
                Renderer.GenFace(X, Y + 1, Z,
                    0, 0, 1,
                    1, 0, 0,
                    (int) Block.RenderParam, Block, 3);
                LightingHelper.SetLightUp(WorldX, WorldY + 1, WorldZ, Block.Biomed);
            }
        }

        public override void RenderDownSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, -1, 0))) {
                Visible = true;
                int rot;
                switch ((int) Block.RenderParam % 4) {
                    case 2: rot = 1;
                        break;
                    case 3: rot = 0;
                        break;
                    case 1: rot = 2;
                        break;
                    default: rot = 3;
                        break;
                }
                Renderer.GenFace(X, Y, Z,
                    1, 0, 0,
                    0, 0, 1,
                    rot, Block, 2);
                LightingHelper.SetLightDown(WorldX, WorldY - 1, WorldZ, Block.Biomed);
            }
        }

        public override void RenderSouthSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, 0, 1))) {
                Visible = true;
                Renderer.GenFace(X, Y, Z + 1,
                    1, 0, 0,
                    0, 1, 0,
                    0, Block, 4);
                LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed);
            }
        }

        public override void RenderNorthSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, 0, -1))) {
                Visible = true;
                Renderer.GenFace(X, Y, Z,
                    0, 1, 0,
                    1, 0, 0,
                    2, Block, 5);
                LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed);
            }
        }
    }
}