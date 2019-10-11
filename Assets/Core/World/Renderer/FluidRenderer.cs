namespace World.Renderer {
    public class FluidRenderer : ARenderer {
        private float _level;
        private float _xrLevel;
        private float _xlLevel;
        private float _zrLevel;
        private float _zlLevel;
        private bool _xrNeed;
        private bool _xlNeed;
        private bool _zrNeed;
        private bool _zlNeed;
        private int _upNeighborId;

        public FluidRenderer(SectionRenderer renderer) : base(renderer) {}

        public override void Initialize() {
            _level = Block.RenderParam;
            _upNeighborId = GetNeighbor(0, 1, 0);
            if (_upNeighborId != 0 &&
                GameSession.Palette[_upNeighborId].RenderType == RenderType.Fluid) {
                _level = 1;
            }
            _xrLevel = 0;
            _xlLevel = 0;
            _zrLevel = 0;
            _zlLevel = 0;
            _xrNeed = true;
            _xlNeed = true;
            _zrNeed = true;
            _zlNeed = true;
            int neighborId = GetNeighbor(1, 0, 0);
            if (!Renderer.IsRenderNeed(Block, BlockId, neighborId)) {
                Block neighbor = GameSession.Palette[neighborId];
                if (neighbor.RenderType == RenderType.Fluid) {
                    _xlLevel = neighbor.RenderParam;
                    if (neighbor.RenderParam >= _level ||
                        GetNeighbor(1, 1, 0) != 0) {
                        _xlNeed = false;
                    }
                }
            }
            neighborId = GetNeighbor(-1, 0, 0);
            if (!Renderer.IsRenderNeed(Block, BlockId, neighborId)) {
                Block neighbor = GameSession.Palette[neighborId];
                if (neighbor.RenderType == RenderType.Fluid) {
                    _xrLevel = neighbor.RenderParam;
                    if (neighbor.RenderParam >= _level || 
                        GetNeighbor(-1, 1, 0) != 0) {
                        _xrNeed = false;
                    }
                }
            }

            neighborId = GetNeighbor(0, 0, 1);
            if (!Renderer.IsRenderNeed(Block, BlockId, neighborId)) {
                Block neighbor = GameSession.Palette[neighborId];
                if (neighbor.RenderType == RenderType.Fluid) {
                    _zlLevel = neighbor.RenderParam;
                    if (neighbor.RenderParam >= _level ||
                        GetNeighbor(0, 1, 1) != 0) {
                        _zlNeed = false;
                    }
                }
            }
            neighborId = GetNeighbor(0, 0, -1);
            if (!Renderer.IsRenderNeed(Block, BlockId, neighborId)) {
                Block neighbor = GameSession.Palette[neighborId];
                if (neighbor.RenderType == RenderType.Fluid) {
                    _zrLevel = neighbor.RenderParam;
                    if (neighbor.RenderParam >= _level ||
                        GetNeighbor(0, 1, -1) != 0) {
                        _zrNeed = false;
                    }
                }
            }
        }

        public override void RenderWestSide() {
            Visible = true;
            if (_xlNeed) {
                Renderer.GenFace(X + 1, Y + _xlLevel, Z,
                    0, _level - _xlLevel, 0,
                    0, 0, 1,
                    2, Block, 0, _level - _xlLevel);
                LightingHelper.SetLightWest(WorldX + 1, WorldY, WorldZ, Block.Biomed);
            }
        }

        public override void RenderEastSide() {
            Visible = true;
            if (_xrNeed) {
                Renderer.GenFace(X, Y + _xrLevel, Z,
                    0, 0, 1,
                    0, _level - _xrLevel, 0,
                    0, Block, 1, _level - _xrLevel);
                LightingHelper.SetLightEast(WorldX - 1, WorldY, WorldZ, Block.Biomed);
            }
        }

        public override void RenderUpSide() {
            Visible = true;
            if (Renderer.IsRenderNeed(Block, BlockId, _upNeighborId)) {
                Renderer.GenFace(X, Y + _level, Z,
                    0, 0, 1,
                    1, 0, 0,
                    1, Block, 3);
                LightingHelper.SetLightUp(WorldX, WorldY + 1, WorldZ, Block.Biomed);
            }
        }

        public override void RenderDownSide() {
            Visible = true;
            if (Renderer.IsRenderNeed(Block, BlockId, GetNeighbor(0, -1, 0))) {
                Renderer.GenFace(X, Y, Z,
                    1, 0, 0,
                    0, 0, 1,
                    3, Block, 2);
                LightingHelper.SetLightDown(WorldX, WorldY - 1, WorldZ, Block.Biomed);
            }
        }

        public override void RenderSouthSide() {
            Visible = true;
            if (_zlNeed) {
                Renderer.GenFace(X, Y + _zlLevel, Z + 1,
                    1, 0, 0,
                    0, _level - _zlLevel, 0,
                    0, Block, 4, _level - _zlLevel);
                LightingHelper.SetLightSouth(WorldX, WorldY, WorldZ + 1, Block.Biomed);
            }
        }

        public override void RenderNorthSide() {
            Visible = true;
            if (_zrNeed) {
                Renderer.GenFace(X, Y + _zrLevel, Z,
                    0, _level - _zrLevel, 0,
                    1, 0, 0,
                    2, Block, 5, _level - _zrLevel);
                LightingHelper.SetLightNorth(WorldX, WorldY, WorldZ - 1, Block.Biomed);
            }
        }
    }
}