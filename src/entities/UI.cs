using static SDL2.SDL_ttf;
using static SDL2.SDL;

class UI : Renderable
{
    IntPtr font;

    public UI()
    {
        TTF_Init();
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
        var white = new SDL_Color();
        white.r = 255;
        white.g = 255;
        white.b = 255;
        white.a = 255;
        var surfaceMessage = TTF_RenderText_Solid(this.font, "Score: " + Scene.Instance.Score, white);

        var texture = SDL_CreateTextureFromSurface(renderer.GetRaw(), surfaceMessage);
        int width;
        int height;

        SDL_QueryTexture(texture, out _, out _, out width, out height);

        SDL_Rect rect = new SDL_Rect();
        rect.x = 0;
        rect.y = 0;
        rect.w = width;
        rect.h = height;

        SDL_RenderCopy(renderer.GetRaw(), texture, IntPtr.Zero, ref rect);

        if (!Scene.Instance.running)
        {
            var surfaceMessage2 = TTF_RenderText_Solid(this.font, "Press r to restart", white);
            var texture2 = SDL_CreateTextureFromSurface(renderer.GetRaw(), surfaceMessage2);
            int width2;
            int height2;

            SDL_QueryTexture(texture2, out _, out _, out width2, out height2);

            SDL_Rect rect2 = new SDL_Rect();
            rect2.x = Scene.SCREEN_WIDTH / 2 - width2 / 2;
            rect2.y = Scene.SCREEN_HEIGHT / 2 - height2 / 2;
            rect2.w = width2;
            rect2.h = height2;

            SDL_RenderCopy(renderer.GetRaw(), texture2, IntPtr.Zero, ref rect2);
        }
    }
}
