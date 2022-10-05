using static SDL2.SDL_ttf;
using static SDL2.SDL;

class UI : Renderable
{
    IntPtr font;
    SDL_Color color;

    public UI()
    {
        TTF_Init();
        color = new SDL_Color();
        color.r = 255;
        color.g = 255;
        color.b = 255;
        color.a = 255;
        var assemblyName = this.GetType().Assembly.GetName().Name!;
        var fontStream = this.GetType().Assembly.GetManifestResourceStream($"{assemblyName}.assets.font.ttf");
        var fontFile = System.IO.Path.GetTempPath() + "asdlteroids-font.ttf";
        using (var fileStream = System.IO.File.Create(fontFile))
        {
            fontStream!.CopyTo(fileStream);
        }

        this.font = SDL2.SDL_ttf.TTF_OpenFont(fontFile, 18);
    }

    public void Render(Renderer renderer, double dx)
    {
        RenderText(renderer, "Score: " + Scene.Instance.Score, 0, 0);
        RenderText(renderer, "Level: " + Scene.Instance.Level, 0, 20);

        if (!Scene.Instance.running && DateTime.Now.Second % 2 == 0)
        {
            RenderText(renderer, "Press r to restart", Scene.SCREEN_WIDTH / 2, Scene.SCREEN_HEIGHT / 2, true);
        }
    }

    public void RenderText(Renderer renderer, String text, int x, int y, bool center = false)
    {
        var surfaceMessage = TTF_RenderText_Solid(this.font, text, this.color);

        var texture = SDL_CreateTextureFromSurface(renderer.GetRaw(), surfaceMessage);
        int width;
        int height;

        SDL_QueryTexture(texture, out _, out _, out width, out height);

        SDL_Rect rect = new SDL_Rect();
        rect.x = x;
        rect.y = y;
        rect.w = width;
        rect.h = height;

        if (center)
        {
            rect.x -= width / 2;
            rect.y -= height / 2;
        }

        SDL_RenderCopy(renderer.GetRaw(), texture, IntPtr.Zero, ref rect);
    }
}
