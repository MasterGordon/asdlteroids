class Ship : Logic, Renderable
{
    const double SPEED = 3 * Scene.SCALE;
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
        if (keyState.isPressed(Control.LEFT))
        {
            rotation -= ROTATION_SPEED * dt;
        }
        if (keyState.isPressed(Control.RIGHT))
        {
            rotation += ROTATION_SPEED * dt;
        }
        if (keyState.isPressed(Control.THRUST))
        {
            thrust = true;
            this.dx += Math.Cos(rotation) * SPEED * dt;
            this.dy += Math.Sin(rotation) * SPEED * dt;
        }
        if (keyState.isPressed(Control.SHOOT) && (DateTime.Now - lastShot).TotalMilliseconds > 200)
        {
            lastShot = DateTime.Now;
            var shot = new Shot(x, y, rotation);
            Scene.Instance.AudioPlayer.Play(Sound.SHOT);
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
            var halfAsteroidSize = (int)asteroid.Size / 2 * Scene.SCALE;
            if (Point.Distance(x, y, asteroid.X - halfAsteroidSize, asteroid.Y - halfAsteroidSize) < halfAsteroidSize)
            {
                Scene.Instance.Loose();
            }
        }
    }

    public void Render(Renderer renderer, double dx)
    {
        renderer.setColor(255, 255, 255);
        renderer.DrawLines(new double[][] {
            new double[] { x + Math.Cos(rotation) * 10 * Scene.SCALE, y + Math.Sin(rotation) * 10 * Scene.SCALE },
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3) * 10 * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3) * 10  * Scene.SCALE},
            new double[] { x + Math.Cos(rotation) * 10 * Scene.SCALE, y + Math.Sin(rotation) * 10  * Scene.SCALE},
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3 * 2) * 10 * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3 * 2) * 10 * Scene.SCALE },
            });
        renderer.DrawLines(new double[][] {
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3) * 5 * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3) * 5 * Scene.SCALE },
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3 * 2) * 5 * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3 * 2) * 5 * Scene.SCALE },
            });

        if (thrust)
        {
            renderer.setColor(255, 0, 0);
            var random = new Random();
            // Draw flame behind ship
            renderer.DrawLines(new double[][] {
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3) * 10 * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3) * 10  * Scene.SCALE},
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3 * 1.2) * 5+random.Next(5) * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3 * 1.2) * 5+random.Next(5) * Scene.SCALE },
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3 * 1.4) * 10 * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3 * 1.4) * 10 * Scene.SCALE },
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3 * 1.6) * 6+random.Next(5) * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3 * 1.6) * 5+random.Next(4) * Scene.SCALE },
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3 * 1.8) * 10 * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3 * 1.8) * 10 * Scene.SCALE },
            new double[] { x + Math.Cos(rotation + (Math.PI * 2) / 3 * 2) * 5+random.Next(5) * Scene.SCALE, y + Math.Sin(rotation + (Math.PI * 2) / 3 * 2) * 5+random.Next(5) * Scene.SCALE },
                });
        }
    }
}
