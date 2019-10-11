namespace World.Renderer {
    public class BedRenderer : ARenderer {
        public BedRenderer(SectionRenderer renderer) : base(renderer) {}

        public override void RenderWestSide() {
            Visible = true;
            switch ((int) Block.RenderParam) {
                case 0:
                    Renderer.GenFace(X + 0.1875f, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.4375f); 
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.1875f, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4609375f, 2.8125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 1:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z + 1,
                        0, 0.375f, 0,
                        0, 0, -1,
                        -3, Block, 0, 0.375f, 0.25f, 1.171875f, 2.125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f, 2.8125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.390625f, 2.4375f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 2:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        0, 0, 1,
                        0, Block, 0, 2.666667f, 0.09375f, 1.171875f, 1.625f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f, 2.4375f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.1875f, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.8125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 3:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        0, 0, 1,
                        1, Block, 0, 2.666667f, 0.09375f, 1, 1.625f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.1875f, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4609375f, 2.4375f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.390625f, 2.8125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 4:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z + 1,
                        0, 0.375f, 0,
                        0, 0, -1,
                        -3, Block, 0, 0.375f, 0.25f, 1.046875f, 2.8125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f, 2.25f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.390625f, 2.625f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 5:
                    Renderer.GenFace(X + 0.1875f, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.625f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.1875f, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4609375f, 2.25f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 6:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        0, 0, 1,
                        0, Block, 0, 2.666667f, 0.09375f, 1.171875f, 2.3125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.1875f, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4609375f, 2.625f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 1,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.390625f, 2.25f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 7:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        0, 0, 1,
                        1, Block, 0, 2.666667f, 0.09375f, 1, 2.3125f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f, 2.625f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.1875f, Y, Z + 0.1875f,
                        0, 0, -0.1875f,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.25f);
                    LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;
            }
        }

        public override void RenderEastSide() {
            Visible = true;
            switch ((int) Block.RenderParam) {
                case 0:
                    Renderer.GenFace(X, Y + 0.1875f, Z + 1,
                        0, 0, -1,
                        0, 0.375f, 0,
                        -1, Block, 0, 0.375f, 0.25f, 1.171875f, 2.125f);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f, 2.8125f);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 1:
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 0.046875f, 2.8125f);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.8125f, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f + 0.046875f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 2:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        0, 0, 1,
                        0, 0.375f, 0,
                        2, Block, 0, 2.666667f, 0.09375f, 1, 1 + 20.0f / 32);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 6.0f/128, 3f - 0.1875f * 3);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f - 3.0f / 128, 2.8125f);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 3:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        0, 0, 1,
                        0, 0.375f, 0,
                        3, Block, 0, 2.666667f, 0.09375f, 1 + 22.0f / 128, 1 + 20.0f / 32);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 3.0f / 128, 2.8125f);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 4:
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 0.046875f, 3f - 0.1875f * 4);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 0.8125f, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f + 0.046875f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 5:
                    Renderer.GenFace(X, Y + 0.1875f, Z + 1,
                        0, 0, -1,
                        0, 0.375f, 0,
                        -1, Block, 0, 0.375f, 0.25f, 1.171875f - 0.125f, 2.125f + 0.6875f);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.0859375f, 3f - 0.1875f * 4);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 6:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        0, 0, 1,
                        0, 0.375f, 0,
                        2, Block, 0, 2.666667f, 0.09375f, 1, 1 + 42.0f / 32);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 3.0f / 128, 3f - 0.1875f * 4);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;

                case 7:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        0, 0, 1,
                        0, 0.375f, 0,
                        3, Block, 0, 2.666667f, 0.09375f, 1 + 22.0f / 128, 1 + 42.0f / 32);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 6.0f/128, 3f - 0.1875f * 2);
                        
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0, 0.1875f, 0,
                        0, 0, -0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4140625f - 3.0f / 128, 3f - 0.1875f * 4);
                    LightingHelper.SetLightEast(WorldX + 1, WorldY, WorldZ, Block.Biomed, false);
                    break;
            }
        }

        public override void RenderUpSide() {
            Visible = true;
            if (Block.RenderParam < 4) {
                Renderer.GenFace(X, Y + 0.5625f, Z,
                    0, 0, 1,
                    1, 0, 0,
                    (int) Block.RenderParam, Block, 0, 1, 0.25f, 1.046875f, 1.625f);
            } else {
                Renderer.GenFace(X, Y + 0.5625f, Z,
                    0, 0, 1,
                    1, 0, 0,
                    (int) Block.RenderParam, Block, 0, 1, 0.25f, 1.046875f, 2.3125f);
            }
            LightingHelper.SetLightUp(WorldX, WorldY + 1, WorldZ, Block.Biomed, false);
        }

        public override void RenderDownSide() {
            Visible = true;
            int rot;
            switch ((int) Block.RenderParam) {
                case 0:
                    rot = 2;
                    Renderer.GenFace(X, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        1, Block, 0, 1, 0.046875f, 1.4375f, 2.53125f);
                    Renderer.GenFace(X, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        3, Block, 0, 1, 0.046875f, 1.4375f, 2.90625f);
                    break;
                case 1:
                    rot = 3;
                    Renderer.GenFace(X + 0.8125f, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4375f, 2.90625f);
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.53125f);
                    break;
                case 2:
                    rot = 0;
                    Renderer.GenFace(X, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        1, Block, 0, 1, 0.046875f, 1.4375f, 2.90625f);
                    Renderer.GenFace(X + 0.8125f, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4375f, 2.53125f);
                    break;
                case 3:
                    rot = 1;
                    Renderer.GenFace(X, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        3, Block, 0, 1, 0.046875f, 1.4375f, 2.53125f);
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.90625f);
                    break;

                case 4:
                    rot = 2;
                    Renderer.GenFace(X + 0.8125f, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4375f, 2.34375f);
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.71875f);
                    break;
                case 5:
                    rot = 3;
                    Renderer.GenFace(X, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        1, Block, 0, 1, 0.046875f, 1.4375f, 2.71875f);
                    Renderer.GenFace(X, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        3, Block, 0, 1, 0.046875f, 1.4375f, 2.34375f);
                    break;
                case 6:
                    rot = 0;
                    Renderer.GenFace(X, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        3, Block, 0, 1, 0.046875f, 1.4375f, 2.71875f);
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.8125f,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        0, Block, 0, 1, 0.046875f, 1.4375f, 2.34375f);
                    break;
                default:
                    rot = 1;
                    Renderer.GenFace(X, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        1, Block, 0, 1, 0.046875f, 1.4375f, 2.34375f);
                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z,
                        0.1875f, 0, 0,
                        0, 0, 0.1875f,
                        2, Block, 0, 1, 0.046875f, 1.4375f, 2.71875f);
                    break;
            }
            if (Block.RenderParam < 4) {
                Renderer.GenFace(X, Y + 0.1875f, Z,
                    1, 0, 0,
                    0, 0, 1,
                    rot, Block, 0, 1, 0.25f, 1.2109375f, 1.625f);
            } else {
                Renderer.GenFace(X, Y + 0.1875f, Z,
                    1, 0, 0,
                    0, 0, 1,
                    rot, Block, 0, 1, 0.25f, 1.2109375f, 2.3125f);
            }
            LightingHelper.SetLightDown(WorldX, WorldY - 1, WorldZ, Block.Biomed, false);
            LightingHelper.SetLightDown(WorldX, WorldY - 1, WorldZ, Block.Biomed, false);
            LightingHelper.SetLightDown(WorldX, WorldY - 1, WorldZ, Block.Biomed, false);
        }

        public override void RenderSouthSide() {
            Visible = true;
            switch ((int) Block.RenderParam) {
                case 0:
                    Renderer.GenFace(X, Y + 0.1875f, Z + 1,
                        1, 0, 0,
                        0, 0.375f, 0,
                        2, Block, 0, 2.666667f, 0.09375f, 1, 1 + 20.0f / 32);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f - 3.0f / 128, 2.8125f);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f + 6.0f/128, 3f - 0.1875f * 3);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);
                    break;
                case 1:
                    Renderer.GenFace(X, Y + 0.1875f, Z + 1,
                        1, 0, 0,
                        0, 0.375f, 0,
                        3, Block, 0, 2.666667f, 0.09375f, 1 + 22.0f / 128, 1 + 20.0f / 32);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f + 3.0f / 128, 2.8125f);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);
                    break;

                case 2:
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.5f - 0.109375f + 0.046875f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);
 
                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f + 0.046875f, 2.8125f);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;

                case 3:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z + 1,
                        -1, 0, 0,
                        0, 0.375f, 0,
                        -1, Block, 0, 0.375f, 0.25f, 1.171875f, 2.125f);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.5f - 0.109375f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f, 2.8125f);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);
                    break;

                case 4:
                    Renderer.GenFace(X, Y + 0.1875f, Z + 1,
                        0, 0.375f, 0,
                        1, 0, 0,
                        -4, Block, 0, 2.666667f, 0.09375f, 1 + 22.0f / 128, 1 + 42.0f / 32);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f + 3.0f / 128, 3f - 0.1875f * 4);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);
                    break;
                case 5:
                    Renderer.GenFace(X, Y + 0.1875f, Z + 1,
                        0, 0.375f, 0,
                        1, 0, 0,
                        -1, Block, 0, 2.666667f, 0.09375f, 1, 1 + 42.0f / 32);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f - 3.0f / 128, 3f - 0.1875f * 4);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f + 6.0f/128, 3f - 0.1875f * 2);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);
                    break;

                case 6:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z + 1,
                        -1, 0, 0,
                        0, 0.375f, 0,
                        -1, Block, 0, 0.375f, 0.25f, 1.171875f - 0.125f, 2.125f + 0.6875f);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.5f - 0.109375f, 3f - 0.1875f * 2);
                        
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.5f - 0.0859375f, 3f - 0.1875f * 4);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed, false);
                    break;

                case 7:
                    Renderer.GenFace(X + 0.8125f, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.5f - 0.109375f + 0.046875f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
 
                    Renderer.GenFace(X, Y, Z + 0.1875f,
                        0.1875f, 0, 0,
                        0, 0.1875f, 0,
                        0, Block, 0, 1, 0.046875f, 1.4140625f + 0.046875f, 3f - 0.1875f * 4);
                    LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;
            }
        }

        public override void RenderNorthSide() {
            Visible = true;
            switch ((int) Block.RenderParam) {
                case 0:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        1, 0, 0,
                        0, Block, 0, 2.666667f, 0.09375f, 1 + 22.0f / 128, 1 + 20.0f / 32);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1 - 0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 3.0f / 128, 2.8125f);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;
                case 1:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        1, 0, 0,
                        1, Block, 0, 2.666667f, 0.09375f, 1, 1 + 20.0f / 32);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1 - 0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 6.0f/128, 3f - 0.1875f * 3);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f - 3.0f / 128, 2.8125f);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;
                case 2:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        -1, 0, 0,
                        -3, Block, 0, 0.375f, 0.25f, 1.171875f, 2.125f);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f, 2.8125f);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;
                case 3:
                    Renderer.GenFace(X + 0.8125f, Y, Z + 1-0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 0.046875f, 2.8125f);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
 
                    Renderer.GenFace(X, Y, Z + 1-0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f + 0.046875f, 3f - 0.1875f * 3);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;

                case 4:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        1, 0, 0,
                        0, 0.375f, 0,
                        -2, Block, 0, 2.666667f, 0.09375f, 1, 1 + 42.0f / 32);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z + 1 - 0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 6.0f/128, 3f - 0.1875f * 2);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f - 3.0f / 128, 3f - 0.1875f * 4);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;

                case 5:
                    Renderer.GenFace(X, Y + 0.1875f, Z,
                        1, 0, 0,
                        0, 0.375f, 0,
                        -3, Block, 0, 2.666667f, 0.09375f, 1 + 22.0f / 128, 1 + 42.0f / 32);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z + 1 - 0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 3.0f / 128, 3f - 0.1875f * 4);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;

                case 6:
                    Renderer.GenFace(X + 0.8125f, Y, Z + 1-0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.4140625f + 0.046875f, 3f - 0.1875f * 4);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
 
                    Renderer.GenFace(X, Y, Z + 1-0.1875f,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f + 0.046875f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;

                case 7:
                    Renderer.GenFace(X + 1, Y + 0.1875f, Z,
                        0, 0.375f, 0,
                        -1, 0, 0,
                        -3, Block, 0, 0.375f, 0.25f, 1.171875f - 0.125f, 2.125f + 0.6875f);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.0859375f, 3f - 0.1875f * 4);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);

                    Renderer.GenFace(X + 1 - 0.1875f, Y, Z,
                        0, 0.1875f, 0,
                        0.1875f, 0, 0,
                        2, Block, 0, 1, 0.046875f, 1.5f - 0.109375f, 3f - 0.1875f * 2);
                    LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed, false);
                    break;
            }
        }
    }
}