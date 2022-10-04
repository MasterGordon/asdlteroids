enum Sound : int
{
    SHOT = 0,
    EXPLOSION = 1,
}

class AudioPlayer
{
    private Dictionary<Sound, byte[]> audioFiles = new();
    private string assemblyName;

    public AudioPlayer()
    {
        SDL2.SDL_mixer.Mix_OpenAudio(44100, SDL2.SDL_mixer.MIX_DEFAULT_FORMAT, 2, 2048);
        this.assemblyName = this.GetType().Assembly.GetName().Name!;
    }

    public void Register(Sound name, string path)
    {
        var stream = this.GetType().Assembly.GetManifestResourceStream($"{this.assemblyName}.{path}");
        var buffer = new byte[stream!.Length];
        stream.Read(buffer, 0, buffer.Length);
        this.audioFiles.Add(name, buffer);
    }

    public void Play(Sound name)
    {
        var buffer = this.audioFiles[name];
        var sound = SDL2.SDL_mixer.Mix_QuickLoad_WAV(buffer);
        SDL2.SDL_mixer.Mix_PlayChannel((int)name, sound, 0);
    }
}
