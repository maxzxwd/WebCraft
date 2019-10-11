namespace World.Renderer {
    public class RailRenderer : ARenderer {
        public RailRenderer(SectionRenderer renderer) : base(renderer) {}

        public override void RenderUpSide() {
            Visible = true;
            if (Block.RenderParam > 15) {
                Renderer.GenFace(X, Y, Z,
                    0, 1, 1,
                    1, 0, 0,
                    (int) Block.RenderParam, Block, 0);
            } else if (Block.RenderParam > 11) {
                Renderer.GenFace(X, Y + 1, Z,
                    0, -1, 1,
                    1, 0, 0,
                    (int) Block.RenderParam, Block, 0);
            } else if (Block.RenderParam > 7) {
                Renderer.GenFace(X, Y, Z,
                    0, 0, 1,
                    1, 1, 0,
                    (int) Block.RenderParam, Block, 0);
            } else if (Block.RenderParam > 3) {
                Renderer.GenFace(X, Y + 1, Z,
                    0, 0, 1,
                    1, -1, 0,
                    (int) Block.RenderParam, Block, 0);
            } else {
                Renderer.GenFace(X, Y, Z,
                    0, 0, 1,
                    1, 0, 0,
                    (int) Block.RenderParam, Block, 0);
            }
            LightingHelper.SetLightUp(WorldX, WorldY, WorldZ, Block.Biomed, false);
        }

        public override void RenderDownSide() {
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
            if (Block.RenderParam > 15) {
                Renderer.GenFace(X, Y, Z,
                    1, 0, 0,
                    0, 1, 1,
                    rot, Block, 0);
                
            } else if (Block.RenderParam > 11) {
                Renderer.GenFace(X, Y + 1, Z,
                    1, 0, 0,
                    0, -1, 1,
                    rot, Block, 0);
                
            } else if (Block.RenderParam > 7) {
                Renderer.GenFace(X, Y, Z,
                    1, 1, 0,
                    0, 0, 1,
                    rot, Block, 0);
            } else if (Block.RenderParam > 3) {
                Renderer.GenFace(X, Y + 1, Z,
                    1, -1, 0,
                    0, 0, 1,
                    rot, Block, 0);
            } else {
                return;
            }
            
            LightingHelper.SetLightDown(WorldX, WorldY, WorldZ, Block.Biomed);
        }
    }
}