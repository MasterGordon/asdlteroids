using static SDL2.SDL;

enum Control
{
    THRUST,
    LEFT,
    RIGHT,
    SHOOT,
    RESTART
}

static class ControlKeyExtension
{
    public static SDL_Keycode Key(this Control c)
    {
        switch (c)
        {
            case Control.THRUST:
                return SDL_Keycode.SDLK_w;
            case Control.LEFT:
                return SDL_Keycode.SDLK_a;
            case Control.RIGHT:
                return SDL_Keycode.SDLK_d;
            case Control.SHOOT:
                return SDL_Keycode.SDLK_SPACE;
            case Control.RESTART:
                return SDL_Keycode.SDLK_r;
            default:
                throw new ArgumentException("Invalid control");
        }
    }
}
