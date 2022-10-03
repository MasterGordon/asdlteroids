using static SDL2.SDL;


class Ship : Logic, Renderable
{
    const double SPEED = 0.3;
    const double ROTATION_SPEED = 5;

    double rotation = 0;
    bool thrust = false;
    private double dx, dy = 0;
    double x;
    double y;
    DateTime lastShot = DateTime.Now;

    public Ship(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public void Update(KeyState keyState, double dt)
    {
        thrust = false;
        if (keyState.isPressed(SDL_Keycode.SDLK_LEFT))
        {
            rotation -= ROTATION_SPEED * dt;
        }
        if (keyState.isPressed(SDL_Keycode.SDLK_RIGHT))
        {
            rotation += ROTATION_SPEED * dt;
        }
        if (keyState.isPressed(SDL_Keycode.SDLK_UP))
        {
            thrust = true;
            this.dx += Math.Cos(rotation) * SPEED * dt;
            this.dy += Math.Sin(rotation) * SPEED * dt;
        }
        if (keyState.isPressed(SDL_Keycode.SDLK_SPACE) && (DateTime.Now - lastShot).TotalMilliseconds > 200)
        {
            lastShot = DateTime.Now;
            var shot = new Shot(x, y, rotation);
            Scene.Instance.Shots.Add(shot);
        }
        rotation = rotation % (2 * Math.PI);
        if (rotation < 0)
        {
            rotation += 2 * Math.PI;
        }
        this.x += this.dx;
        this.y += this.dy;
        this.dx *= 0.9998;
        this.dy *= 0.9998;
        this.x = this.x % Scene.SCREEN_WIDTH;
        this.y = this.y % Scene.SCREEN_HEIGHT;
        if (this.x < 0)
        {
            this.x += Scene.SCREEN_WIDTH;
        }
        if (this.y < 0)
        {
            this.y += Scene.SCREEN_HEIGHT;
        }
        foreach (var asteroid in Scene.Instance.Asteroids)
        {
            if (Point.Distance(x, y, asteroid.X, asteroid.Y) < (int)asteroid.Size / 2)
            {
                Scene.Instance.Loose();
            }
        }
    }

    public void Render(Renderer renderer, double dx)
    {
        renderer.setColor(255, 255, 255);
        renderer.DrawLines(new double[][] {
            new double[]{x + Math.Cos(rotation) * 10, y + Math.Sin(rotation) * 10},
            new double[]{x + Math.Cos(rotation + Math.PI * 2 / 3) * 10, y + Math.Sin(rotation + Math.PI * 2 / 3) * 10},
            new double[]{x + Math.Cos(rotation + Math.PI * 4 / 3) * 10, y + Math.Sin(rotation + Math.PI * 4 / 3) * 10},
            new double[]{x + Math.Cos(rotation) * 10, y + Math.Sin(rotation) * 10}
            });
        if (thrust)
        {
            renderer.setColor(255, 0, 0);
            // Draw flame behind ship
            renderer.DrawLines(new double[][] {
                new double[]{x + Math.Cos(rotation + Math.PI * 2 / 3) * 10, y + Math.Sin(rotation + Math.PI * 2 / 3) * 10},
                new double[]{x + Math.Cos(rotation + Math.PI * 2 / 3) * 10 + Math.Cos(rotation + Math.PI) * 10, y + Math.Sin(rotation + Math.PI * 2 / 3) * 10 + Math.Sin(rotation + Math.PI) * 10},
                new double[]{x + Math.Cos(rotation + Math.PI * 4 / 3) * 10, y + Math.Sin(rotation + Math.PI * 4 / 3) * 10},
                new double[]{x + Math.Cos(rotation + Math.PI * 2 / 3) * 10, y + Math.Sin(rotation + Math.PI * 2 / 3) * 10}
                });
        }
    }
}
