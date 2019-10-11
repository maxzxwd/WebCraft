using UnityEngine;

namespace World.Renderer {
    public class GrassRenderer : ARenderer {
        public GrassRenderer(SectionRenderer renderer) : base(renderer) {}

        public override void RenderWestSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(1, 0, 0))) {
                Visible = true;
                Renderer.GenFace(X + 1, Y, Z,
                    0, 1, 0,
                    0, 0, 1,
                    2, Block, 0);
                LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, BiomedType.None);
                Renderer.GenFace(X + 1, Y, Z,
                    0, 1, 0,
                    0, 0, 1,
                    2, Block.GrassOverlay, 0);
                LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, BiomedType.Grass);
            }
        }

        public override void RenderEastSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(-1, 0, 0))) {
                Visible = true;
                Renderer.GenFace(X, Y, Z,
                    0, 0, 1,
                    0, 1, 0,
                    0, Block, 1);
                LightingHelper.SetLightEast(WorldX - 1, WorldY, WorldZ, BiomedType.None);
                Renderer.GenFace(X, Y, Z,
                    0, 0, 1,
                    0, 1, 0,
                    0, Block.GrassOverlay, 1);
                LightingHelper.SetLightEast(WorldX - 1, WorldY, WorldZ, BiomedType.Grass);
            }
        }

        public override void RenderUpSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, 1, 0))) {
                Visible = true;
                Renderer.GenFace(X, Y + 1, Z,
                    0, 0, 1,
                    1, 0, 0,
                    1, Block, 3);
                LightingHelper.SetLightUp(WorldX, WorldY + 1, WorldZ, BiomedType.Grass);
            }
        }

        public override void RenderDownSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, -1, 0))) {
                Visible = true;
                Renderer.GenFace(X, Y, Z,
                    1, 0, 0,
                    0, 0, 1,
                    3, Block, 2);
                LightingHelper.SetLightDown(WorldX, WorldY - 1, WorldZ, BiomedType.None);
            }
        }

        public override void RenderSouthSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, 0, 1))) {
                Visible = true;
                Renderer.GenFace(X, Y, Z + 1,
                    1, 0, 0,
                    0, 1, 0,
                    0, Block, 4);
                LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, BiomedType.None);
                Renderer.GenFace(X, Y, Z + 1,
                    1, 0, 0,
                    0, 1, 0,
                    0, Block.GrassOverlay, 4);
                LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, BiomedType.Grass);
            }
        }

        public override void RenderNorthSide() {
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, 0, -1))) {
                Visible = true;
                Renderer.GenFace(X, Y, Z,
                    0, 1, 0,
                    1, 0, 0,
                    2, Block, 5);
                LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, BiomedType.None);
                Renderer.GenFace(X, Y, Z,
                    0, 1, 0,
                    1, 0, 0,
                    2, Block.GrassOverlay, 5);
                LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, BiomedType.Grass);
            }
        }
    }
}