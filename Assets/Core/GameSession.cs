using System.Collections.Generic;
using World.Colormap;
using World;

public static class GameSession {
    public const int DEF_BLOCK_ID = 1;
    public static string Login;
    public static string ServerUrl;
    public static Block[] Palette;
    public static readonly IColorMap GrassColormap = new DefaultColormap();
    public static readonly IColorMap FoliageColormap = new DefaultColormap();
    public static readonly IColorMap BirchColormap = new BirchColormap();
    public static readonly IColorMap SpruceColormap = new SpruceColormap();
    private static Dictionary<int, int> _blocksRemapper;

    public static void SetRemapper(Dictionary<int, int> blocksRemapper) {
        _blocksRemapper = blocksRemapper;
    }

    public static int FromMC(int rawId) {
        if (_blocksRemapper.ContainsKey(rawId)) {
            return _blocksRemapper[rawId];
        }

        return DEF_BLOCK_ID;
    }
}