using System.Runtime.InteropServices;
using static SDL2.SDL;

class KeyState
{
    private byte[] keys;

    public KeyState()
    {
        var origArray = SDL_GetKeyboardState(out var arraySize);
        this.keys = new byte[arraySize];
        Marshal.Copy(origArray, keys, 0, arraySize);
    }

    public bool isPressed(SDL_Keycode keycode)
    {
        byte scanCode = (byte)SDL_GetScancodeFromKey(keycode);
        return (this.keys[scanCode] == 1);
    }

    public bool isPressed(Control c)
    {
        return this.isPressed(c.Key());
    }
}
