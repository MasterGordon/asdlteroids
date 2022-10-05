using static SDL2.SDL;

class Scene
{
    public const double SCALE = 2;
    public const int SCREEN_WIDTH = (int)(800 * SCALE);
    public const int SCREEN_HEIGHT = (int)(600 * SCALE);
    private Ship ship;
    private UI ui;
    private Renderer renderer;
    private static Scene? instance;

    public Ufo ufo;
    public HashSet<Shot> Shots;
    public HashSet<Asteroid> Asteroids;
    public int Score = 0;
    public int Level = 1;
    public AudioPlayer AudioPlayer;
    public bool running = true;

    public Scene(Renderer renderer)
    {
        this.ship = new Ship(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
        this.ufo = new Ufo(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
        this.ui = new UI();
        this.Shots = new HashSet<Shot>();
        this.Asteroids = new HashSet<Asteroid>();
        this.renderer = renderer;
        this.AudioPlayer = new AudioPlayer();
        this.AudioPlayer.Register(Sound.SHOT, "assets.shot.wav");
        this.AudioPlayer.Register(Sound.EXPLOSION, "assets.explode.wav");
        instance = this;
    }

    public void Restart()
    {
        this.running = true;
        this.Score = 0;
        this.Level = 1;
        this.Asteroids.Clear();
        this.Shots.Clear();
        this.ship = new Ship(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
        SpawnAsteroids(Level + 3);
    }

    public int Run()
    {
        SpawnAsteroids(Level + 3);
        double dx = 0;
        DateTime start = DateTime.Now;
        while (true)
        {
            start = DateTime.Now;
            pollEvents();

            var entities = new List<Object>();
            entities.Add(ship);
            entities.Add(ui);
            // entities.Add(ufo);
            entities.AddRange(Shots);
            entities.AddRange(Asteroids);

            var keyState = new KeyState();
            renderer.Clear();

            foreach (var entity in entities)
            {
                if (entity is Logic && running)
                {
                    ((Logic)entity).Update(keyState, dx);
                }
                if (entity is Renderable)
                {
                    ((Renderable)entity).Render(renderer, dx);
                }
            }

            if (Asteroids.Count == 0)
            {
                Level++;
                SpawnAsteroids(Level + 3);
            }
            if (!running)
            {
                if (keyState.isPressed(Control.RESTART))
                {
                    this.Restart();
                }
            }

            renderer.Present();
            SDL_Delay(10);
            DateTime end = DateTime.Now;
            dx = (end - start).TotalSeconds;
        }
    }

    public void SpawnAsteroids(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnAsteroid();
        }
    }

    public void SpawnAsteroid()
    {
        var edge = new Random().Next(0, 4);
        var x = 0;
        var y = 0;
        switch (edge)
        {
            case 0:
                x = 0;
                y = new Random().Next(0, SCREEN_HEIGHT);
                break;
            case 1:
                x = SCREEN_WIDTH;
                y = new Random().Next(0, SCREEN_HEIGHT);
                break;
            case 2:
                x = new Random().Next(0, SCREEN_WIDTH);
                y = 0;
                break;
            case 3:
                x = new Random().Next(0, SCREEN_WIDTH);
                y = SCREEN_HEIGHT;
                break;
        }
        Asteroids.Add(new Asteroid(x, y, new Random().Next(0, 200) / 100.0 * Math.PI, Size.Large));
    }

    public void Loose()
    {
        running = false;
    }

    private void pollEvents()
    {
        SDL_Event e;
        while (SDL_PollEvent(out e) != 0)
        {
            if (e.type == SDL_EventType.SDL_QUIT)
            {
                System.Environment.Exit(0);
            }
            if (e.type == SDL_EventType.SDL_KEYDOWN)
            {
                if (e.key.keysym.sym == SDL_Keycode.SDLK_ESCAPE)
                {
                    System.Environment.Exit(0);
                }
            }
        }
    }

    public static Scene Instance
    {
        get
        {
            if (instance == null)
            {
                throw new Exception("Scene not initialized");
            }
            return instance;
        }
    }
}
