using static SDL2.SDL;

class Renderer
{
    IntPtr renderer;

    public Renderer(Window window)
    {
        this.renderer = SDL_CreateRenderer(window.GetWindow(), -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
    }

    public void Clear()
    {
        SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
        SDL_RenderClear(renderer);
    }

    public void Present()
    {
        SDL_RenderPresent(renderer);
    }

    public void DrawRect(double x, double y, int w, int h)
    {
        this.DrawRect((int)x, (int)y, w, h);
    }

    public void DrawRect(int x, int y, int w, int h)
    {
        SDL_Rect rect = new SDL_Rect();
        rect.x = x;
        rect.y = y;
        rect.w = w;
        rect.h = h;

        SDL_RenderFillRect(renderer, ref rect);
    }

    public void DrawLines(double[][] points)
    {
        SDL_Point[] sdlPoints = new SDL_Point[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            sdlPoints[i].x = (int)points[i][0];
            sdlPoints[i].y = (int)points[i][1];
        }

        SDL_RenderDrawLines(renderer, sdlPoints, points.Length);
    }

    public void setColor(int r, int g, int b, int a = 255)
    {
        SDL_SetRenderDrawColor(renderer, (byte)r, (byte)g, (byte)b, (byte)a);
    }

    public IntPtr GetRaw()
    {
        return renderer;
    }
}
