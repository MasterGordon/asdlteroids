using static SDL2.SDL;

enum Control
{
    THRUST,
    LEFT,
    RIGHT,
    SHOOT
}

static class ControlKeyExtension
{
    public static SDL_Keycode Key(this Control c)
    {
        switch (c)
        {
            case Control.THRUST:
                return SDL_Keycode.SDLK_UP;
            case Control.LEFT:
                return SDL_Keycode.SDLK_LEFT;
            case Control.RIGHT:
                return SDL_Keycode.SDLK_RIGHT;
            case Control.SHOOT:
                return SDL_Keycode.SDLK_SPACE;
            default:
                throw new ArgumentException("Invalid control");
        }
    }
}
