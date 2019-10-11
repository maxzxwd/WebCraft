namespace World.Renderer {
    public class SaplingRenderer : ARenderer {
        public SaplingRenderer(SectionRenderer renderer) : base(renderer) {}

        public override void Initialize() {
            Visible = true;
            Renderer.GenFace(X + 1, Y, Z + 1,
                -1, 0, -1,
                0, 1, 0,
                0, Block, 0);
            LightingHelper.SetLightUp(WorldX, WorldY, WorldZ, Block.Biomed, false);

            Renderer.GenFace(X, Y, Z,
                1, 0, 1,
                0, 1, 0,
                0, Block, 0);
            LightingHelper.SetLightUp(WorldX, WorldY, WorldZ, Block.Biomed, false);

            Renderer.GenFace(X, Y, Z + 1,
                1, 0, -1,
                0, 1, 0,
                0, Block, 0);
            LightingHelper.SetLightUp(WorldX, WorldY, WorldZ, Block.Biomed, false);

            Renderer.GenFace(X + 1, Y, Z,
                -1, 0, 1,
                0, 1, 0,
                0, Block, 0);
            LightingHelper.SetLightUp(WorldX, WorldY, WorldZ, Block.Biomed, false);
        }
    }
}